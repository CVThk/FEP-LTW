using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEP.Core.Interfaces.IServices
{
    public interface ISneakerService:IBasicService<Sneaker>
    {
        int GetIDSneaker(string name);
        List<string> GetCoverPicture(string idSneaker);
    }
}
