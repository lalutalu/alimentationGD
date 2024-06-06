namespace BottinToCsvForm.Parsing
{
    public class FileCreation
    {
        private string newFilePath;
        String separator = ",";
        String[] headings = { "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible",
            "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2",
            "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4",
            "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6",
            "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2",
            "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5",
            "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2",
            "customTextMandatory2", "brand" };

        public FileCreation()
        {
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            newFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Produits_{dateString}.csv");
        }

        public string CreateFile(List<Product> products)
        {
            using (var writer = new StreamWriter(newFilePath))
            {
                writer.WriteLine(string.Join(separator, headings));
                foreach (var product in products)
                {
                    string description = $"{product.Quantite}X{product.Format}";
                    string[] productValues = {
                        product.HandleID,
                        product.fieldType,
                        product.Nom,
                        description,
                        "",  // productImageUrl
                        product.Category,
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
