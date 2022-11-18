using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FEP.Controllers
{
    public class HomeController : Controller
    {
        static ISneakerData _NHibernateData = new NHibernateData();
        static SneakerService _SneakerService = new SneakerService(_NHibernateData);
        static List<Sneaker> _Sneakers = _SneakerService.getAll();
        static int IDTypeNike = _SneakerService.GetIDSneaker("Nike");
        static int IDTypeAdidas = _SneakerService.GetIDSneaker("Adidas");
        static int IDTypeLuxury = _SneakerService.GetIDSneaker("Luxury");
        static int IDTypeMLB = _SneakerService.GetIDSneaker("MLB");
        static int IDTypeDep = _SneakerService.GetIDSneaker("Dép");
        // GET: Home
        public ActionResult FEP()
        {
            ViewBag.Nikes = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeNike).Take(8).ToList();
            ViewBag.Adidas = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeAdidas).Take(4).ToList();
            ViewBag.MLBs = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeMLB).Take(4).ToList();
            ViewBag.Deps = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeDep).Take(4).ToList();
            ViewBag.Luxurys = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeLuxury).Take(3).ToList();


            Session["IDTypeNike"] = IDTypeNike;
            Session["IDTypeAdidas"] = IDTypeAdidas;
            Session["IDTypeLuxury"] = IDTypeLuxury;
            Session["IDTypeMLB"] = IDTypeMLB;
            Session["IDTypeDep"] = IDTypeDep;
            return View();
        }

        public ActionResult Products(int? page, int idSneakerType)
        {
            Session["IDTypeNike"] = IDTypeNike;
            Session["IDTypeAdidas"] = IDTypeAdidas;
            Session["IDTypeLuxury"] = IDTypeLuxury;
            Session["IDTypeMLB"] = IDTypeMLB;
            Session["IDTypeDep"] = IDTypeDep;
            var pageNumber = (page ?? 1);
            ViewBag.pageSize = 8;
            var sneaker = _Sneakers.FindAll(x => x.IDSneakerType == idSneakerType).ToList();
            ViewBag.idSneakerType = idSneakerType;
            return View(sneaker.ToPagedList((int)pageNumber, (int)ViewBag.pageSize));
        }

        public ActionResult ProductDetails(string idSneaker)
        {
            List<int> inventory = _SneakerService.GetSizeInventory(idSneaker);
            Sneaker s = _Sneakers.Find(x => x.ID == idSneaker);
            if(!_SneakerService.CheckInventory(inventory))
            {
                return RedirectToAction("ErrInventory", "Home", new { sneaker = s });
            }    
            ViewBag.Sneaker = s;
            ViewBag.SPTT = _Sneakers.FindAll(x => x.IDSneakerType == s.IDSneakerType).Take(8).ToList();
            ViewBag.DetailsImage = _SneakerService.GetDetailsPicture(idSneaker);
            ViewBag.Sizes = _SneakerService.GetSizes();
            ViewBag.InventorySize = inventory;
            ViewBag.SneakerService = _SneakerService;
            return View();
        }

        public ActionResult ErrInventory(Sneaker sneaker)
        {
            return View(sneaker);
        }
    }
}