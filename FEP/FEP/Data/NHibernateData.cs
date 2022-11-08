using FEP.Core.Interfaces.IData;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Data
{
    public class NHibernateData : ISneakerData
    {
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