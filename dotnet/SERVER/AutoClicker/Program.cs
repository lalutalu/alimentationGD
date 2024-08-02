using NPOI.HSSF.Extractor;
using System;
using System.IO;

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

                    //string processedText = Regex.Replace(text, @"\r?\n", " ");
                    //processedText = processedText.Trim();
                    //Console.WriteLine(processedText);
                    Console.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        public string SeperateStringIntoProductStrings(string initialString)
        {
            return "";
        }
    }
}