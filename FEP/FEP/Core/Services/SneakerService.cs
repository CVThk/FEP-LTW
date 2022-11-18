using FEP.Core.Interfaces.IData;
using FEP.Core.Interfaces.IServices;
using FEP.Data;
using FEP.Models;
using NHibernate;
using NHibernate.Transform;
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

        public bool CheckInventory(string idSneaker)
        {
            List<int> sizeOfSneaker = GetSizeInventory(idSneaker);
            foreach (var item in sizeOfSneaker)
            {
                if (item == 0)
                    return false;
            }
            return true;
        }

        public bool CheckInventory(List<int> sizeOfSneaker)
        {
            foreach (var item in sizeOfSneaker)
            {
                if (item == 0)
                    return false;
            }
            return true;
        }

        public bool CheckSizeInventory(string idSneaker, int size)
        {
            List<int> sizeOfSneaker = GetSizeInventory(idSneaker);
            foreach (var item in sizeOfSneaker)
            {
                if (item == size)
                    return true;
            }
            return false;
        }

        public bool CheckSizeInventory(List<int> sizeOfSneaker, int size)
        {
            foreach (var item in sizeOfSneaker)
            {
                if (item == size)
                    return true;
            }
            return false;
        }

        public List<Sneaker> getAll()
        {
            return _data.GetSneakers();
        }

        public int GetAmountInventorySneaker(string idSneaker)
        {
            int amount;
            using (var session = NHibernateHelper.OpenSession())
            {
                amount = session.CreateSQLQuery("declare @sl int exec @sl = sp_GetAmountInventorySneaker :idSneaker select @sl")
                .SetParameter("idSneaker", idSneaker)
                .UniqueResult<int>();
            }
            return amount;
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

        public List<string> GetDetailsPicture(string idSneaker)
        {
            List<string> list = new List<string>();
            using (var session = NHibernateHelper.OpenSession())
            {
                list = (List<string>)session.CreateSQLQuery("select Link from tbl_DetailsImage where tbl_DetailsImage.IDSneaker = :id").SetParameter("id", idSneaker).List<string>();
            }
            return list;
        }

        public int GetIDSneaker(string nameSneaker)
        {
            int row;
            using (var session = NHibernateHelper.OpenSession())
            {
                row = session.CreateSQLQuery("exec sp_GetIDSneaker :nameSneaker").SetParameter("nameSneaker", nameSneaker).UniqueResult<int>();
            }
            return row;
        }

        public List<int> GetSizeInventory(string idSneaker)
        {
            List<int> list = new List<int>();
            using (var session = NHibernateHelper.OpenSession())
            {
                list = (List<int>)session.CreateSQLQuery("exec sp_GetSizeInventory :id").SetParameter("id", idSneaker).List<int>();
            }
            return list;
        }

        public List<int> GetSizes()
        {
            List<int> list = new List<int>();
            using (var session = NHibernateHelper.OpenSession())
            {
                list = (List<int>)session.CreateSQLQuery("select Size from tbl_Size").List<int>();
            }
            return list;
        }
    }
}