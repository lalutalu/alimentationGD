using BottinToCSV;

public class PdfParser
{
    public static void Main(string[] args)
    {
        string filePath = "C:\\Users\\lalutalu\\Desktop\\work\\alimentationgd\\dotnet\\BottinToCSV\\files\\bottin.pdf";
        PdfDataParsing pdfDataParsing = new PdfDataParsing();
        FileCreation fileCreation = new FileCreation();
        int counter = 0;
        try
        {
            // Parse the PDF and extract information
            List<string> parsedData = PdfDataParsing.ParsePdf(filePath);

            if (parsedData.Count > 1)
            {
                Console.WriteLine("Warning: ParsePdf returned multiple records. Using only the first page.");
            }

            List<Product> products = new List<Product>();
            foreach (var dataString in parsedData) // Iterate through each data string
            {
                counter++;
                Product product = pdfDataParsing.ParseProducts(dataString);
                product.HandleID = $"Produit_{counter}";
                Console.WriteLine($"HANDLE_ID: {product.HandleID},Code39: {product.Code39}, Name: {product.Nom}, Quantity: {product.Quantite}, Unit: {product.Format}, Price: {product.Prix}");
                products.Add(product);
            }

            fileCreation.CreateFile(products);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing PDF: {ex.Message}");
        }
    }
}
