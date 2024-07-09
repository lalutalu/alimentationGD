namespace BottinToCsvForm.Parsing
{
    public class CSVFileCreation
    {
        private string newFilePath;
        private string dateString = DateTime.Now.ToString("yyyy-MM-dd");
        private int fileCounter = 0;
        string baseFileName = "";
        private string folderPath = "";
        private int productCounter = 0;
        String separator = ",";
        String[] headings = { "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible",
            "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2",
            "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4",
            "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6",
            "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2",
            "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5",
            "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2",
            "customTextMandatory2", "brand" };

        public CSVFileCreation(string bottin_Nom)
        {
            baseFileName = Path.GetFileNameWithoutExtension(bottin_Nom);
            folderPath = GetUniqueFolderPath($"{baseFileName}  {dateString}");
            Directory.CreateDirectory(folderPath);
            //newFilePath = Path.Combine(folderPath, $"{baseFileName}_{dateString}.csv");
        }

        private string GetUniqueFolderPath(string folderPath)
        {
            int counter = 0;
            bool promptOverwrite = true;
            string desktopFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderPath);
            if (!Directory.Exists(desktopFolderPath))
            {
                return desktopFolderPath;
            }

            while (Directory.Exists(desktopFolderPath))
            {
                if (promptOverwrite)
                {
                    var result = MessageBox.Show($"The folder '{folderPath}' already exists. Do you want to overwrite this folder?", "Existing Folder",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    promptOverwrite = false;

                    if (result == DialogResult.Yes)
                    {
                        Directory.Delete(desktopFolderPath, true);
                        return desktopFolderPath;
                    }
                    counter++;
                    string newFolderName = $"({counter})_{folderPath}";
                    desktopFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), newFolderName);
                }
            }

            return desktopFolderPath;
        }

        private string GetNewFileName()
        {
            fileCounter++;
            newFilePath = "";
            string formattedName = Path.GetFileNameWithoutExtension(baseFileName);
            return Path.Combine(folderPath, $"{fileCounter}_fichier de remplacement.csv");

        }

        public string CreateFile(List<Product> products)

        {
            newFilePath = GetNewFileName();
            using (var writer = new StreamWriter(newFilePath, true))
            {
                if (!File.Exists(newFilePath))
                {
                    writer.WriteLine(string.Join(separator, headings));
                }
                foreach (var product in products)
                {
                    string description = $"{product.Quantite}X{product.Format}";
                    if (product.Quantite == "No Quantity")
                    {
                        description = $"{product.Format}";
                    }
                    string categories = string.Join(";", product.Categories);
                    string[] productValues = {
                        product.HandleID,
                        product.fieldType,
                        product.Nom,
                        description,
                        "",  // productImageUrl
                        categories,
                        product.Code39,
                        "",  // ribbon
                        product.Prix.ToString(),
                        "",  // surcharge
                        product.Visible,
                        product.DiscountMode,
                        "0",  // discountValue
                        product.Inventory,
                        "",  // weight
                        "",  // cost
                        "",  // productOptionName1
                        "",  // productOptionType1
                        "",  // productOptionDescription1
                        "",  // productOptionName2
                        "",  // productOptionType2
                        "",  // productOptionDescription2
                        "",  // productOptionName3
                        "",  // productOptionType3
                        "",  // productOptionDescription3
                        "",  // productOptionName4
                        "",  // productOptionType4
                        "",  // productOptionDescription4
                        "",  // productOptionName5
                        "",  // productOptionType5
                        "",  // productOptionDescription5
                        "",  // productOptionName6
                        "",  // productOptionType6
                        "",  // productOptionDescription6
                        "",  // additionalInfoTitle1
                        "",  // additionalInfoDescription1
                        "",  // additionalInfoTitle2
                        "",  // additionalInfoDescription2
                        "",  // additionalInfoTitle3
                        "",  // additionalInfoDescription3
                        "",  // additionalInfoTitle4
                        "",  // additionalInfoDescription4
                        "",  // additionalInfoTitle5
                        "",  // additionalInfoDescription5
                        "",  // additionalInfoTitle6
                        "",  // additionalInfoDescription6
                        "",  // customTextField1
                        "",  // customTextCharLimit1
                        "",  // customTextMandatory1
                        "",  // customTextField2
                        "",  // customTextCharLimit2
                        "",  // customTextMandatory2
                        ""   // brand
                    };

                    productCounter++;
                    string productRow = string.Join(separator, productValues);
                    writer.WriteLine(productRow);
                }
            }
            return newFilePath;
        }

        // Cette fonction separe les produits en liste de 4999 ou moins. Wix permet seulement 4999 produits importés a la fois.
        public List<List<Product>> SplitListIntoChunks(List<Product> allProducts, int chunkSize)
        {
            List<List<Product>> chunks = new List<List<Product>>();
            List<Product> currentChunk = new List<Product>();

            foreach (Product product in allProducts)
            {
                currentChunk.Add(product);
                if (currentChunk.Count == chunkSize)
                {
                    chunks.Add(currentChunk);
                    currentChunk = new List<Product>();
                }
            }
            if (currentChunk.Count > 0)
            {
                chunks.Add(currentChunk);
            }

            return chunks;
        }
    }
}
