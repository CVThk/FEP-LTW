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
    public class AccountService : IAccountService
    {
        private IAccountData _data;
        public AccountService(IAccountData accountData)
        {
            this._data = accountData;
        }
        public List<Account> getAll()
        {
            return _data.GetAccounts();
        }

        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(string name, string username, string password, string phone, DateTime dateOfBirth, string gender, int idWard)
        {
            int result = 0;
            using(var session = NHibernateHelper.OpenSession())
            {
                result = session.CreateSQLQuery(@"declare @result int exec sp_AddAccountClient :name, :username, :password, :phone, :dateOfBirth, :gender, :idWard select @result")
                    .SetParameter("name", name)
                    .SetParameter("username", username)
                    .SetParameter("password", password)
                    .SetParameter("phone", phone)
                    .SetParameter("dateOfBirth", dateOfBirth)
                    .SetParameter("gender", gender)
                    .SetParameter("idWard", idWard)
                    .UniqueResult<int>();
            }
            return result > 0;
        }
    }
}