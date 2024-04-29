using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCSV
{
    public class PdfDataParsing
    {
        public static List<string> ParsePdf(string filePath)
        {
            List<string> dataObjects = new List<string>();

            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;

                for (int i = 39; i <= totalPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    string pageData = pageText;
                    dataObjects.Add(pageData);
                }
            }
            return dataObjects;
        }
        public List<string> ParseProductName(string dataString)
        {
            List<string> productNames = new List<string>();
            string pattern = @"(?<= \s*\d{7} \s+)((?:\S+\s*)+)(?= \d+)";
            MatchCollection matches = Regex.Matches(dataString, pattern);

            foreach (Match match in matches)
            {
                productNames.Add(match.Value);
            }
            return productNames;
        }
        public List<string> ParsePackage(string dataString)
        {
            List<string> packages = new List<string>();
            string pattern = @"";
            MatchCollection matches = Regex.Matches(dataString, pattern);

            foreach (Match match in matches)
            {
                packages.Add(match.Value);
            }
            return packages;
        }

        public List<string> ParseQuantity(string dataString)
        {
            List<string> quantity = new List<string>();
            string pattern = @"";
            MatchCollection matches = Regex.Matches(dataString, pattern);

            foreach (Match match in matches)
            {
                quantity.Add(match.Value);
            }
            return quantity;
        }
        public List<double> ParsePrix(string dataString)
        {
            List<string> initialPrices = new List<string>();
            List<double> newPrices = new List<double>();
            string pattern = @"";
            MatchCollection matches = Regex.Matches(dataString, pattern);

            foreach (Match match in matches)
            {
                initialPrices.Add(match.Value);
            }

            foreach (string price in initialPrices)
            {
                var newPrice = CalculateNewPrice(price);
                newPrices.Add(newPrice);

            }
            return newPrices;
        }

        public List<string> ParseCode39(string dataString)
        {
            List<string> codes = new List<string>();
            string patternCode39 = @"\d{7}";
            MatchCollection matches = Regex.Matches(dataString, patternCode39);

            foreach (Match match in matches)
            {
                codes.Add(match.Value);
            }
            return codes;
        }

        public double CalculateNewPrice(string initialPrice)
        {
            double price = double.Parse(initialPrice);
            double newPrice = price / 0.70;
            return newPrice;

        }
    }
}
