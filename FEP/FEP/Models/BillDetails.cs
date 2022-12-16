using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class BillDetails
    {
		public int ID { get; set; }
		public int IDBill { get; set; }
		public int IDSize { get; set; }
		public string IDSneaker { get; set; }
		public int AmountBuy { get; set; }
		public double IntoMoney { get; set; }
	}
}