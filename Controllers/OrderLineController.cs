//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using MailOrderPharmacy_RefillService.Models;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace MailOrderPharmacy_RefillService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderLineController : ControllerBase
//    {
//        public static List<RefillOrderLine> ls = new List<RefillOrderLine>
//        {
//            new RefillOrderLine
//            {
//                Id = 1,
//                Drug  = "Paracetamol",
//                DrugQuantity = 100
//            }
//        };

//        // GET: api/<OrderLineController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<OrderLineController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<OrderLineController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<OrderLineController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<OrderLineController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
