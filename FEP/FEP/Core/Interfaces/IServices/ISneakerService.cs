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
        int GetIDSneakerType(string nameSneaker);
        List<string> GetCoverPicture(string idSneaker);
        List<string> GetDetailsPicture(string idSneaker);
        int GetAmountInventorySneaker(string idSneaker);
        Sneaker GetSneaker(string IDSneaker);

        List<int> GetSizes();
        int GetSize(int idSize);
        List<int> GetInventory(string idSneaker);
        List<int> GetSizeInventory(string idSneaker);
        bool CheckInventory(string idSneaker);
        bool CheckAmountInventory(string idSneaker, int size, int amount);
        bool CheckInventory(List<int> sizeOfSneaker);
        bool CheckSizeInventory(string idSneaker, int size);
        bool CheckSizeInventory(List<int> sizeOfSneaker, int size);
        int GetMaxInventory(string idSneaker, int idSize);
    }
}
