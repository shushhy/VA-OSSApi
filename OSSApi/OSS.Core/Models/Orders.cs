using System;

namespace OSS.Core.Models {
    public class Orders {
        public Int64 order_id { get; set; }
        public Int32 customer_id { get; set; }
        public string order_details { get; set; }
        public char order_status { get; set; }
    }
}
