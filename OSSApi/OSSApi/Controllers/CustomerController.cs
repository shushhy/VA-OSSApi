using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Services.Services;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        // Customer GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var data = await unitOfWork.Customers.GetAllAsync();
            return Ok(data);
        }

        // Customer GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var data = await unitOfWork.Customers.GetByIdAsync(id);
            if (data == null) {
                return BadRequest();
            }
            return Ok(data);
        }

        // Customer Post
        [HttpPost]
        public async Task<IActionResult> Insert(Customer customer) {
            await unitOfWork.Customers.InsertAsync(customer);
            return Ok();
        }

        // Customer Put
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer) {
            await unitOfWork.Customers.UpdateAsync(customer);
            return Ok();
        }

        // Customer Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            await unitOfWork.Customers.DeleteAsync(id);
            return Ok();
        }
    }
}
