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
        // GET: User
        static List<User> Clients = ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Client");
        static List<Account> Accounts = accountService.getAll();
        public ActionResult Profile(int idUser, int idAccount)
        {
            User user = Clients.Find(x => x.ID == idUser);
            Account account = Accounts.Find(x => x.ID == idAccount);
            if (user == null)
                return RedirectToAction("Error", "Error");
            Session["User"] = user;
            Session["Account"] = account;
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
        [HttpPost]
        public ActionResult Profile(FormCollection fc, HttpPostedFileBase Image)
        {
            string name, phone, idWard, gender, img;
            name = fc["Name"];
            phone = fc["Phone"];
            idWard = fc["Ward"];
            gender = fc["Gender"];
            img = fc["Image"];
            string dateofbirth = string.Format("{0:dd/MM/yyyy}", fc["DateOfBirth"]);
            bool kt = true;
            if (string.IsNullOrEmpty(img))
                img = "user.png";
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(idWard) || string.IsNullOrEmpty(dateofbirth))
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
                //ADOHelper.Instance.ExecuteNonQuery(@"update tbl_Client
                //                                    set Name = @para_0, DateOfBirth = @para_1, Gender = @para_2, Phone = @para_3, IDWard = @para_4, Image = @para_5
                //                                    where ID = @para_6", new object[] { name, dateofbirth, gender, phone, int.Parse(idWard), img, idUser });
                if (Image != null && Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/Profile/"), fileName);
                    Image.SaveAs(path);
                }
                Session["Error"] = "false";
            }
            return View();
        }
    }
}