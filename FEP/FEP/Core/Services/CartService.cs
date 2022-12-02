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
    }
}