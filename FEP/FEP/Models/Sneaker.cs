using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Models
{
    public class Sneaker
    {
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual int Discount { get; set; }
        public virtual int IDSneakerType { get; set; }
        public virtual double PriceAfterDiscount
        {
            get
            {
                return Price - (Price * (Discount / 100.0));
            }
            
        }

        public Sneaker() { }
    }
}