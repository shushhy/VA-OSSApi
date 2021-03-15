using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private const string connectionString = "Data Source=ITSPT-2NRHTQ2;Initial Catalog=OnlineShoppingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    }
}
