using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MailOrderPharmacy_RefillService.Models;
//using MailOrderPharmacy_RefillService.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailOrderPharmacy_RefillService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefillController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        //IRefill _db;

        public RefillController()
        {
            
            _log4net = log4net.LogManager.GetLogger(typeof(RefillController));
        }


        public static List<RefillDetails> ls = new List<RefillDetails>
        {
            new RefillDetails
            {
                RefillOrderId=1,
                Subscription_ID = 7,
                DrugID=1,
                RefillDate=new DateTime(2020,09,12),
                FromDate = new DateTime(2020, 05, 15),
                NextRefillDate=new DateTime(2020,10,08),
                Status="pending",
                Policy_ID = 001,
                Member_ID = 01,
                Location = "Delhi"
            },
            new RefillDetails
            {
                RefillOrderId=2,
                Subscription_ID = 8,
                DrugID=1,
                RefillDate=new DateTime(2020,09,12),
                FromDate = new DateTime(2020, 05, 15),
                NextRefillDate=new DateTime(2020,10,08),
                Status="clear",
                Policy_ID = 001,
                Member_ID = 02,
                Location = "Mumbai"
            }
        };
        // GET: api/<RefillController>/7
        [HttpGet("RefillStatus/{id}")]
        public IActionResult RefillStatus(int id)
        {
            var item = ls.Where(x => x.Subscription_ID == id).FirstOrDefault();
            return Ok(item);
        }

        //GET api/<RefillController>/5
        //[HttpGet("RefillFromDate/{id}")]
        //public ActionResult RefillFromDate(int id)
        //{
        //    string data = JsonConvert.SerializeObject(id);
        //    int x = obj.Drug_ID;
        //    Uri baseAddress = new Uri("https://localhost:44318/api");
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = baseAddress;

        //    HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Subscription/" + id).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        data = response.Content.ReadAsStringAsync().Result;
        //        Subs s = JsonConvert.DeserializeObject<Subs>(data);
        //        if (s.Sub_id == id)
        //        {


        //            return Ok(ls);
        //        }
        //        return BadRequest("Unavailable");

        //    }

        //    return BadRequest();
        //}

        [HttpGet("RefillDueAsOfDate/{id}/{FromDate}")]
        public IActionResult RefillDueAsOfDate(int id, DateTime FromDate)
        {
            string data = JsonConvert.SerializeObject(id);
            //int x = obj.Drug_ID;
            Uri baseAddress = new Uri("https://localhost:44318/api/Subscription/" +id );
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                Subs s = JsonConvert.DeserializeObject<Subs>(data);
                string freq = s.RefillOccurrence;
                 return Ok(PendingRefill(id,FromDate, freq));
                

            }

            return BadRequest();
        }
        [HttpPost]
        public IEnumerable<RefillDetails> PendingRefill(int id,DateTime date, string freq)
        {
            List<RefillDetails> Pending = new List<RefillDetails>();
            if(freq=="Weekly")
            {
                int month = date.Month;
                int nxtmonth = month + 1;

                while(month!=nxtmonth)
                {

                    
                    RefillDetails refill = new RefillDetails();
                    refill.Subscription_ID = id;
                    
                    date = date.AddDays(7);
                    refill.RefillDate = date;
                    refill.NextRefillDate = date.AddDays(7);
                    Pending.Add(refill);
                    month = date.Month;

                }
            }
            else
           
            {
                int year = date.Year;
                int nxtyear = year + 1;

                while (year != nxtyear)
                {


                    RefillDetails refill = new RefillDetails();
                    refill.Subscription_ID = id;

                    date = date.AddMonths(1);
                    refill.RefillDate = date;
                    refill.NextRefillDate = date.AddMonths(1);
                    Pending.Add(refill);
                    year = date.Year;

                }
            }
            return Pending;
        }

        // POST api/<RefillController>
        [HttpPost("requestAdhocRefill")]
        public IActionResult requestAdhocRefill([FromBody] RefillOrderLine order)
        {
            RefillDetails detail=ls.Where(x => x.Member_ID == order.Member_ID).FirstOrDefault();

            //string data = JsonConvert.SerializeObject(order);
            //int x = obj.Drug_ID;
            Uri baseAddress = new Uri("https://localhost:44372/api/Drugs/" + detail.DrugID);
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Drug s = JsonConvert.DeserializeObject<Drug>(data);
                if(s.drugLocation.Location==order.Location)
                {
                    return Ok(detail);
                }
                return Ok("Unavailable");


            }

            return BadRequest();
        }

        // PUT api/<RefillController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RefillController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
