using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCsvForm.Parsing
{
    public class PdfDataParsing
    {
        private LineSplitter _lineSplitter;
        public static List<string> ParsePdf(string filePath)
        {
            List<string> dataObjects = new List<string>();
            bool startAddingProducts = false;

            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;
                for (int i = 1; i <= totalPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);

                    if (pageText.ToUpper().Contains("BOTTIN DE COMMANDE"))
                    {
                        startAddingProducts = true;
                    }

                    if (startAddingProducts)
                    {
                        List<string> lines = LineSplitter.SplitLines(pageText);
                        foreach (String line in lines)
                        {
                            dataObjects.Add(line);
                        }
                    }
                }
            }
            return dataObjects;
        }

        //public static List<string> ParsePdfDelete(string filePath)
        //{
        //    List<string> dataObjects = new List<string>();
        //    using (PdfReader reader = new PdfReader(filePath))
        //    {
        //        int totalPages = reader.NumberOfPages;
        //        Console.WriteLine("Entrez le numéro de la page de PRODUITS ENLEVES: ");
        //        int startDelete = int.Parse(Console.ReadLine());
        //        Console.WriteLine("Entrez le numéro de la dernière page de PRODUITS ENLEVES: ");
        //        int endDelete = int.Parse(Console.ReadLine());
        //        for (int i = startDelete; i <= endDelete; i++)
        //        {
        //            string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
        //            List<string> lines = LineSplitter.SplitLines(pageText);
        //            List<string> linesToProcess = new List<string>(lines);
        //            foreach (string line in linesToProcess)
        //            {
        //                dataObjects.Add(line);
        //                if (line.Contains(@"PRODUITS RETIRES TEMPORAIREMENT\r\n"))
        //                {
        //                    Console.WriteLine(dataObjects.Count);
        //                    return dataObjects;
        //                };
        //            }
        //        }
        //    }
        //    return dataObjects;
        //}
        public Product ParseProducts(string dataString)
        {
            Product product = new Product();
            string code = ParseCode39(dataString);
            product.Code39 = code.PadLeft(7, '0');
            product.Nom = ParseNames(dataString);
            product.Quantite = ParseQuantity(dataString);
            product.Taxes = LineSplitter.GetProductTaxes(dataString);
            product.Format = ParseUnit(dataString);
            product.Prix = ParsePrix(dataString);
            List<string> categories = AssignCategories(product.Nom.ToLower(), product.Taxes);
            product.Categories = categories.ToList();
            return product;
        }

        private bool IsCigarette(string nom)
        {
            string[] cigaretteWords = {
                "tabac",
                "copenhagen",
                "skoal",
                "cig",
                "itsa",
                "butane",
                "briquet",
                "medico",
                "david ross",
                "export",
                "macdonald",
                "seville",
                "lighter",
                "clipper",
                "geomet",
                "cone"
            };
            foreach (string cigaretteWord in cigaretteWords)
            {
                if (nom.Contains(cigaretteWord))
                {
                    return true;
                }
            }
            return false;
        }

        private List<string> AssignCategories(string nom, string taxes)
        {
            bool isCigarette = IsCigarette(nom);
            var categories = new List<string>();
            if (isCigarette)
            {
                categories.Add("Cigarette");
            }
            else if (taxes != "NoTaxes" && isCigarette == false)
            {
                categories.Add("Produits Secs");
                categories.Add("Taxes");
            }
            if (categories.Count == 0)
            {
                categories.Add("Produits Secs");
            }
            return categories;

        }
        public string ParseCode39(string InitialString)
        {
            string pattern = @"\d{7}";
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
                return match.Groups[1].Value.Replace(",", ".");
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

        //public string GetTaxes(string dataString)
        //{
        //    string taxes = LineSplitter.GetProductTaxes(dataString);
        //    return taxes;
        //}
        public double ParsePrix(string dataString)
        {
            string pattern = @"\d{1,6}$";
            Match match = Regex.Match(dataString, pattern);

            if (match.Success)
            {
                var newPrice = double.Parse(match.Value) / 100;
                return Math.Round(newPrice / (1 - 0.13), 2);
            }
            string pattern1 = @"\d{1,6}";
            MatchCollection matches = Regex.Matches(dataString, pattern1);
            if (matches.Count > 0)
            {
                Match lastMatch = matches[matches.Count - 1];
                var newPrice = double.Parse(lastMatch.Value) / 100;
                return Math.Round(newPrice / (1 - 0.13), 2);
            }
            return 0;
        }

        //public double CalculateNewPrice(double initialPrice, double taxes)
        //{
        //    double newPrice = Math.Round(initialPrice / 0.87, 2);
        //    double taxPercentage = newPrice * taxes;
        //    return newPrice + taxPercentage;
        //}
    }
}
