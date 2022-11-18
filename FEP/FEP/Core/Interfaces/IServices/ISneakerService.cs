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
        int GetIDSneaker(string nameSneaker);
        List<string> GetCoverPicture(string idSneaker);
        List<string> GetDetailsPicture(string idSneaker);
        int GetAmountInventorySneaker(string idSneaker);

        List<int> GetSizes();
        List<int> GetSizeInventory(string idSneaker);
        bool CheckInventory(string idSneaker);
        bool CheckInventory(List<int> sizeOfSneaker);
        bool CheckSizeInventory(string idSneaker, int size);
        bool CheckSizeInventory(List<int> sizeOfSneaker, int size);
    }
}
