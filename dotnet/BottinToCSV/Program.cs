using BottinToCSV;

public class PdfParser
{
    public static void Main(string[] args)
    {
        string filePath = "D:\\BottinToCSV\\files\\bottin.pdf";
        PdfDataParsing pdfDataParsing = new PdfDataParsing();
        try
        {
            // Parse the PDF and extract information
            List<string> parsedData = PdfDataParsing.ParsePdf(filePath);

            // Process or store the parsed data as needed
            int counter = 0;
            //Console.WriteLine($"Extracted data from {parsedData.Count} pages.");
            foreach (var data in parsedData)
            {
                //Console.WriteLine(data);

                // Isolate product names using regex
                List<string> productNames = PdfDataParsing.ParseProductName(data);
                foreach (string name in productNames)
                {
                    Console.WriteLine($"Product Name: {name}");
                }

                counter++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing PDF: {ex.Message}");
        }

    }
}
