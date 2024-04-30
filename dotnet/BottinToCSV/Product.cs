﻿namespace BottinToCSV
{
    public class Product
    {
        public string Code39 { get; set; }
        public string HandleID { get; set; }
        public string fieldType { get; set; }
        public string Inventory { get; set; }
        public string Visible { get; set; }
        public string DiscountMode { get; set; }
        public string Nom { get; set; }
        public string Quantite { get; set; }
        public string Format { get; set; }
        public double Prix { get; set; }
        public Product()
        {
            fieldType = "Product";
            Visible = "true";
            DiscountMode = "PERCENT";
            Inventory = "InStock";
        }
    }


}
