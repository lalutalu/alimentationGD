using BottinToCSV;

public class PdfParser
{
    public static void Main(string[] args)
    {
        //string filePath = "C:\\Users\\lalutalu\\dev\\alimentationgd\\dotnet\\BottinToCSV\\files\\old_bottin.pdf";
        string filePath;
        do
        {
            Console.WriteLine("Veuillez choisir le chemin du bottin:");
            filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Erreur: Le fichier n'existe pas. Veuillez réessayer: ");
            }
        } while (!File.Exists(filePath));

        PdfDataParsing pdfDataParsing = new PdfDataParsing();
        FileCreation fileCreation = new FileCreation();
        CSVFile file = new CSVFile();
        int counter = 0;
        List<string> parsedData = PdfDataParsing.ParsePdf(filePath);
        List<Product> lastWeek = file.ReadCSVFile();
        List<string> deleteData = PdfDataParsing.ParsePdfDelete(filePath);
        try
        {
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
            CSVFile.UpdatePrices(lastWeek, products);
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
