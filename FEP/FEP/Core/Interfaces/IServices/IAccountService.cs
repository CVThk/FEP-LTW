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
        bool SignUp(string username, string password, string name, string phone);
    }
}
