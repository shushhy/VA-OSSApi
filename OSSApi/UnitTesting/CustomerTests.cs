using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using OSS.Data.Repository;
using OSS.Services.Services;
using Moq;
using OSS.Core.Models;
using FluentAssertions;

namespace OSSApi.Tests {
    [TestClass]
    public class CustomerTests {
        // -------------------------------------------------------------------------------
        private readonly CustomerService customerService;
        private readonly Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();

        public CustomerTests() {
            this.customerService = new CustomerService(mock.Object);
        }
        // -------------------------------------------------------------------------------

        // Customer_GetById_IfExists
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task Customer_GetById_IfExists(int id)
        {
            var mockCustomer = new Customer
            {
                CustomerId = id,
                FirstName = "Name",
                LastName = "Surname",
                Email = "teste@gmail.com",
                Password = "testpassword",
                Gender = 'M',
                Country = "Iceland",
                PhoneNumber = "987654321"
            };

            mock.Setup(x =>
                x.GetByIdAsync(id))
                .ReturnsAsync(mockCustomer);

            Customer customer = await customerService.GetByIdAsync(id);
            mockCustomer.CustomerId.Should().Be(customer.CustomerId);
        }
    }
}
