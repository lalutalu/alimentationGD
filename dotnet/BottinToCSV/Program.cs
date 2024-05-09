using BottinToCSV;

public class PdfParser
{
    public static void Main(string[] args)
    {
        string filePath = "C:\\Users\\lalutalu\\dev\\alimentationgd\\dotnet\\BottinToCSV\\files\\bottin.pdf";
        PdfDataParsing pdfDataParsing = new PdfDataParsing();
        FileCreation fileCreation = new FileCreation();
        CSVFile file = new CSVFile();
        int counter = 0;
        List<string> parsedData = PdfDataParsing.ParsePdf(filePath);
        List<string> deleteData = PdfDataParsing.ParsePdfDelete(filePath);
        try
        {
            if (parsedData.Count > 1)
            {
                Console.WriteLine("Warning: ParsePdf returned multiple records. Using only the first page.");
            }

            List<Product> productsToBeDeleted = new List<Product>();
            foreach (var datastring in deleteData)
            {
                Product product = pdfDataParsing.ParseProducts(datastring);
                productsToBeDeleted.Add(product);
            }

            List<Product> products = new List<Product>();
            foreach (var datastring in parsedData)
            {
                counter++;
                Product product = pdfDataParsing.ParseProducts(datastring);
                product.HandleID = $"Produit_{counter}";
                products.Add(product);

            }
            CSVFile.DeleteProducts(products, productsToBeDeleted);
            fileCreation.CreateFile(products);
            Console.WriteLine("Produits.csv crée sur le bureau!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing PDF: {ex.Message}");
        }
    }
}
