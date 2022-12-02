using FEP.Core.Interfaces.IServices;
using FEP.Data;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEP.Core.Services
{
    public class CartService : ICartService
    {
        public void DeleteCart(int idClient, string idSneaker, int idSize)
        {
            ADOHelper.Instance.ExecuteNonQuery("delete tbl_Cart where IDClient = @para_0 and IDSneaker = @para_1 and IDSize = @para_2",
                new object[] { idClient, idSneaker, idSize });
        }

        public List<Cart> GetCarts()
        {
            List<Cart> carts = new List<Cart>();
            carts = ADOHelper.Instance.ExecuteReader<Cart>("select * from tbl_Cart");
            return carts;
        }

        public void InsertCart(Cart cart)
        {
            ADOHelper.Instance.ExecuteNonQuery(@"exec sp_InsertCart @para_0, @para_1, @para_2, @para_3",
                    new object[] { cart.IDClient, cart.IDSneaker, cart.IDSize, cart.AmountBuy });
        }

        public void UpdateCart(int idClient, string idSneaker, int idSize, int amount)
        {
            ADOHelper.Instance.ExecuteNonQuery("update tbl_Cart set AmountBuy = @para_0 where IDClient = @para_1 and IDSneaker = @para_2 and IDSize = @para_3",
                new object[] { amount, idClient, idSneaker, idSize });
        }
    }
}