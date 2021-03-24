﻿using Microsoft.AspNetCore.Http;
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
    public class OrdersController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        // GetAll Order
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var data = await unitOfWork.Orders.GetAll();
            return Ok(data);
        }

        // GetById Order
        /*[HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Orders.GetById(id);
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }*/

        // Post Order
        [HttpPost]
        public async Task<IActionResult> Insert(Orders entity) {
            await unitOfWork.Orders.Insert(entity);
            return Ok();
        }

    }
}
