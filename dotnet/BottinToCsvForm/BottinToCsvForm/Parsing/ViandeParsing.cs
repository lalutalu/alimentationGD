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
            List<string> oldCodes = new List<string>();
            bool startAddingProducts = false;
            string currentProductCode = null;

            using (PdfReader reader = new PdfReader(filePath))
            {
                int totalPages = reader.NumberOfPages;
                for (int i = 1; i <= totalPages; i++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
                    foreach (string code in codes)
                    {
                        if (pageText.ToUpper().Contains("BOTTIN") && pageText.Contains(code))
                        {
                            currentProductCode = code;
                            oldCodes.Add(code);
                            //codes.Remove(currentProductCode);
                            startAddingProducts = true;
                        }
                        if (startAddingProducts)
                        {
                            foreach (String line in LineSplitter.SplitLinesViande(pageText, currentProductCode, oldCodes))
                            {
                                dataObjects.Add(line);
                            }
                            startAddingProducts = false;
                            currentProductCode = null;
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
            product.Nom = ParseNames(dataString).Replace("é", "e").Replace("â", "a").Replace("à", "a").
                Replace("ô", "o").Replace("ê", "e").Replace("è", "e").Replace("û", "u").Replace("ù", "u").ToUpper();
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
            return "Pas de Code39";
        }

        public string ParseNames(string initialString)
        {
            string pattern = @"(?<=^\d+\s+\d+\s+)(.*?)(?=\s+\d+K)";
            string pattern1 = @"^\d+\s+\d+\s+(.*?)(?=\s+\d{3,}g\s+\d+\s+\d+\.\d{2}\s+\d+\.\d{2}\s+\d+\.\d{2})";
            string pattern2 = @"^\d+\s+\d+\s+(.*?)(?=\s+\d+(\.\d+)?(kg|g|ml)\s+\d+\s+\d+\.\d{2}\s+\d+\.\d{2}\s+\d+\.\d{2})";
            string pattern3 = @"^(\d{8})\s+(\d{10,11})?\s*(.+)\b[a-zA-z]{0,12}(?=((\d+x)?\d+(g|mg|kg|ml|l|k)))";

            Match match = Regex.Match(initialString, pattern);
            Match match1 = Regex.Match(initialString, pattern1);
            Match match2 = Regex.Match(initialString, pattern2);
            Match match3 = Regex.Match(initialString, pattern3, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            else
            {
                if (match1.Success)
                {
                    return match1.Groups[1].Value.Trim();
                }
                if (match2.Success)
                {
                    return match2.Groups[1].Value.Trim();
                }
                if (match3.Success)
                {
                    return match3.Groups[3].Value.Trim();
                }
            }
            return "No Name";
        }

        public string ParseUnit(string initialString)
        {
            string[] patterns = {
                @"\d{7}\s.*?(?=\s\d{1,2}\s\d+)\s\d+\s((\d+x)*\d+(\.\d+)?)\s*(G|g|l|L|KG|K|UN|U|ML|M|PQ)\b",
                @"((\d+X)*\d+(\.\d+)?)\s*(G|g|l|L|KG|K|UN|U|ML|M|PQ)\b",
                @"^(\d{8})\s+(\d{10,11})?\s*(.+)\b[a-zA-z]{0,12}(?=((\d+x)?\d+(g|mg|kg|ml|l|k)))"
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

                    // NO UNIT fix
                    if (match.Groups.Count == 7)
                    {
                        string unitMeasure = match.Groups[4].Value;
                        return unitMeasure;
                    }

                    return number + " " + unit;
                }
            }
            return "NO UNIT";
        }

        public double ParsePrix(string dataString)
        {
            string pattern = @"\d+\.\d{2}(?=\s+\d+\.\d{2}\s*$)";
            Match match = Regex.Match(dataString, pattern);

            if (match.Success)
            {
                var price = double.Parse(match.Value);
                return Math.Round(price / (1 - 0.20), 2);
            }

            return 0;
        }
    }
}
