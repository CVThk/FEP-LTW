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

        public bool SignUp(string username, string password, string name, string phone)
        {
            int result = 0;
            using(var session = NHibernateHelper.OpenSession())
            {
                result = session.CreateSQLQuery(@"declare @result int exec sp_AddAccountClient :username, :password, :name, :phone select @result")
                    .SetParameter("username", username)
                    .SetParameter("password", password)
                    .SetParameter("name", name)
                    .SetParameter("phone", phone)
                    .UniqueResult<int>();
            }
            return result > 0;
        }
    }
}