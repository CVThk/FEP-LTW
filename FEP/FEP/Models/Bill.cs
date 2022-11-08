using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class Bill
    {
        public int ID { get; set; }
        public int IDClient { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalMoney { get; set; }
    }
}