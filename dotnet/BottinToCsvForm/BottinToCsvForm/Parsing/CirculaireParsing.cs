using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCsvForm.Parsing
{
    public class CirculaireParsing
    {
        private string ExtractProductName(string productLine)
        {
            var match = Regex.Match(productLine, @"IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})");
            if (match.Success)
            {
                string capturedText = match.Groups[1].Value.Trim();
                return capturedText;
            }
            return "No Name";
        }

        private string ExtractProductCode(string productLine)
        {
            var match = Regex.Match(productLine, @"\b(\d{7})[A-Za-z]?\b");
            if (match.Success)
            {
                string capturedText = match.Groups[1].Value.Trim();
                return capturedText;
            }
            return "No Code";
        }

        private string ExtractOriginalPrice(string productLine)
        {
            var matches = Regex.Matches(productLine, @"\b(\d+\.\d+)\s+");
            //if (match.Success)
            //{
            //    string capturedText = match.Groups[2].Value.Trim();
            //    return capturedText;
            //}
            if (matches.Count >= 3) // Check if there are at least two matches
            {
                string capturedText = matches[2].Groups[1].Value.Trim(); // Second match is at index 1
                return capturedText;
            }
            return "No Original Price";
        }

        private string ExtractQuantity(string productLine)
        {
            var match = Regex.Match(productLine, @"\d+\s+(\d+)");
            if (match.Success)
            {
                string capturedText = match.Groups[1].Value.Trim();
                return capturedText;
            }
            return "No Quantity";
        }
        private double CalculateNewPrice(double ogPrice)
        {
            double newPrice = ogPrice / (0.87);
            return Math.Round(newPrice, 2);
        }

        private Tuple<string, string> ExtractWeightAndUnit(string productLine)
        {
            var match = Regex.Match(productLine, @"\d+\s+(\d+)\s+([A-Za-z]+)");
            if (match.Success)
            {
                string weight = match.Groups[1].Value.Trim();
                string unit = match.Groups[2].Value.Trim();
                return new Tuple<string, string>(weight, unit);
            }
            else
            {
                var weightMatch = Regex.Match(productLine, @"(\d+)\D*$");
                string weight = weightMatch.Success ? weightMatch.Groups[1].Value.Trim() : "No Weight";

                var unitMatch = Regex.Match(productLine, @"([^\d]+)$");
                string unit = unitMatch.Success ? unitMatch.Groups[1].Value.Trim() : "No Unit";

                if (weight == "No Weight" && unit == "No Unit")
                {
                    Console.WriteLine($"No Weight or Unit: {productLine}");
                }
                return Tuple.Create(weight, unit);
            }
        }

        private List<string> ExtractProductWithDetails(string pdfPath)
        {
            List<string> productLines = new List<string>();
            using (PdfReader reader = new PdfReader(pdfPath))
            {
                for (int pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
                {
                    string pageContent = PdfTextExtractor.GetTextFromPage(reader, pageNum);
                    string[] lines = pageContent.Split('\n');
                    string productLine = "";

                    foreach (string line in lines)
                    {
                        if (Regex.IsMatch(line, @"^IMPACT-\d+\s+"))
                        {
                            productLine = line;
                        }
                        else if (!string.IsNullOrEmpty(productLine))
                        {
                            productLine += " " + line;
                            productLines.Add(productLine);
                            productLine = "";
                        }
                    }
                }
                return productLines;
            }
        }

        public List<Product> ExtractCirculaireProducts(string filePath)
        {
            List<Product> circulaireProducts = new List<Product>();
            List<string> productLines = ExtractProductWithDetails(filePath);

            foreach (string line in productLines)
            {
                string productName = ExtractProductName(line);
                string productCode = ExtractProductCode(line);
                string originalPriceStr = ExtractOriginalPrice(line);
                double originalPrice = Convert.ToDouble(originalPriceStr);
                double newPrice = CalculateNewPrice(originalPrice);
                string quantity = ExtractQuantity(line);
                Tuple<string, string> weightAndUnit = ExtractWeightAndUnit(line);

                List<string> categories = new List<string>
                {
                    "Produits Secs",

                };
                Product product = new Product
                {
                    Code39 = productCode.PadLeft(7, '0'),
                    Nom = productName,
                    Prix = newPrice,
                    Quantite = quantity,
                    Categories = categories,
                    Format = $"{weightAndUnit.Item1} {weightAndUnit.Item2}"
                };

                circulaireProducts.Add(product);
            }
            return circulaireProducts;
        }
    }
}
