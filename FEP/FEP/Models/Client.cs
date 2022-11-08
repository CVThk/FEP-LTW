using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int IDAddress { get; set; } // lấy id của xã/phường rồi chuẩn hóa Address sau
        public string Address { get; set; }
    }
}