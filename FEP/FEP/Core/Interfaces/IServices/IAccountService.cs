using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEP.Core.Interfaces.IServices
{
    public interface IAccountService:IBasicService<Account>
    {
        bool SignUp(string name, string username, string password, string phone, DateTime dateOfBirth, string gender, int idWard);
        void ChangePassword(string username, string password);
        void ResetPassword(string username);
        int GetIDAccount(string username, string password);
        int GetIDClientByIDAccount(int idAccount);
        int GetIDStaffByIDAccount(int idAccount);
    }
}
