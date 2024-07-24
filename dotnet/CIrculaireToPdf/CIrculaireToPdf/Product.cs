using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIrculaireToPdf
{
    public class Product
    {
        public string Name { get; set; }
        public double OriginalPrice { get; set; }
        public double NewPrice { get; set; }
        public string Code39 { get; set; }
        public string Weight { get; set; }
        public string Unit{ get; set; }
        public string Quantity { get; set; }
    }
}
