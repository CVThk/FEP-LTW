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
    public class AdminController : Controller
    {
        static ISneakerData _NHibernateData = new NHibernateData();
        static SneakerService _SneakerService = new SneakerService(_NHibernateData);
        static SneakerManagementAPIController api = new SneakerManagementAPIController();
        // GET: Admin
        int checkAccount()
        {
            int idAccount = 0;
            if (Session["Account"] != null)
                idAccount = int.Parse(Session["Account"].ToString());
            Account account = api.GetAccount(idAccount);
            if (account == null) return 0;
            int id = ADOHelper.Instance.ExecuteScalar(@"declare @id int
                                                            exec @id = sp_GetIDStaffByIDAccount @para_0
                                                            select @id", new object[] { account.ID });
            if (id != 0) return 1;
            return -1;
        }
        public ActionResult Dashboard()
        {
            int check = checkAccount();
            if (check == 0) return RedirectToAction("Login", "Account");
            if (check == -1) return RedirectToAction("Forbidden", "Error");
            Session["Bills"] = ADOHelper.Instance.ExecuteReader<Bill>("select * from tbl_Bill").Take(10).ToList();
            Session["Clients"] = api.GetClients();
            Session["Slider"] = 0;
            return View();
        }
        public ActionResult Products(int? page)
        {
            int check = checkAccount();
            if (check == 0) return RedirectToAction("Login", "Account");
            if (check == -1) return RedirectToAction("Forbidden", "Error");
            Session["Slider"] = 1;
            var pageNumber = (page ?? 1);
            ViewBag.pageSize = 10;
            var sneakers = _SneakerService.getAll();
            var sizes = ADOHelper.Instance.ExecuteReader<SizeSneaker>("select * from tbl_Size");
            Session["Sizes"] = sizes;
            Session["SneakerTypes"] = ADOHelper.Instance.ExecuteReader<SneakerType>("select * from tbl_SneakerType");
            Session["SneakerService"] = _SneakerService;
            return View(sneakers.ToPagedList((int)pageNumber, (int)ViewBag.pageSize));
        }
    }
}