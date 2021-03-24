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
    public class OrderDetailsController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public OrderDetailsController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var data = await unitOfWork.OrderDetails.GetAll();
            return Ok(data);
        }

        // Post OrderDetails
        [HttpPost]
        public async Task<IActionResult> Insert(OrderDetails orderDetails) {
            await unitOfWork.OrderDetails.Insert(orderDetails);
            return Ok();
        }
    }
}
