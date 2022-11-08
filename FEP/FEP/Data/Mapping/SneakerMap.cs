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
            Table("tbl_Sneaker");
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.LinkPicture);
            Map(x => x.LinkPictureDetails);
            Map(x => x.Price);
            Map(x => x.Discount);
            Map(x => x.PriceAfterDiscount);
            Map(x => x.IDSneakerType);
        }    
    }
}