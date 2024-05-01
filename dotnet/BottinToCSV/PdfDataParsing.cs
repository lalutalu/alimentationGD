using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCSV
{
    public class PdfDataParsing
    {
        // Change Page Values everytime a bottin is changed
        public static List<string> ParsePdf(string filePath)
        {
            List<string> dataObjects = new List<string>();
            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;
                for (int i = 39; i <= totalPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    List<string> lines = LineSplitter.SplitLines(pageText);
                    foreach (string line in lines)
                    {
                        dataObjects.Add(line);
                    }
                }
            }
            return dataObjects;
        }
        // Change Page Values everytime a bottin is changed
        public static List<string> ParsePdfDelete(string filePath)
        {
            List<string> dataObjects = new List<string>();
            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;
                for (int i = 9; i <= 11; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    List<string> lines = LineSplitter.SplitLines(pageText);
                    foreach (string line in lines)
                    {
                        dataObjects.Add(line);
                    }
                }
            }
            return dataObjects;
        }
        public Product ParseProducts(string dataString)
        {
            Product product = new Product();
            product.Code39 = ParseCode39(dataString);
            product.Nom = ParseNames(dataString);
            product.Quantite = ParseQuantity(dataString);
            product.Format = ParseUnit(dataString);
            product.Prix = ParsePrix(dataString);

            return product;
        }

        public string ParseCode39(string InitialString)
        {
            string pattern = @"^\d{7}";
            Match match = Regex.Match(InitialString, pattern);
            if (match.Success)
            {
                return match.Value;
            }
            return "";
        }

        public string ParseNames(string InitialString)
        {
            string pattern = @"^\d+\s+(.*?)(?=\s+\d+\s+\d+)";
            Match match = Regex.Match(InitialString, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "";
        }

        public string ParseQuantity(string initialString)
        {
            string pattern = @"\d{7}\s(.*?)(?=\s\d{1,2}\s\d+)\s(\d+)";
            Match match = Regex.Match(initialString, pattern);
            if (match.Success)
            {
                return match.Groups[2].Value;
            }
            else
            {
                pattern = @"\d{7}\s(.*?)\s(\d+)";
                match = Regex.Match(initialString, pattern);
                if (match.Success)
                {
                    return match.Groups[2].Value;
                }
            }
            return "";
        }

        public string ParseUnit(string InitialString)
        {
            string pattern = @"\d{7}\s.*?(?=\s\d{1,2}\s\d+)\s\d+\s(\d+[A-Z]+)";
            Match match = Regex.Match(InitialString, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                pattern = @"\d{7}\s.*?(?:\s+\S+\s+)(\d+[A-Z]+)";
                match = Regex.Match(InitialString, pattern);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
            return "";
        }

        public double ParsePrix(string dataString)
        {
            string pattern = @"\d{1,4}$";
            Match match = Regex.Match(dataString, pattern);

            if (match.Success)
            {
                var newPrice = double.Parse(match.Value) / 100;
                newPrice = CalculateNewPrice(newPrice);
                return newPrice;
            }

            return 0;
        }

        public double CalculateNewPrice(double initialPrice)
        {
            double newPrice = initialPrice / 0.70;
            return newPrice;
        }
    }
}
