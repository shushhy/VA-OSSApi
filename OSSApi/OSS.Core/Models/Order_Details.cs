using System;

namespace OSS.Core.Models {
    public class Order_Details {
        public Int32 order_details_id { get; set; }
        public Int32 product_id { get; set; }
        public Int64 order_id { get; set; }
        public Int16 order_details_quantity { get; set; }
        public decimal order_details_price { get; set; }
    }
}
