using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSS.Data.Repository;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var data = await unitOfWork.Customers.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var data = await unitOfWork.Customers.GetById(id);
            if (data == null) {
                return BadRequest();
            }
            return Ok(data);
        }
    }
}
