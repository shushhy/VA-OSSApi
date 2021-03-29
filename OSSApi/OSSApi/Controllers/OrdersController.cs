using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Services.Services;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        // GetAll Order
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var data = await unitOfWork.Orders.GetAllAsync();
            return Ok(data);
        }

        // GetById Order
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) {
            var data = await unitOfWork.Orders.GetByIdAsync(id);
            if (data == null) {
                return BadRequest();
            }
            return Ok(data);
        }

        // Post Order
        [HttpPost]
        public async Task<IActionResult> InsertAsync(Orders orders) {
            await unitOfWork.Orders.InsertAsync(orders);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Orders orders) {
            await unitOfWork.Orders.UpdateAsync(orders);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            await unitOfWork.Orders.DeleteAsync(id);
            return Ok();
        }

    }
}
