using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OSS.Core.Models {
    public class Orders {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderDescription { get; set; }
        public char OrderStatus { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
