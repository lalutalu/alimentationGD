namespace BottinToCsvForm.Parsing
{
    public class FileCreation
    {
        String newFilePath = @"C:\Users\lalutalu\Desktop\Produits.csv";
        String separator = ",";
        String[] headings = { "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible",
            "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2",
            "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4",
            "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6",
            "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2",
            "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5",
            "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2",
            "customTextMandatory2", "brand" };

        public void CreateFile(List<Product> products)
        {
            using (var writer = new StreamWriter(newFilePath))
            {
                writer.WriteLine(string.Join(",", headings));
                foreach (var product in products)
                {
                    string handleId = product.HandleID + ",";
                    string fieldType = product.fieldType + ",";
                    string name = product.Nom + ",";
                    string description = $"{product.Quantite}" + "X" + $"{product.Format}";
                    string productImageUrl = "" + ",";
                    string collection = "metro" + ",";
                    string sku = product.Code39 + ",";
                    string ribbon = "" + ",";
                    string price = product.Prix.ToString() + ",";
                    string surcharge = "" + ",";
                    string visible = product.Visible + ",";
                    string discountMode = product.DiscountMode + ",";
                    string discountValue = "0" + ",";
                    string inventory = product.Inventory + ",";
                    string weight = "" + ",";
                    string cost = "" + ",";
                    string productOptionName1 = "" + ",";
                    string productOptionType1 = "" + ",";
                    string productOptionDescription1 = "" + ",";
                    string productOptionName2 = "" + ",";
                    string productOptionType2 = "" + ",";
                    string productOptionDescription2 = "" + ",";
                    string productOptionName3 = "" + ",";
                    string productOptionType3 = "" + ",";
                    string productOptionDescription3 = "" + ",";
                    string productOptionName4 = "" + ",";
                    string productOptionType4 = "" + ",";
                    string productOptionDescription4 = "" + ",";
                    string productOptionName5 = "" + ",";
                    string productOptionType5 = "" + ",";
                    string productOptionDescription5 = "" + ",";
                    string productOptionName6 = "" + ",";
                    string productOptionType6 = "" + ",";
                    string productOptionDescription6 = "" + ",";
                    string additionalInfoTitle1 = "" + ",";
                    string additionalInfoDescription1 = "" + ",";
                    string additionalInfoTitle2 = "" + ",";
                    string additionalInfoDescription2 = "" + ",";
                    string additionalInfoTitle3 = "" + ",";
                    string additionalInfoDescription3 = "" + ",";
                    string additionalInfoTitle4 = "" + ",";
                    string additionalInfoDescription4 = "" + ",";
                    string additionalInfoTitle5 = "" + ",";
                    string additionalInfoDescription5 = "" + ",";
                    string additionalInfoTitle6 = "" + ",";
                    string additionalInfoDescription6 = "" + ",";
                    string customTextField1 = "" + ",";
                    string customTextCharLimit1 = "" + ",";
                    string customTextMandatory1 = "" + ",";
                    string customTextField2 = "" + ",";
                    string customTextCharLimit2 = "" + ",";
                    string customTextMandatory2 = "" + ",";
                    string brand = "" + ",";
                    string productRow = $"{handleId}{fieldType}{name}{description}{productImageUrl}{collection}{sku}{ribbon}{price}{surcharge}{visible}{discountMode}{discountValue}{inventory}" +
                        $"{weight}{cost}{productOptionName1}{productOptionType1}{productOptionDescription1}{productOptionName2}{productOptionType2}{productOptionDescription2}{productOptionName3}{productOptionType3}{productOptionDescription3}" +
                        $"{productOptionName4}{productOptionType4}{productOptionDescription4}{productOptionName5}{productOptionType5}{productOptionDescription5}{productOptionName6}{productOptionType6}{productOptionDescription6}" +
                        $"{additionalInfoTitle1}{additionalInfoDescription1}{additionalInfoTitle2}{additionalInfoDescription2}{additionalInfoTitle3}{additionalInfoDescription3}{additionalInfoTitle4}{additionalInfoDescription4}" +
                        $"{additionalInfoTitle5}{additionalInfoDescription5}{additionalInfoTitle6}{additionalInfoDescription6}{customTextField1}{customTextCharLimit1}{customTextMandatory1}{customTextField2}{customTextCharLimit2}{customTextMandatory2}" +
                        $"{brand}";
                    writer.WriteLine(productRow);
                }
                writer.WriteLine();
            }
        }
    }

}
