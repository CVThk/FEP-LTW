using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FEP.Utility;
using System.IO;

namespace FEP.Controllers
{
    public class UserController : Controller
    {
        static IAccountData _NHibernateData = new NHibernateData();
        static AccountService accountService = new AccountService(_NHibernateData);
        static SneakerManagementAPIController api = new SneakerManagementAPIController();

        // GET: User
        static List<User> Clients;
        static List<Account> Accounts = accountService.getAll();
        public ActionResult Profile()
        {
            try
            {
                Clients = ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Client");
                int idUser = int.Parse(Session["User"].ToString());
                int idAccount = int.Parse(Session["Account"].ToString());
                User user = Clients.Find(x => x.ID == idUser);
                Account account = Accounts.Find(x => x.ID == idAccount);
                if (user == null)
                    return RedirectToAction("Error", "Error");
                List<City> cities = HelperData.Instance.GetCities();
                List<District> districts = HelperData.Instance.GetDistricts();
                List<Ward> wards = HelperData.Instance.GetWards();
                Ward ward = wards.Find(x => x.ID == user.IDWard);
                District district = districts.Find(x => x.ID == ward.IDDistrict);
                City city = cities.Find(x => x.ID == district.IDCity);
                ViewBag.City = city;
                ViewBag.District = district;
                ViewBag.Ward = ward;
                ViewBag.Citys = cities;
                ViewBag.Districts = districts;
                ViewBag.Wards = wards;
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpPost]
        public ActionResult Profile(FormCollection fc, HttpPostedFileBase Image)
        {
            int idUser = 0;
            if (Session["User"] != null)
            {
                idUser = int.Parse(Session["User"].ToString());
            }
            User user = api.GetClient(idUser);
            if (user == null)
                return RedirectToAction("Login", "Account");
            
            string name, phone, idWard, gender, img;
            string strURL = fc["URL"];
            
            name = fc["Name"];
            phone = fc["Phone"];
            idWard = fc["Ward"];
            gender = fc["Gender"];
            string dateofbirth = string.Format("{0:dd/MM/yyyy}", fc["DateOfBirth"]);
            bool kt = true;
            
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(idWard) || idWard == "null" || string.IsNullOrEmpty(dateofbirth))
            {
                Session["Error"] = "Không được bỏ trống thông tin !!!";
                kt = false;
            }    
            else if (!Utilities.Instance.CheckPhone(phone))
            {
                kt = false;
                Session["Error"] = "Số điện thoại không đúng định dạng";
            }    
            if(kt)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/Profile/"), fileName);
                    Image.SaveAs(path);
                    img = fileName;
                }
                else img = "user.png";
                ADOHelper.Instance.ExecuteNonQuery(@"update tbl_Client
                                                    set Name = @para_0, DateOfBirth = @para_1, Gender = @para_2, Phone = @para_3, IDWard = @para_4, Image = @para_5
                                                    where ID = @para_6", new object[] { name, dateofbirth, gender, phone, int.Parse(idWard), img, user.ID });
                Session["Error"] = "false";
                Session["User"] = user.ID;
            }
            return Redirect(strURL);
        }
        public ActionResult PasswordSwitch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PasswordSwitch(FormCollection fc)
        {
            try
            {
                string password, newPassword, prePassword;
                password = fc["Password"];
                newPassword = fc["NewPassword"];
                prePassword = fc["PrePassword"];
                bool kt = true;
                if (string.IsNullOrEmpty(password))
                {
                    ViewData["LoiPassword"] = "Không được bỏ trống !!!";
                    kt = false;
                }
                if (string.IsNullOrEmpty(newPassword))
                {
                    ViewData["LoiNewPassword"] = "Không được bỏ trống !!!";
                    kt = false;
                }
                else if (!Utilities.Instance.CheckInputPassword(newPassword))
                {
                    ViewData["LoiNewPassword"] = "Mật khẩu ít nhất 5 ký tự !!!";
                    kt = false;
                }

                if (string.IsNullOrEmpty(prePassword))
                {
                    ViewData["LoiPrePassword"] = "Không được bỏ trống !!!";
                    kt = false;
                }
                else if (newPassword != prePassword)
                {
                    ViewData["LoiPrePassword"] = "Mật khẩu không trùng khớp !!!";
                    kt = false;
                }
                int idAccount = 0;
                if (Session["Account"] != null)
                    idAccount = int.Parse(Session["Account"].ToString());
                Account account = api.GetAccount(idAccount);
                if(account != null)
                {
                    if (account.Password != password)
                    {
                        ViewData["LoiPassword"] = "Mật khẩu không chính xác !!!";
                        kt = false;
                    }
                }    
                if (kt)
                {
                    accountService.ChangePassword(api.GetAccount(int.Parse(Session["Account"].ToString())).Username, newPassword);
                    ViewData["Error"] = "Cập nhật thành công.";
                }

            }
            catch
            {
                ViewData["Error"] = "Cập nhật thất bại !!!";
            }
            return View();
        }

        public ActionResult Bill(int idClient)
        {
            List<Bill> bills = ADOHelper.Instance.ExecuteReader<Bill>("select * from tbl_Bill where IDClient = @para_0", new object[] { idClient });
            return View(bills);
        }
    }
}