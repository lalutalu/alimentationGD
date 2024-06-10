using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
namespace BottinToCsvForm.Parsing
{
    public class CirculaireToCSV
    {
        private const double PERCENTAGE = 14.95;

        public static string ExtractProductName(string productLine)
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

        public static string ExtractProductUpc(string productLine)
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

        public static string ExtractProductCode(string productLine)
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

        public static List<string> ExtractProductWithDetails(string pdfPath)
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

        public static string ExtractOriginalPrice(string productLine)
        {
            Match match = Regex.Match(productLine, @"\b(\d+\.\d+)\s+");
            return match.Success ? match.Groups[1].Value : "No Original Price";
        }

        public static string ExtractQuantity(string productLine)
        {
            Match match = Regex.Match(productLine, @"\d+\s+(\d+)");
            return match.Success ? match.Groups[1].Value : "No Quantity";
        }

        public static (string Weight, string Unit) ExtractWeight(string productLine)
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

        public static double CalculateNewPrice(double ogPrice)
        {
            double newPrice = ogPrice * (1 + PERCENTAGE / 100);
            return Math.Round(newPrice, 2);
        }

        public static List<Product> ReadProductsFromCsv(string filePath)
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
                    string weight = values[3].Split('X')[0];
                    string unit = values[3];
                    Product product = new Product();
                    product.HandleID = handleId;
                    product.Nom = name;
                    product.Prix = prixNew;
                    product.Code39 = code39;
                    products.Add(product);
                }
            }
            return products;
        }
    }
}
