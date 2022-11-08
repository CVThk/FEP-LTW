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
        // GET: Home
        public ActionResult FEP()
        {
            ViewBag.Nikes = _Sneakers.FindAll(x => x.IDSneakerType == 2).Take(8).ToList();
            ViewBag.Adidas = _Sneakers.FindAll(x => x.IDSneakerType == 3).Take(4).ToList();
            ViewBag.MLBs = _Sneakers.FindAll(x => x.IDSneakerType == 7).Take(4).ToList();
            ViewBag.Deps = _Sneakers.FindAll(x => x.IDSneakerType == 8).Take(4).ToList();
            ViewBag.Luxurys = _Sneakers.FindAll(x => x.IDSneakerType == 5).Take(3).ToList();
            return View();
        }

        public ActionResult Products(int? page, int idSneakerType)
        {
            var pageNumber = (page ?? 1);
            ViewBag.pageSize = 8;
            var sneaker = _Sneakers.FindAll(x => x.IDSneakerType == idSneakerType).ToList();
            ViewBag.idSneakerType = idSneakerType;
            return View(sneaker.ToPagedList((int)pageNumber, (int)ViewBag.pageSize));
        }
    }
}