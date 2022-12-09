using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FEP.Controllers
{
    public class SneakerManagementAPIController : ApiController
    {
        static IAccountData _NHibernateData = new NHibernateData();
        static AccountService accountService = new AccountService(_NHibernateData);

        // GET api/<controller>
        public List<User> GetClients()
        {
            return ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Client");
        }
        // GET api/<controller>/5
        public User GetClient(int id)
        {
            return GetClients().SingleOrDefault(x => x.ID == id);
        }
        public List<User> GetStaffs()
        {
            return ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Staff");
        }
        public User GetStaff(int id)
        {
            return GetStaffs().SingleOrDefault(x => x.ID == id);
        }
        public List<Account> GetAccounts()
        {
            return accountService.getAll();
        }
        public Account GetAccount(int id)
        {
            return GetAccounts().SingleOrDefault(x => x.ID == id);
        }
        public int GetIDAccount(string username, string password)
        {
            return accountService.GetIDAccount(username, password);
        }
        public int GetIDClientByIDAccount(int idAccount)
        {
            return accountService.GetIDClientByIDAccount(idAccount);
        }

        public int GetIDStaffByIDAccount(int idAccount)
        {
            return accountService.GetIDStaffByIDAccount(idAccount);
        }

        // POST api/<controller>
        public bool SignUp(string name, string username, string password, string phone, DateTime dateOfBirth, string gender, int idWard)
        {
            return accountService.SignUp(name, username, password, phone, dateOfBirth, gender, idWard);
        }

        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}