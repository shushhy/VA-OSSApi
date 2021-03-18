using System;

namespace OSS.Core.Models {
    public class Customer {

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public char Gender { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }
}
