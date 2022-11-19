using FEP.Core.Interfaces.IData;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Data
{
    public class NHibernateData : ISneakerData, IAccountData
    {
        public List<Account> GetAccounts()
        {
            List<Account> accounts;
            using (var session = NHibernateHelper.OpenSession())
            {
                accounts = session.Query<Account>().ToList();
            }
            return accounts;
        }

        public List<Sneaker> GetSneakers()
        {
            List<Sneaker> sneakers;
            using(var session = NHibernateHelper.OpenSession())
            {
                sneakers = session.Query<Sneaker>().ToList();
            }
            return sneakers;
        }
    }
}