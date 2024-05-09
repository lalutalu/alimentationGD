using Microsoft.VisualBasic.FileIO;

namespace BottinToCSV
{
    public class CSVFile
    {
        public List<Product> ReadCSVFile()
        {
            string filePath = "C:\\Users\\lalutalu\\Desktop\\last_week.csv";
            List<Product> products = new List<Product>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist");
                return products;
            }

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                if (!parser.EndOfData)
                    parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length >= 10)
                    {
                        Product product = new Product
                        {
                            HandleID = fields[0],
                            Nom = fields[2],
                            Quantite = fields[3],
                            Code39 = fields[6],
                            Prix = double.Parse(fields[8])
                        };
                        products.Add(product);
                    }
                }
                return products;
            }
        }

        public static void UpdatePrices(List<Product> old_products, List<Product> new_products)
        {
            foreach (var oldProduct in old_products)
            {
                Product newProduct = new_products.FirstOrDefault(p => p.Code39 == oldProduct.Code39);
                if (newProduct != null)
                {
                    oldProduct.Prix = newProduct.Prix;
                }
            }
        }
        public static void DeleteProducts(List<Product> old_products, List<Product> products_deleted)
        {
            List<Product> productsToKeep = old_products.Where(oldProduct =>
            {
                Product newProduct = products_deleted.FirstOrDefault(p => p.Code39 == oldProduct.Code39);
                return newProduct == null;
            }).ToList();
            old_products.Clear();
            old_products.AddRange(productsToKeep);
        }
    }
}
