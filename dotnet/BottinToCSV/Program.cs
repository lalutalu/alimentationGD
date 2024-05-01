using BottinToCSV;

public class PdfParser
{
    public static void Main(string[] args)
    {
        string filePath = "C:\\Users\\lalutalu\\Desktop\\work\\alimentationgd\\dotnet\\BottinToCSV\\files\\bottin.pdf";
        PdfDataParsing pdfDataParsing = new PdfDataParsing();
        FileCreation fileCreation = new FileCreation();
        CSVFile file = new CSVFile();
        int counter = 0;
        List<Product> old_products = new List<Product>();
        old_products = file.ReadCSVFile();
        Console.WriteLine(old_products.Count);
        try
        {
            List<string> parsedData = PdfDataParsing.ParsePdf(filePath);

            if (parsedData.Count > 1)
            {
                Console.WriteLine("Warning: ParsePdf returned multiple records. Using only the first page.");
            }

            List<Product> products = new List<Product>();
            foreach (var datastring in parsedData)
            {
                counter++;
                // Console.WriteLine(dataString);
                Product product = pdfDataParsing.ParseProducts(datastring);
                product.HandleID = $"Produit_{counter}";
                // Console.WriteLine($"HANDLE_ID: {product.HandleID}, Code39: {product.Code39}, Name: {product.Nom}, Quantity: {product.Quantite}, Unit: {product.Format}, Price: {product.Prix}");
                products.Add(product);

            }
            CSVFile.UpdatePrices(old_products, products);
            fileCreation.CreateFile(old_products);
            Console.WriteLine("Produits.csv crée sur le bureau!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing PDF: {ex.Message}");
        }
    }
}
