using System;
using Xunit;
using OSS.Services.Services;
using OSS.Data.Repository;
using OSS.Core.Models;
using Moq;
using System.Threading.Tasks;

namespace ProductApiMoq {
    public class ProductServiceTest {
        private readonly ProductService productService;
        private readonly Mock<IProductRepository> mock = new Mock<IProductRepository>();

        public ProductServiceTest() {
            this.productService = new ProductService(mock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenCustomerExists() {
            Random r = new Random();
            var productId = r.Next(0, 20);
            var productMock = new Product {
                ProductId = productId,
                ProductName = "Teste Name",
                ProductPrice = 10.99m,
                ProductSize = "XXL",
                ProductColor = "Black",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book."
            };
            mock.Setup(x =>
                x.GetByIdAsync(productId))
                .ReturnsAsync(productMock);
            var product = await productService.GetByIdAsync(productId);
            Assert.Equal(productMock, product);
        }
    }
}
