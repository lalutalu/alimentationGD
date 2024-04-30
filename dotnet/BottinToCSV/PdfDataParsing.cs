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

                    // Call LineSplitter to split the page text
                    List<string> lines = LineSplitter.SplitLines(pageText);

                    foreach (string line in lines)
                    {
                        // Add each split line to dataObjects for further processing
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
            string pattern = @"\d{7}\s(.*?)(?=\s\d{1,2}\s\d+)";
            Match match = Regex.Match(InitialString, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return "";
        }

        public string ParseQuantity(string InitialString)
        {
            string pattern = @"\d{7}\s(.*?)(?=\s\d{1,2}\s\d+)\s(\d+)";
            Match match = Regex.Match(InitialString, pattern);
            if (match.Success)
            {
                return match.Groups[2].Value;
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
            return "";
        }

        public double ParsePrix(string dataString)
        {
            string pattern = @"\d{1,4}$";
            Match match = Regex.Match(dataString, pattern);

            if (match.Success)
            {
                var newPrice = CalculateNewPrice(double.Parse(match.Value));
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
