using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using OSS.Data.Repository;
using OSS.Services.Services;
using Moq;
using OSS.Core.Models;
using FluentAssertions;

namespace OSSApi.Tests {
    [TestClass]
    public class ProductTests {
        // -------------------------------------------------------------------------------
        private readonly ProductService productService;
        private readonly Mock<IProductRepository> mock = new Mock<IProductRepository>();

        public ProductTests() {
            this.productService = new ProductService(mock.Object);
        }
        // -------------------------------------------------------------------------------

        // Product_GetById_IfExists
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task Product_GetById_IfExists(int id)
        {
            var mockProduct = new Product
            {
                ProductId = id,
                ProductName = "Teste Name",
                ProductPrice = 10.99m,
                ProductSize = "XXL",
                ProductColor = "Black",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book."
            };

            mock.Setup(x =>
                x.GetByIdAsync(id))
                .ReturnsAsync(mockProduct);

            Product product = await productService.GetByIdAsync(id);
            mockProduct.ProductId.Should().Be(product.ProductId);
        }

        // Product_GetById_NullWhenNoProduct
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public async Task Product_GetById_NullWhenNoProduct(int id)
        {
            mock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(() => null).Verifiable();
            var product = await productService.GetByIdAsync(id);
            mock.Verify();
        }

        // Product_Insert
        [TestMethod]
        public async Task Product_Insert()
        {
            var product = new Product
            {
                ProductName = "Teste Name",
                ProductPrice = 10.99m,
                ProductSize = "XXL",
                ProductColor = "Black",
                ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book."
            };
            mock.Setup(x => x.InsertAsync(product)).Verifiable();
            await productService.InsertAsync(product);
            mock.Verify();
        }
    }
}
