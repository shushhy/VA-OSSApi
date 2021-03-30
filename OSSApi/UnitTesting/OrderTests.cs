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

        // Order_GetById_Exists
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task Order_GetById_Exists(int id)
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

        // Order_GetById_NullWhenNoOrder
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task Order_GetById_NullWhenNoOrder(int id)
        {
            mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(() => null).Verifiable();
            var order = await orderService.GetByIdAsync(id);
            mock.Verify();
        }

        // Order_Insert
        [TestMethod]
        public async Task Order_Insert()
        {
            var order = new Orders
            {
                CustomerId = 1,
                OrderDescription = "No description.",
                OrderStatus = 'P',
                OrderDate = DateTime.UtcNow
            };
            mock.Setup(x => x.InsertAsync(order)).Verifiable();
            await orderService.InsertAsync(order);
            mock.Verify();
        }
    }
}
