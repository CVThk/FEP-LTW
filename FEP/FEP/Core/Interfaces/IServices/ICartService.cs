using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEP.Core.Interfaces.IServices
{
    public interface ICartService
    {
        List<Cart> GetCarts();
        void InsertCart(Cart cart);
        void DeleteCart(int idClient, string idSneaker, int idSize);
        void UpdateCart(int idClient, string idSneaker, int idSize, int amount);
        void DeleteAll(int idClient);
    }
}
