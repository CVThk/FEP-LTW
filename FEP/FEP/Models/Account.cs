﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public int IDClient { get; set; }
    }
}