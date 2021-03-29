using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Services.Services;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public ProductController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        // GetAll Product
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var data = await unitOfWork.Products.GetAllAsync();
            return Ok(data);
        }

        // GetById Product
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) {
            var data = await unitOfWork.Products.GetByIdAsync(id);
            if (data == null) {
                return BadRequest();
            }
            return Ok(data);
        }

        // Post Product
        [HttpPost]
        public async Task<IActionResult> InsertAsync(Product product) {
            await unitOfWork.Products.InsertAsync(product);
            return Ok();
        }

        // Put Product
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Product product) {
            await unitOfWork.Products.UpdateAsync(product);
            return Ok();
        }

        // Delete Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            await unitOfWork.Products.DeleteAsync(id);
            return Ok();
        }
    }
}
