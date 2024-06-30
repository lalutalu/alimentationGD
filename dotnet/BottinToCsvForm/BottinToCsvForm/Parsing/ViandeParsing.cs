using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCsvForm.Parsing
{
    public class ViandeParsing
    {
        private LineSplitter _lineSplitter;
        public static List<string> ParsePdf(string filePath, List<string> codes)
        {
            List<string> dataObjects = new List<string>();
            bool startAddingProducts = false;
            string currentProductCode = null;

            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;
                for (int i = 1; i <= totalPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);

                    if (pageText.ToUpper().Contains("BOTTIN") && codes.Any(code => pageText.Contains(code)))
                    {
                        currentProductCode = codes.FirstOrDefault(code => pageText.Contains(code));
                        codes.Remove(currentProductCode);
                        startAddingProducts = true;
                    }
                    if (startAddingProducts)
                    {
                        foreach (String line in LineSplitter.SplitLinesViande(pageText, currentProductCode))
                        {
                            dataObjects.Add(line);
                        }
                        if (codes.Count == 0)
                        {
                            break;
                        }
                    }
                }
            }
            return dataObjects;
        }

        public Product ParseViande(string dataString)
        {
            Product product = new Product();
            string code = ParseCode39(dataString);
            List<string> categories = new List<string>();
            categories.Add("Viande");
            product.Code39 = code.PadLeft(8, '0');
            product.Nom = ParseNames(dataString);
            product.Taxes = LineSplitter.GetProductTaxes(dataString);
            product.Format = ParseUnit(dataString);
            product.Categories = categories;
            product.Prix = ParsePrix(dataString);
            return product;
        }

        public string ParseCode39(string InitialString)
        {
            string pattern = @"\d{8}";
            Match match = Regex.Match(InitialString, pattern);
            if (match.Success)
            {
                return match.Value;
            }
            return "";
        }


        public string ParseNames(string initialString)
        {
            string pattern = @"(?<=^\d+\s+\d+\s+)(.*?)(?=\s+\d+K)"; // Capture between digits and \d+K
            Match match = Regex.Match(initialString, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim(); // Capture group 1 and trim whitespace
            }
            return "";
        }

        public string ParseUnit(string initialString)
        {
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
            string pattern = @"\d+(?=\s+\d+$)";
            Match match = Regex.Match(dataString, pattern);

            if (match.Success)
            {
                var price = double.Parse(match.Value) / 100;
                return Math.Round(price / (1 - 0.20), 2);
            }

            return 0;
        }
    }
}
