using FEP.Core.Interfaces.IData;
using FEP.Core.Interfaces.IServices;
using FEP.Data;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Core.Services
{
    public class SneakerService : ISneakerService
    {
        private ISneakerData _data;
        public SneakerService(ISneakerData sneakerData)
        {
            this._data = sneakerData;
        }
        public List<Sneaker> getAll()
        {
            return _data.GetSneakers();
        }

        public List<string> GetCoverPicture(string idSneaker)
        {
            List<string> list = new List<string>();
            using (var session = NHibernateHelper.OpenSession())
            {
                list = (List<string>)session.CreateSQLQuery("select Link from tbl_CoverImage where tbl_CoverImage.IDSneaker = :id").SetParameter("id", idSneaker).List<string>();
            }
            return list;
        }

        public int GetIDSneaker(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                int row = session.CreateSQLQuery("exec sp_GetIDSneaker :name").SetParameter("name", name).UniqueResult<int>();
                return row;
            }
        }
    }
}