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

        public void ChangePassword(string username, string password)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.CreateSQLQuery(@"update tbl_Account set Password = :pass where Username = :username")
                    .SetParameter("pass", password)
                    .SetParameter("username", username)
                    .UniqueResult<int>();
            }
        }

        public List<Account> getAll()
        {
            return _data.GetAccounts();
        }

        public int GetIDAccount(string username, string password)
        {
            return ADOHelper.Instance.ExecuteScalar(@"declare @idAccount int
                                                                    exec @idAccount = sp_GetIDAccount @para_0,@para_1
                                                                    select @idAccount", new object[] { username, password });
        }

        public int GetIDClientByIDAccount(int idAccount)
        {
            return ADOHelper.Instance.ExecuteScalar(@"declare @id int
                                                            exec @id = sp_GetIDClientByIDAccount @para_0
                                                            select @id", new object[] { idAccount });
        }

        public int GetIDStaffByIDAccount(int idAccount)
        {
            return ADOHelper.Instance.ExecuteScalar(@"declare @id int
                                                            exec @id = sp_GetIDStaffByIDAccount @para_0
                                                            select @id", new object[] { idAccount });
        }

        public void ResetPassword(string username)
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