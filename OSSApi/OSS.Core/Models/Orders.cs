using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;


namespace OSS.Core.Models {
    [Table("Orders")]

    public class Orders {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string OrderDescription { get; set; }
        public char OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

        //public OrderDetails OrderDetails { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
