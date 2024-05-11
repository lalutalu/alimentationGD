using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCSV
{
    public class PdfDataParsing
    {
        //public static List<string> ParsePdf(string filePath)
        //{
        //    List<string> dataObjects = new List<string>();
        //    using (PdfReader reader = new PdfReader(filePath))
        //    {
        //        int totalPages = reader.NumberOfPages;
        //        Console.WriteLine("Entrez le numéro de la page avec le titre commande du bottin: ");
        //        int startOfProducts = int.Parse(Console.ReadLine());
        //        for (int i = startOfProducts; i <= totalPages; i++)
        //        {
        //            string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
        //            Console.WriteLine(pageText);
        //            List<string> lines = LineSplitter.SplitLines(pageText);
        //            List<string> linesToProcess = new List<string>(lines);
        //            foreach (string line in linesToProcess)
        //            {
        //                dataObjects.Add(line);
        //            }
        //        }
        //    }
        //    return dataObjects;
        //}

        public static List<string> ParsePdf(string filePath)
{
  List<string> dataObjects = new List<string>();
  bool startAddingProducts = false; // Flag to control product line capturing

  using (PdfReader reader = new PdfReader(filePath))
  {
    int totalPages = reader.NumberOfPages;

    for (int i = 1; i <= totalPages; i++) // Start from page 1
    {
      string pageText = PdfTextExtractor.GetTextFromPage(reader, i);

      // Check if page contains "BOTTIN DE COMMANDE" (case-insensitive)
      if (pageText.ToUpper().Contains("BOTTIN DE COMMANDE"))
      {
        startAddingProducts = true;
        //Console.WriteLine($"Found 'BOTTIN DE COMMANDE' on page {i}");
      }

      if (startAddingProducts)
      {
        List<string> lines = LineSplitter.SplitLines(pageText);
        dataObjects.AddRange(lines);
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
                return Math.Round(newPrice,2);
            }
            string pattern1 = @"\d{1,4}";
            MatchCollection matches = Regex.Matches(dataString, pattern1);
            if (matches.Count > 0) { 
                Match lastMatch = matches[matches.Count - 1];
                Console.WriteLine(lastMatch.Value);
                var newPrice = double.Parse(lastMatch.Value) / 100;
                newPrice = CalculateNewPrice(newPrice);
                return Math.Round(newPrice,2);
            }
            return 0;
        }

        //public double ParsePrix(string dataString)
        //{
        //    // Try with the first pattern for end-of-line prices
        //    //string pattern1 = @"\d{1,4}$";
        //    string pattern1 = @"\d+(?:\*\*)?$";
        //    Match match = Regex.Match(dataString, pattern1);

        //    if (match.Success)
        //    {
        //        var newPrice = double.Parse(match.Value) / 100;
        //        Console.WriteLine(newPrice);
        //        newPrice = CalculateNewPrice(newPrice);
        //        return newPrice;
        //    }
        //    //Console.WriteLine(dataString + ": " + 0);
        //    return 0;
        //}

        //public double ParsePrix(string dataString)
        //{
        //    // Regex to capture digits, optionally followed by "**"
        //    string pattern = @"\d+(?:\*\*)?";
        //    Match match = Regex.Match(dataString, pattern);

        //    if (match.Success)
        //    {
        //        // Extract only the digits from the captured value
        //        string priceString = Regex.Match(match.Value, @"\d+").Value;
        //        Console.WriteLine(priceString);
        //        double price = double.Parse(priceString) / 100;
        //        // Round the price to two decimal places
        //        price = Math.Round(price, 2);
        //        return price;
        //    }

        //    // Handle cases where no match is found (return 0)
        //    return 0;
        //}


        public double CalculateNewPrice(double initialPrice)
        {
            double newPrice = Math.Round(initialPrice / 0.70, 2);
            return newPrice;
        }
    }
}
