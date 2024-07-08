using ClosedXML.Excel;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace BottinToCsvForm.Parsing
{
    public class CSVDataParsing
    {
        public List<Product> ReadFile(string filePath)
        {
            List<Product> products = new List<Product>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Ce fichier n'existe pas. Veuillez choisir un fichier existant.");
                return products;
            }

            string fileExtension = Path.GetExtension(filePath).ToLower();

            if (fileExtension == ".csv")
            {
                products = ReadCSVFile(filePath);
            }
            else if (fileExtension == ".xlsx")
            {
                products = ReadXLSXFile(filePath);
            }
            else
            {
                MessageBox.Show("Format de fichier non pris en charge. Veuillez choisir un fichier CSV ou XLSX.");
            }

            return products;
        }
        private List<Product> ReadCSVFile(string filePath)
        {
            List<Product> products = new List<Product>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Ce fichier n'existe pas. Veuillez choisir un fichier existant.");
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
                    if (fields.Length == 5)
                    {
                        Product product = new Product
                        {
                            Code39 = fields[0],
                            Nom = fields[1],
                            Quantite = fields[2],
                            Format = fields[3],
                            Prix = double.Parse(fields[5].Replace("$", "")),
                        };
                        products.Add(product);
                    }
                }
                return products;
            }
        }

        private List<Product> ReadXLSXFile(string filePath)
        {
            List<Product> products = new List<Product>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed();
                foreach (var row in rows.Skip(1))
                {
                    List<string> categories = new List<string>();
                    string code39 = row.Cell(1).GetString().PadLeft(7, '0');
                    string nom = row.Cell(2).GetString();
                    string quantite = row.Cell(3).GetString();
                    string format = row.Cell(4).GetString();
                    string formattedString = row.Cell(6).GetString().Replace("$", "").Replace(",", ".").Trim();
                    double prix;
                    if (!double.TryParse(formattedString, NumberStyles.Any, CultureInfo.InvariantCulture, out prix))
                    {
                        MessageBox.Show($"Le prix '{formattedString}' n'est pas dans un format valide.");
                        continue;
                    }

                    if (filePath.Contains("viande"))
                    {
                        if (nom.Contains("COUPER"))
                        {
                            categories.Add("Viande Couper");
                        }
                        else
                        {
                            categories.Add("Viande");
                        }
                    }
                    else if (filePath.Contains("fruit"))
                    {
                        categories.Add("Fruits et Legumes");
                    }
                    else
                    {
                        categories.Add("Produits Secs");
                    }
                    prix = Math.Round(prix, 2);
                    Product product = new Product
                    {
                        Code39 = code39,
                        Nom = nom,
                        Quantite = quantite,
                        Format = format,
                        Prix = prix,
                        Categories = categories
                    };

                    products.Add(product);
                }
            }
            return products;
        }
        public static int UpdatePrices(List<Product> currentProducts, List<Product> fileProducts, int counter)
        {
            foreach (var fileProduct in fileProducts)
            {
                var existingProduct = currentProducts.FirstOrDefault(p => p.Code39 == fileProduct.Code39);
                if (existingProduct != null)
                {
                    existingProduct.Prix = fileProduct.Prix;
                }
                else
                {
                    fileProduct.HandleID = $"Produit_{counter}";
                    counter++;
                    currentProducts.Add(fileProduct);
                }
            }
            return counter;
        }

        public static void DeleteProducts(List<Product> old_products, List<Product> products_deleted)
        {
            HashSet<string> deletedProductCodes = new HashSet<string>(products_deleted.Select(p => p.Code39));

            List<Product> productsToKeep = old_products.Where(p => !deletedProductCodes.Contains(p.Code39)).ToList();

            old_products.Clear();
            old_products.AddRange(productsToKeep);
        }
    }
}
