using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace BottinToCsvForm.Parsing
{
    public class ViandeParsing
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

                    if (pageText.ToUpper().Contains("BOTTIN"))
                    {
                        startAddingProducts = true;
                    }

                    if (startAddingProducts)
                    {
                        List<string> lines = LineSplitter.SplitLinesViande(pageText);
                        foreach (String line in lines)
                        {
                            dataObjects.Add(line);
                        }
                    }
                }
            }
            return dataObjects;
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


    }
}
