using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace OSS.Core.Models {
    [Table("OrderDetails")]
    public class OrderDetails {

        [Key]
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailsQuantity { get; set; }
        public decimal OrderDetailsPrice { get; set; }
    }
}
