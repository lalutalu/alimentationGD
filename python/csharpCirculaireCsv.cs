using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PdfToCSV
{
    public class Product
    {
        public string Name { get; set; }
        public double PrixOg { get; set; }
        public double PrixNew { get; set; }
        public string Code39 { get; set; }
        public string Weight { get; set; }
        public string Unit { get; set; }
        public string HandleId { get; set; }
        public string FieldType { get; set; } = "Product";
        public string Visible { get; set; } = "true";
        public string DiscountMode { get; set; } = "PERCENT";
        public string Inventory { get; set; } = "InStock";

        public Product(string name, double prixOg, double prixNew, string code39, string weight, string unit, string handleid)
        {
            Name = name;
            PrixOg = prixOg;
            PrixNew = prixNew;
            Code39 = code39;
            Weight = weight;
            Unit = unit;
            HandleId = handleid;
        }
    }

    class Program
    {
        private const double PERCENTAGE = 14.95;

        static string ExtractProductName(string productLine)
        {
            string pattern = @"IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})";
            Match match = Regex.Match(productLine, pattern);
            if (match.Success)
            {
                string capturedText = match.Groups[1].Value;
                return capturedText.Trim();
            }
            else
            {
                return "No Name";
            }
        }

        static string ExtractProductUpc(string productLine)
        {
            string pattern = @"IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})";
            Match match = Regex.Match(productLine, pattern);
            if (match.Success)
            {
                string capturedText = match.Groups[2].Value;
                return capturedText.Trim();
            }
            else
            {
                return "No UPC";
            }
        }

        static string ExtractProductCode(string productLine)
        {
            Match match = Regex.Match(productLine, @"\b(\d{7})[A-Za-z]?\b");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                Console.WriteLine($"No Code: {productLine}");
                return "No Code";
            }
        }

        static List<string> ExtractProductWithDetails(string pdfPath)
        {
            List<string> productLines = new List<string>();
            using (PdfReader reader = new PdfReader(pdfPath))
            {
                for (int pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string content = PdfTextExtractor.GetTextFromPage(reader, pageNum, strategy);

                    string[] lines = content.Split('\n');
                    string productLine = "";

                    foreach (string line in lines)
                    {
                        if (Regex.IsMatch(line, @"^IMPACT-\d+\s+"))
                        {
                            productLine = line;
                        }
                        else if (!string.IsNullOrEmpty(productLine))
                        {
                            productLine += $" {line}";
                            productLines.Add(productLine);
                            productLine = "";
                        }
                    }
                }
            }
            return productLines;
        }

        static string ExtractOriginalPrice(string productLine)
        {
            Match match = Regex.Match(productLine, @"\b(\d+\.\d+)\s+");
            return match.Success ? match.Groups[1].Value : "No Original Price";
        }

        static string ExtractQuantity(string productLine)
        {
            Match match = Regex.Match(productLine, @"\d+\s+(\d+)");
            return match.Success ? match.Groups[1].Value : "No Quantity";
        }

        static (string Weight, string Unit) ExtractWeight(string productLine)
        {
            Match match = Regex.Match(productLine, @"\d+\s+(\d+)\s+([A-Za-z]+)");

            if (match.Success)
            {
                return (match.Groups[1].Value, match.Groups[2].Value);
            }
            else
            {
                Match weightMatch = Regex.Match(productLine, @"(\d+)\D*$");
                string weight = weightMatch.Success ? weightMatch.Groups[1].Value : "No Weight";

                Match unitMatch = Regex.Match(productLine, @"([^\d]+)$");
                string unit = unitMatch.Success ? unitMatch.Groups[1].Value.Trim() : "No Unit";

                if (weight == "No Weight" && unit == "No Unit")
                {
                    Console.WriteLine($"No Weight or Unit: {productLine}");
                }

                return (weight, unit);
            }
        }

        static double CalculateNewPrice(double ogPrice)
        {
            double newPrice = ogPrice * (1 + PERCENTAGE / 100);
            return Math.Round(newPrice, 2);
        }

        static void Main(string[] args)
        {
            string pdfPath = "../pdfs/circulaire-metro.pdf";
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string desktopDir = Path.Combine(homeDir, "Desktop");
            string csvPath = Path.Combine(desktopDir, "Produits_2024-06-07.csv");
            string newCsvPath = Path.Combine(desktopDir, "ProduitsAvecRabais.csv");

            List<string> combinedLines = ExtractProductWithDetails(pdfPath);
            List<Product> pdfProducts = new List<Product>();
            List<Product> csvProducts = new List<Product>();

            foreach (string line in combinedLines)
            {
                (string weight, string unit) = ExtractWeight(line);

                Product product = new Product(
                    ExtractProductName(line),
                    double.Parse(ExtractOriginalPrice(line)),
                    CalculateNewPrice(double.Parse(ExtractOriginalPrice(line))),
                    ExtractProductCode(line),
                    weight,
                    unit,
                    ""
                );

                pdfProducts.Add(product);
            }

            csvProducts = ReadProductsFromCsv(csvPath);

            foreach (Product csvProduct in csvProducts)
            {
                foreach (Product pdfProduct in pdfProducts)
                {
                    if (csvProduct.Code39 == pdfProduct.Code39)
                    {
                        csvProduct.Name = pdfProduct.Name;
                        csvProduct.PrixOg = pdfProduct.PrixOg;
                        csvProduct.PrixNew = pdfProduct.PrixNew;
                        csvProduct.Code39 = pdfProduct.Code39;
                        csvProduct.Weight = pdfProduct.Weight;
                        csvProduct.Unit = pdfProduct.Unit;
                        break;
                    }
                }
            }

            CreateCsv(newCsvPath, csvProducts);
            Console.WriteLine($"CSV file '{newCsvPath}' created successfully.");
        }

        static List<Product> ReadProductsFromCsv(string filePath)
        {
            List<Product> products = new List<Product>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isHeaderLine = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeaderLine)
                    {
                        isHeaderLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');
                    string handleId = values[0];
                    string name = values[2];
                    double prixOg = double.Parse(values[6]);
                    double prixNew = double.Parse(values[8]);
                    string code39 = values[6];
                    string weight = values[3].Split('x')[0];
                    string unit = values[3].Split('x')[1];

                    Product product = new Product(name, prixOg, prixNew, code39, weight, unit, handleId);
                    products.Add(product);
                }
            }
            return products;
        }

        static void CreateCsv(string filePath, List<Product> products)
        {
            string[] headings = {
                "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible", "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2", "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4", "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6", "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2", "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5", "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2", "customTextMandatory2", "brand"
            };

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(string.Join(",", headings));

                foreach (Product product in products)
                {
                    string[] values = {
                        product.HandleId, product.FieldType, product.Name, $"{product.Weight}x{product.Unit}", "", "metro", product.Code39, "", product.PrixNew.ToString(), "", product.Visible, product.DiscountMode, "0", product.Inventory, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""
                    };
                    writer.WriteLine(string.Join(",", values));
                }
            }
        }
    }
}

// This C# code translates the provided Python code to C#. It uses the `iTextSharp` library for PDF parsing and handling. The code defines a `Product` class to store product information and provides methods for extracting product details from PDF files and CSV files. The main functionality remains the same as the Python code, where it reads product information from a PDF file, updates the product details with information from a CSV file, and creates a new CSV file with the updated product information and discounted prices.
