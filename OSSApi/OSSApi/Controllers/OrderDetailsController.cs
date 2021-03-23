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
    public class OrderDetailsController : ControllerBase {
        private readonly IUnitOfWork unitOfWork;
        public OrderDetailsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Post OrderDetails
        [HttpPost]
        public async Task<IActionResult> Insert(OrderDetails entity)
        {
            var data = await unitOfWork.OrderDetails.Insert(entity);
            return Ok(data);
        }
    }
}
