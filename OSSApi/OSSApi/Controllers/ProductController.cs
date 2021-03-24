using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSS.Data.Repository;
using OSS.Core.Models;

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
        public async Task<IActionResult> GetAll() {
            var data = await unitOfWork.Products.GetAll();
            return Ok(data);
        }

        // GetById Product
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var data = await unitOfWork.Products.GetById(id);
            if (data == null) {
                return BadRequest();
            }
            return Ok(data);
        }

        // Post Product
        [HttpPost]
        public async Task<IActionResult> Insert(Product product) {
            await unitOfWork.Products.Insert(product);
            return Ok();
        }

        // Put Product
        [HttpPut]
        public async Task<IActionResult> Update(Product product) {
            await unitOfWork.Products.Update(product);
            return Ok();
        }

        // Delete Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await unitOfWork.Products.Delete(id);
            return Ok();
        }
    }
}
