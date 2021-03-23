using System;
using System.Collections.Generic;

namespace OSS.Core.Models {
    public class OrderDetails {
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailsQuantity { get; set; }
        public decimal OrderDetailsPrice { get; set; }
    }
}
