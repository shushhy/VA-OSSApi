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
    public class CustomerController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        // Customer GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var data = await unitOfWork.Customers.GetAll();
            return Ok(data);
        }

        // Customer GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var data = await unitOfWork.Customers.GetById(id);
            if (data == null) {
                return BadRequest();
            }
            return Ok(data);
        }

        // Customer Post
        [HttpPost]
        public async Task<IActionResult> Insert(Customer entity) {
            await unitOfWork.Customers.Insert(entity);
            return Ok();
        }

        // Customer Put
        [HttpPut]
        public async Task<IActionResult> Update(Customer entity) {
            await unitOfWork.Customers.Update(entity);
            return Ok();
        }

        // Customer Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            await unitOfWork.Customers.Delete(id);
            return Ok();
        }
    }
}
