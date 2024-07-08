namespace BottinToCsvForm.Parsing
{
    public class FileCreation
    {
        private string newFilePath;
        private string dateString = DateTime.Now.ToString("yyyy-MM-dd");
        String separator = ",";
        String[] headings = { "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible",
            "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2",
            "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4",
            "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6",
            "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2",
            "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5",
            "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2",
            "customTextMandatory2", "brand" };

        public FileCreation(string bottin_Nom)
        {
            string baseFileName = Path.GetFileNameWithoutExtension(bottin_Nom);
            string nameToCheck = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{baseFileName}_{dateString}.csv");
            newFilePath = GetUniqueFilePath(nameToCheck, bottin_Nom);
        }

        private string GetUniqueFilePath(string filePath, string bottinNom)
        {
            int counter = 0;
            bool promptOverwrite = true;

            if (!File.Exists(filePath))
            {
                return filePath;
            }

            string baseName = Path.GetFileNameWithoutExtension(filePath);

            while (File.Exists(filePath))
            {
                if (promptOverwrite)
                {
                    var result = MessageBox.Show($"Le fichier '{baseName}' existe deja, Voulez-vous écraser ce fichier?", "Fichier Existant",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    promptOverwrite = false;

                    if (result == DialogResult.Yes)
                    {
                        return filePath;
                    }
                }

                counter++;
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"({counter.ToString()})_{baseName}_{dateString}.csv");
            }

            return filePath;
        }
        public string CreateFile(List<Product> products)
        {
            using (var writer = new StreamWriter(newFilePath))
            {
                writer.WriteLine(string.Join(separator, headings));
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

                    string productRow = string.Join(separator, productValues);
                    writer.WriteLine(productRow);
                }
            }
            return newFilePath;
        }
    }
}
