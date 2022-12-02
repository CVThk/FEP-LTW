﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int IDClient { get; set; }
        public int IDSize { get; set; }
        public string IDSneaker { get; set; }
        public int AmountBuy { get; set; }

        public Cart() { }
        public Cart(int idClient, string idSneaker, int idSize, int amount)
        {
            this.IDClient = idClient;
            this.IDSneaker = idSneaker;
            this.IDSize = idSize;
            this.AmountBuy = amount;
        }
    }
}