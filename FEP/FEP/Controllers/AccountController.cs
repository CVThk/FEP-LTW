using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.Models;
using FEP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FEP.Controllers
{
    public class AccountController : Controller
    {
        static SneakerManagementAPIController api = new SneakerManagementAPIController();
        // GET: Account
        public ActionResult Login()
        {
            Session["User"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            bool ktlogin = false;
            string pass = string.Format("{0}", fc["Password"]);
            string username = fc["Username"];
            if(string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(username))
            {
                //ViewData["LoiLogin"] = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                ViewData["Login"] = "false";
            }
            else
            {
                int idAccount = api.GetIDAccount(username, pass);
                Session["Account"] = idAccount;
                if(idAccount == 0)
                    ViewData["Login"] = "false";
                else
                {
                    ViewData["Login"] = "true";
                    int id;
                    id = api.GetIDStaffByIDAccount(idAccount);
                    if(id != 0)
                    {
                        ViewData["TypeAccount"] = "AD";
                    }   
                    else
                    {
                        id = api.GetIDClientByIDAccount(idAccount);
                        ViewData["TypeAccount"] = "CL";
                        ktlogin = true;
                    }
                    Session["User"] = id;
                }
            }
            if(ktlogin && Session["idsneaker"] != null)
            {
                return RedirectToAction("ProductDetails", "Home", new { idSneaker = Session["idSneaker"] });
            }    
            return View();
        }
        public ActionResult SignUp()
        {
            ViewBag.City = HelperData.Instance.GetCities();
            ViewBag.District = HelperData.Instance.GetDistricts();
            ViewBag.Ward = HelperData.Instance.GetWards();
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection fc)
        {
            bool ktSignUp = true;
            string username, name, password;
            username = fc["Username"];
            name = fc["Name"];
            password = fc["Password"];
            string phone = fc["Phone"];
            if (string.IsNullOrEmpty(username))
            {
                ktSignUp = false;
                ViewData["LoiUsername"] = "Không được bỏ trống!";
            }
            else if(username.Length < 5)
            {
                ktSignUp = false;
                ViewData["LoiUsername"] = "Username ít nhất 5 ký tự!";
            }    

            if (string.IsNullOrEmpty(name))
            {
                ViewData["LoiName"] = "Không được bỏ trống!";
                ktSignUp = false;
            }    

            if (string.IsNullOrEmpty(password))
            {
                ViewData["LoiPassword"] = "Không được bỏ trống!";
                ktSignUp = false;
            }    
            else if(!Utilities.Instance.CheckInputPassword(password))
            {
                ViewData["LoiPassword"] = "Không được nhập unicode!";
                ktSignUp = false;
            }    

            if (string.IsNullOrEmpty(phone))
            {
                ViewData["LoiPhone"] = "Không được bỏ trống!";
                ktSignUp = false;
            }    
            else if (!Utilities.Instance.CheckPhone(phone))
            {
                ViewData["LoiPhone"] = "Số điện thoại không đúng định dạng!";
                ktSignUp = false;
            }    

            string dateofbirth = string.Format("{0:dd/MM/yyyy}", fc["DateOfBirth"]);
            if (string.IsNullOrEmpty(dateofbirth))
            {
                ViewData["LoiDateOfBirth"] = "Phải chọn ngày sinh!";
                ktSignUp = false;
            }   
            else if(DateTime.Now.Year - DateTime.Parse(dateofbirth).Year < 15)
            {
                ViewData["LoiDateOfBirth"] = "Ngày sinh không phù hợp!";
                ktSignUp = false;
            }    

            string gender = fc["Gender"];
            if(string.IsNullOrEmpty(gender))
            {
                ViewData["LoiGender"] = "Không được bỏ trống!";
                ktSignUp = false;
            }

            string idWard = fc["Ward"];
            if (idWard == "null")
            {
                ViewData["LoiWard"] = "Không được bỏ trống!";
                ktSignUp = false;
            }

            if (ktSignUp)
            {
                bool ktAccount = api.SignUp(name, username, password, phone, DateTime.Parse(dateofbirth), gender, int.Parse(idWard));
                if (!ktAccount)
                    ViewData["LoiSignUp"] = "Tên đăng nhập hoặc số điện thoại đã tồn tại!";
                else return RedirectToAction("Login", "Account");
            }
            ViewBag.City = HelperData.Instance.GetCities();
            ViewBag.District = HelperData.Instance.GetDistricts();
            ViewBag.Ward = HelperData.Instance.GetWards();
            return View();
        }
    }
}