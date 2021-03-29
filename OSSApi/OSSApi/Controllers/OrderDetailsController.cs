using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OSS.Core.Models;
using OSS.Services.Services;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase {
        // private readonly IUnitOfWork unitOfWork;
        // public OrderDetailsController(IUnitOfWork unitOfWork) {
        //     this.unitOfWork = unitOfWork;
        // }

        // [HttpGet]
        // public async Task<IActionResult> GetAllAsync() {
        //     var data = await unitOfWork.OrderDetails.GetAllAsync();
        //     return Ok(data);
        // }

        // // Post OrderDetails
        // [HttpPost]
        // public async Task<IActionResult> InsertAsync(OrderDetails orderDetails) {
        //     await unitOfWork.OrderDetails.InsertAsync(orderDetails);
        //     return Ok();
        // }
    }
}
