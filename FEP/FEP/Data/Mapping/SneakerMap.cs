using FEP.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Data.Mapping
{
    public class SneakerMap : ClassMap<Sneaker>
    {
        public SneakerMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.Price);
            Map(x => x.Discount);
            Map(x => x.IDSneakerType);
            Table("tbl_Sneaker");
        }    
    }
}