//using MailOrderPharmacy_RefillService.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MailOrderPharmacy_RefillService.Repository
//{
//    public class Refill : IRefill
//    {
//        public static List<RefillDetails> ls = new List<RefillDetails>
//        {
//            new RefillDetails
//            {
//                Subscription_ID = 7,
//                FromDate = new DateTime(2020, 05, 15),
//                Status=true,
//                Policy_ID = 001,
//                Member_ID = 01,
//                Location = "Delhi"
//            },
//            new RefillDetails
//            {
//                Subscription_ID = 8,
//                FromDate = new DateTime(2020, 05, 15),
//                Status=false,
//                Policy_ID = 001,
//                Member_ID = 02,
//                Location = "Mumbai"
//            }
//        };

//        public RefillDetails viewRefillStatus(int Sub_Id)
//        {
//            var item = ls.Where(x => x.Subscription_ID == Sub_Id).FirstOrDefault();
//            return item;
//        }
//        public RefillDetails getRefillDuesAsOfDate(int Sub_Id, DateTime fromd)
//        {
//            var item = ls.Where(x => x.Subscription_ID == Sub_Id && x.FromDate == fromdate).FirstOrDefault();
//            return item;
//        }
//        public RefillDetails requestAdhocRefill(int P_Id, int M_Id, int Sub_Id, string location)
//        {
//            var item = ls.Where(x => x.Policy_ID == P_Id && x.Member_ID == M_Id && x.Subscription_ID == Sub_Id && x.Location == location).FirstOrDefault();
//            return item;
//        }
//    }
//}

