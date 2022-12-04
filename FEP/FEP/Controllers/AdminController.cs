using FEP.Data;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FEP.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Account account = Session["Account"] as Account;
            if (account == null) return RedirectToAction("Login", "Account");
            int id = ADOHelper.Instance.ExecuteScalar(@"declare @id int
                                                            exec @id = sp_GetIDStaffByIDAccount @para_0
                                                            select @id", new object[] { account.ID });
            if(id != 0)
                return View();
            return RedirectToAction("Forbidden", "Error");
        }
    }
}