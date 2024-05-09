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
                Console.WriteLine("Entrez le numéro de la page avec le titre commande du bottin: ");
                int startOfProducts = int.Parse(Console.ReadLine());
                for (int i = startOfProducts; i <= totalPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    List<string> lines = LineSplitter.SplitLines(pageText);
                    List<string> linesToProcess = new List<string>(lines);
                    foreach (string line in linesToProcess)
                    {
                        dataObjects.Add(line);
                    }
                }
            }
            return dataObjects;
        }
        public static List<string> ParsePdfDelete(string filePath)
        {
            List<string> dataObjects = new List<string>();
            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;
                Console.WriteLine("Entrez le numéro de la page de PRODUITS ENLEVES: ");
                int startDelete = int.Parse(Console.ReadLine());
                Console.WriteLine("Entrez le numéro de la dernière page de PRODUITS ENLEVES: ");
                int endDelete = int.Parse(Console.ReadLine());
                for (int i = startDelete; i <= endDelete; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    List<string> lines = LineSplitter.SplitLines(pageText);
                    List<string> linesToProcess = new List<string>(lines);
                    foreach (string line in linesToProcess)
                    {
                        dataObjects.Add(line);
                        if (line.Contains(@"PRODUITS RETIRES TEMPORAIREMENT\r\n"))
                        {
                            Console.WriteLine(dataObjects.Count);
                            return dataObjects;
                        };
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

        public string ParseUnit(string initialString)
        {
            if (initialString.Contains("CADENAS CUIVRE ASSORTIS"))
            {
                return "1UN";
            }

            string[] patterns = {
                @"\d{7}\s.*?(?=\s\d{1,2}\s\d+)\s\d+\s((\d+x)*\d+(\.\d+)?)\s*(G|g|l|L|KG|K|UN|U|ML|M|PQ)\b",
                @"((\d+X)*\d+(\.\d+)?)\s*(G|g|l|L|KG|K|UN|U|ML|M|PQ)\b",
            };

            foreach (string pattern in patterns)
            {
                Match match = Regex.Match(initialString, pattern);
                if (match.Success)
                {
                    string unit = match.Groups[4].Value.ToUpper();
                    string number = match.Groups[1].Value;

                    if (number.EndsWith("x") || number.EndsWith("X"))
                    {
                        int startIndex = match.Index + match.Length;
                        Match nextNumberMatch = Regex.Match(initialString.Substring(startIndex), @"\d+(\.\d+)?");
                        if (nextNumberMatch.Success)
                        {
                            number += "x" + nextNumberMatch.Value;
                        }
                    }

                    if (match.Groups.Count > 5 && match.Groups[5].Success)
                    {
                        string extra = match.Groups[5].Value.Trim();
                        return number + " " + unit + extra;
                    }

                    return number + " " + unit;
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
