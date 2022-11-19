using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.SingletonPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FEP.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        static IAccountData _NHibernateData = new NHibernateData();
        static AccountService accountService = new AccountService(_NHibernateData);
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection fc)
        {
            if (fc.Count == 0)
                return View();
            string phone = fc["Phone"];
            if(!Utilities.Instance.CheckPhone(phone))
            {
                ViewBag.Err = "Số điện thoại chỉ được nhập số!";
                return View();
            }
            string username, name, password;
            username = fc["Username"];
            name = fc["Name"];
            password = fc["Password"];
            if(username.Length == 0 || name.Length == 0 || password.Length == 0)
            {
                ViewBag.Err = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }    
            if(!accountService.SignUp(username, password, name, phone))
            {
                ViewBag.Err = "Tên đăng nhập hoặc số điện thoại đã tồn tại!";
                return View();
            }
            return RedirectToAction("Login");
        }
    }
}