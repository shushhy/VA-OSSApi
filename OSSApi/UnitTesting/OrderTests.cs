using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using OSS.Data.Repository;
using OSS.Services.Services;
using Moq;
using OSS.Core.Models;
using FluentAssertions;
using System;

namespace OSSApi.Tests {
    [TestClass]
    public class OrderTests {
        // -------------------------------------------------------------------------------
        private readonly OrdersService orderService;
        private readonly Mock<IOrdersRepository> mock = new Mock<IOrdersRepository>();

        public OrderTests() {
            this.orderService = new OrdersService(mock.Object);
        }
        // -------------------------------------------------------------------------------

        // Order_GetById_IfExists
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task Order_GetById_IfExists(int id)
        {
            var mockOrder = new Orders
            {
                OrderId = id,
                CustomerId = 1,
                OrderDescription = "No description.",
                OrderStatus = 'P',
                OrderDate = DateTime.UtcNow
            };

            mock.Setup(x =>
                x.GetByIdAsync(id))
                .ReturnsAsync(mockOrder);

            Orders order = await orderService.GetByIdAsync(id);
            mockOrder.OrderId.Should().Be(order.OrderId);
        }
    }
}
