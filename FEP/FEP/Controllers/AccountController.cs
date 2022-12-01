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
            string pass = string.Format("{0}", fc["Password"]);
            string username = fc["Username"];
            if(string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(username))
            {
                //ViewData["LoiLogin"] = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                ViewData["Login"] = "false";
            }
            else
            {
                int idAccount = ADOHelper.Instance.ExecuteScalar(@"declare @idAccount int
                                                                    exec @idAccount = sp_GetIDAccount @para_0,@para_1
                                                                    select @idAccount", new object[] { username, pass });
                if(idAccount == 0)
                    ViewData["Login"] = "false";
                else
                {
                    ViewData["Login"] = "true";
                    int id;
                    id = ADOHelper.Instance.ExecuteScalar(@"declare @id int
                                                            exec @id = sp_GetIDStaffByIDAccount @para_0
                                                            select @id", new object[] { idAccount });
                    if(id != 0)
                    {
                        ViewData["TypeAccount"] = "AD";
                        List<User> Staffs = ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Staff");
                        Session["User"] = Staffs.Find(x => x.ID == id);
                    }   
                    else
                    {
                        id = ADOHelper.Instance.ExecuteScalar(@"declare @id int
                                                            exec @id = sp_GetIDClientByIDAccount @para_0
                                                            select @id", new object[] { idAccount });
                        ViewData["TypeAccount"] = "CL";
                        List<User> Clients = ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Client");
                        Session["User"] = Clients.Find(x => x.ID == id);
                    }
                }
            }
            //if (ktLogin)
            //    return RedirectToAction("FEP", "Home");
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
                bool ktAccount = accountService.SignUp(name, username, password, phone, DateTime.Parse(dateofbirth), gender, int.Parse(idWard));
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