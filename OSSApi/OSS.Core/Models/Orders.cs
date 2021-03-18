using System;

namespace OSS.Core.Models {
    public class Orders {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderDetails { get; set; }
        public char OrderStatus { get; set; }
    }
}
