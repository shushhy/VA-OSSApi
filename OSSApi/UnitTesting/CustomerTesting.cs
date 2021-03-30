using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using OSS.Data.Repository;
using OSS.Services.Services;
using Moq;
using OSS.Core.Models;
using FluentAssertions;

namespace UnitTesting {
    [TestClass]
    public class CustomerTesting {
        private readonly CustomerService customerService;
        private readonly Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();

        public CustomerTesting() {
            this.customerService = new CustomerService(mock.Object);
        }
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(-1)]
        public async Task GetByIdAsyncIfTrue(int id) {
            var mockCustomer = new Customer {
                CustomerId = id,
                FirstName = "Teste Name",
                LastName = "Test Surname",
                Email = "teste@teste.gmail.com",
                Password = "testpassword",
                Gender = 'M',
                Country = "Iceland",
                PhoneNumber = "987654321"
            };
            Customer customer = await customerService.GetByIdAsync(id);
            mockCustomer.CustomerId.Should().Be(customer.CustomerId);
        }
    }
}
