using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FEP.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        static ISneakerData _NHibernateData = new NHibernateData();
        static SneakerService _SneakerService = new SneakerService(_NHibernateData);
        static List<Sneaker> _Sneakers = _SneakerService.getAll();

        static CartService _CartService = new CartService();
        static List<Cart> carts;
        public int TotalAmount()
        {
            int sl = 0;
            List<Cart> listCart = Session["ListCart"] as List<Cart>;
            if (listCart != null && listCart.Count > 0)
            {
                sl += listCart.Sum(x => x.AmountBuy);
            }
            return sl;
        }

        public double TotalMoney()
        {
            double tien = 0;
            List<Cart> listCart = Session["ListCart"] as List<Cart>;
            if (listCart != null && listCart.Count > 0)
            {
                tien += listCart.Sum(x => x.AmountBuy * _SneakerService.GetSneaker(x.IDSneaker).PriceAfterDiscount);
            }
            return tien;
        }
        public ActionResult CartPartial()
        {
            carts = _CartService.GetCarts();
            //ViewBag.TotalAmount = TotalAmount();
            User user = Session["User"] as User;
            if (user == null)
                ViewBag.TotalAmount = 0;
            else
            {
                ViewBag.TotalAmount = carts.Count(x => x.IDClient == user.ID);
                Session["ListCart"] = carts;
                Session["ListSneaker"] = _Sneakers;
            }
            return PartialView();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult AddCart(FormCollection fc, string url)
        {
            string idUser = fc["idclient"];
            string idSneaker = fc["idsneaker"];
            string size = fc["size"];
            string amount = fc["amount"];
            if (string.IsNullOrEmpty(idUser))
            {
                Session["idsneaker"] = idSneaker;
                return RedirectToAction("Login", "Account");
            }
            if(_SneakerService.CheckAmountInventory(idSneaker, int.Parse(size), int.Parse(amount)))
            {
                Cart cart = new Cart(int.Parse(idUser), idSneaker, int.Parse(size), int.Parse(amount));
                _CartService.InsertCart(cart);
            }
            else
            {
                Session["ErrAmount"] = "true";
            }
            return Redirect(url);
        }
    }
}