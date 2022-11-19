using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEP.Core.Interfaces.IData
{
    public interface IAccountData
    {
        List<Account> GetAccounts();
    }
}
