﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class Account
    {
        public virtual int ID { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}