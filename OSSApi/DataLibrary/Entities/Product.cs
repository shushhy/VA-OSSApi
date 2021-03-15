using System;

namespace OSSApi.Models {
    public class Product {
        public Int32 product_id { get; set; }
        public string product_name { get; set; }
        public decimal product_price { get; set; }
        public string product_size { get; set; }
        public string product_color { get; set; }
        public string product_description { get; set; }
    }
}
