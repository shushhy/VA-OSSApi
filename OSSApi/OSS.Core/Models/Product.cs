using System;

namespace OSS.Core.Models {
    public class Product {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public string ProductDescription { get; set; }
    }
}
