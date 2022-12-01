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
        string Login(string username, string password);
    }
}
