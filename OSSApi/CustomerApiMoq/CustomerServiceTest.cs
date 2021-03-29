using System;
using Xunit;
using OSS.Services.Services;
using OSS.Data.Repository;
using Moq;
using System.Threading.Tasks;
using OSS.Core.Models;
using FluentAssertions;

namespace CustomerApiMoq {
    public class CustomerServiceTest {
        private readonly CustomerService customerService;
        private readonly Mock<ICustomerRepository> mock = new Mock<ICustomerRepository>();

        public CustomerServiceTest() {
            this.customerService = new CustomerService(mock.Object);
        }

        // Xunit Assertions
        [Fact]
        public async Task GetByIdAsyncIfTrue() {
            Random r = new Random();
            var customerId = r.Next(0, 20);
            var customerMock = new Customer {
                CustomerId = customerId,
                FirstName = "Teste Name",
                LastName = "Test Surname",
                Email = "teste@teste.gmail.com",
                Password = "testpassword",
                Gender = 'M',
                Country = "Iceland",
                PhoneNumber = "987654321"
            };
            mock.Setup(x =>
                x.GetByIdAsync(customerId))
                .ReturnsAsync(customerMock);
            var customer = await customerService.GetByIdAsync(customerId);
            Assert.Equal(customerMock, customer);
        }

        [Fact]
        public async Task GetByIdAsyncNullWhenNoCustomer() {
            mock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            var customer = await customerService.GetByIdAsync(654654);
            Assert.Null(customer);
        }

        // FluentAssertions
        // Should return a requested customer if it exists
        [Fact]
        public async Task GetByIdAsyncIfTrueFA() {
            Random r = new Random();
            var customerId = r.Next(0, 20);
            var customerMock = new Customer {
                CustomerId = customerId,
                FirstName = "Teste Name",
                LastName = "Test Surname",
                Email = "teste@teste.gmail.com",
                Password = "testpassword",
                Gender = 'M',
                Country = "Iceland",
                PhoneNumber = "987654321"
            };
            mock.Setup(x =>
                x.GetByIdAsync(customerId))
                .ReturnsAsync(customerMock);
            var customer = await customerService.GetByIdAsync(customerId);
            customer.Should().Be(customerMock);
        }

        // Should return null when the requested customer doesn't exist
        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        [InlineData(-1)]
        public async Task GetByIdAsyncNullWhenNoCustomerFA(int id) {
            var customer = await customerService.GetByIdAsync(id);
            mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(customer);
            customer.Should().BeNull();
        }

        [Fact]
        public async Task InsertAsync() {
            mock.Verify(x => x.InsertAsync(new Customer {
                FirstName = "Test Subject",
                LastName = "Subject Surname",
                Email = "oooooo@hotmail.com",
                Password = "asjhgashd__1283732$@#$2",
                Gender = 'M',
                Country = "Iceland",
                PhoneNumber = "987654321"
            }), Times.Once());
        }
    }
}
