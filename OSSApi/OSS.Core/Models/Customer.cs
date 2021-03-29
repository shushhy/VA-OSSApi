using Dapper.Contrib.Extensions;

namespace OSS.Core.Models {
    [Table("Customer")]
    public class Customer {

        [Key]
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
