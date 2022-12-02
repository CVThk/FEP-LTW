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
        static int IDTypeNike = _SneakerService.GetIDSneakerType("Nike");
        static int IDTypeAdidas = _SneakerService.GetIDSneakerType("Adidas");
        static int IDTypeLuxury = _SneakerService.GetIDSneakerType("Luxury");
        static int IDTypeMLB = _SneakerService.GetIDSneakerType("MLB");
        static int IDTypeDep = _SneakerService.GetIDSneakerType("Dép");
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
            List<int> sizeInventory = _SneakerService.GetSizeInventory(idSneaker);
            Sneaker s = _Sneakers.Find(x => x.ID == idSneaker);
            if(!_SneakerService.CheckInventory(sizeInventory))
            {
                return RedirectToAction("Error", "Home", new { sneaker = s });
            }
            List<int> inventory = _SneakerService.GetInventory(idSneaker);

            ViewBag.Sneaker = s;
            ViewBag.SPTT = _Sneakers.FindAll(x => x.IDSneakerType == s.IDSneakerType).Take(8).ToList();
            ViewBag.DetailsImage = _SneakerService.GetDetailsPicture(idSneaker);
            ViewBag.Sizes = _SneakerService.GetSizes();
            ViewBag.InventorySize = sizeInventory;
            ViewBag.Inventory = inventory;
            ViewBag.SneakerService = _SneakerService;
            return View();
        }

        public ActionResult Error(Sneaker sneaker)
        {
            return View(sneaker);
        }
    }
}