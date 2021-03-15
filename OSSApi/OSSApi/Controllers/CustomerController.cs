using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using OSSApi.Models;
using System.Threading.Tasks;
using OSSApi.Repository;
using OSSApi.Data;
using Dapper;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSSApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {

        private const string connectiongString = "Data Source=ITSPT-2NRHTQ2;Initial Catalog=OnlineShoppingSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpGet]
        public async Task<IActionResult> Index() {
            var query = @"select * from Customer";
            using (var connection = new SqlConnection(connectiongString)) {
                var customers = await connection.QueryAsync<Customer>(query);
                return Ok(customers);
            };
            return Ok("failed");
        }









        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
