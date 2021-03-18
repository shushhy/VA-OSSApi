using System;

namespace OSS.Core.Models {
    public class Order_Details {
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailsQuantity { get; set; }
        public decimal OrderDetailsPrice { get; set; }
    }
}
