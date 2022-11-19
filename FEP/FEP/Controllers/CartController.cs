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
            ViewBag.TotalAmount = TotalAmount();
            return PartialView();
        }

        public ActionResult Cart()
        {
            var listCart = Session["ListCart"] as List<Cart>;
            return View(listCart);
        }
    }
}