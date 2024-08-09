using NPOI.HSSF.Extractor;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GetCoordinates
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\lalutalu\\dev\\alimentationGD\\files\\fichier tous les produits.xls";
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var extractor = new OldExcelExtractor(fs);
                    string text = extractor.Text;
                    string result = SeparateStringIntoProductStrings(text);
                    Console.Write(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        public static string SeparateStringIntoProductStrings(string initialString)
        {
            var lines = initialString.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new System.Text.StringBuilder();

            var regex = new Regex(@"^\d{7,12}");
            var currentProduct = new System.Text.StringBuilder();
            bool skipLines = false;

            foreach (var line in lines)
            {
                if (line.Contains("Alimentation G.D"))
                {
                    skipLines = true;
                    currentProduct.Clear();
                    continue;
                }

                if (regex.IsMatch(line))
                {
                    skipLines = false;

                    if (currentProduct.Length > 0)
                    {
                        result.Append(currentProduct.ToString().Trim()).Append("\n");
                        currentProduct.Clear();
                    }
                }

                if (!skipLines)
                {
                    currentProduct.Append(line).Append(" ");
                }
            }

            if (currentProduct.Length > 0)
            {
                result.Append(currentProduct.ToString().Trim()).Append("\n");
            }

            return result.ToString();
        }
    }
}