using System;
using Xunit;
using OSS.Services.Services;
using OSS.Data.Repository;
using Moq;
using System.Threading.Tasks;

namespace OrdersApiMoq {
    public class OrdersServiceTest {
        private readonly OrdersService ordersService;
        private readonly Mock<IOrdersRepository> mock = new Mock<IOrdersRepository>();

        public OrdersServiceTest() {
            this.ordersService = new OrdersService(mock.Object);
        }
    }
}
