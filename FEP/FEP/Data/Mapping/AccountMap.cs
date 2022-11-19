using FEP.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Data.Mapping
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(x => x.ID);
            Map(x => x.Username);
            Map(x => x.Password);
            Table("tbl_Account");
        }
    }
}