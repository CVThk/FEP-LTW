using FEP.Core.Interfaces.IData;
using FEP.Core.Services;
using FEP.Data;
using FEP.Models;
using FEP.Utility;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Profile()
        {
            try
            {
                List<User> Staffs = ADOHelper.Instance.ExecuteReader<User>("select * from tbl_Staff");
                int idUser = int.Parse(Session["User"].ToString());
                int idAccount = int.Parse(Session["Account"].ToString());
                User user = Staffs.Find(x => x.ID == idUser);
                Account account = api.GetAccounts().SingleOrDefault(x => x.ID == idAccount);
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
            User user = api.GetStaff(idUser);
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
            if (kt)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/Profile/"), fileName);
                    Image.SaveAs(path);
                    img = fileName;
                }
                else img = "user.png";
                ADOHelper.Instance.ExecuteNonQuery(@"update tbl_Staff
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
                if (account != null)
                {
                    if (account.Password != password)
                    {
                        ViewData["LoiPassword"] = "Mật khẩu không chính xác !!!";
                        kt = false;
                    }
                }
                if (kt)
                {
                    IAccountData accountData = new NHibernateData();
                    AccountService accountService = new AccountService(accountData);
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


        public ActionResult Delete(string idSneaker)
        {
            string result;
            using(var session = NHibernateHelper.OpenSession())
            {
                result = session.CreateSQLQuery(@"declare @result nvarchar(max)
                                                    exec sp_DeleteSneaker @idSneaker=:idSneaker, @result=@result output
                                                    select @result").SetParameter("idSneaker", idSneaker).UniqueResult<string>();
            }
            Session["DeleteProduct"] = result;
            return RedirectToAction("Products", "Admin");
        }

        public ActionResult Create()
        {
            int check = checkAccount();
            if (check == 0) return RedirectToAction("Login", "Account");
            if (check == -1) return RedirectToAction("Forbidden", "Error");
            List<SneakerType> sneakerTypes = ADOHelper.Instance.ExecuteReader<SneakerType>("select * from tbl_SneakerType");
            List<SelectListItem> SneakerTypes = new List<SelectListItem>();

            foreach (var item in sneakerTypes)
            {
                SneakerTypes.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
            }
            Session["SneakerTypes"] = SneakerTypes;
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc, List<HttpPostedFileBase> Images, List<HttpPostedFileBase> ImagesDetails)
        {
            string name, strPrice, strDiscount, strType;
            name = fc["Name"].ToString();
            strPrice = fc["Price"].ToString();
            strDiscount = fc["Discount"].ToString();
            bool kt = true;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(strPrice) || string.IsNullOrEmpty(strDiscount))
                kt = false;
            if (Images.Any(x => x == null))
                kt = false;
            if (kt)
            {
                name = Utilities.Instance.ChuanHoaChuoi(name);
                string id = Utilities.Instance.CreateIDSneaker(name);
                Sneaker sneaker = _SneakerService.GetSneaker(id);
                if(sneaker != null)
                {
                    ViewData["Error"] = "Sản phẩm đã tồn tại !!!";
                    return View();
                }
                strType = fc["SneakerTypes"].ToString();
                ADOHelper.Instance.ExecuteNonQuery(@"INSERT INTO tbl_Sneaker(ID, Name, Price, Discount, IDSneakerType)
                                                    VALUES (@para_0, @para_1, @para_2, @para_3, @para_4)", new object[] { id, name, strPrice, strDiscount, strType });


                string pathLink = Path.Combine(Server.MapPath(@"~\Content\img\Link\"), id);
                DirectoryInfo DIRLink = new DirectoryInfo(Path.Combine(pathLink));
                if(!DIRLink.Exists)
                {
                    DIRLink.Create();
                }
                pathLink += @"\\";
                foreach (var Image in Images)
                {
                    if (Image != null && Image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(Image.FileName);
                        var path = Path.Combine(pathLink, fileName);
                        Image.SaveAs(path);
                        ADOHelper.Instance.ExecuteNonQuery(@"INSERT INTO tbl_CoverImage
                                                                VALUES (@para_0, @para_1)", new object[] { id, fileName });
                    }
                }

                if (ImagesDetails.Count > 0)
                {
                    string pathDetails = Path.Combine(Server.MapPath(@"~\Content\img\Details\"), id);
                    DirectoryInfo DIRDetails = new DirectoryInfo(Path.Combine(pathDetails));
                    if (!DIRDetails.Exists)
                    {
                        DIRDetails.Create();
                    }
                    pathDetails += @"\\";
                    foreach (var Image in ImagesDetails)
                    {
                        if (Image != null && Image.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(Image.FileName);
                            var path = Path.Combine(pathDetails, fileName);
                            Image.SaveAs(path);
                            ADOHelper.Instance.ExecuteNonQuery(@"INSERT INTO tbl_DetailsImage
                                                                VALUES (@para_0, @para_1)", new object[] { id, fileName });
                        }
                    }
                }
                Session["AddProduct"] = "true";
                return RedirectToAction("Products", "Admin");
            }
            ViewData["Error"] = "Không được bỏ trống thông tin !!!";
            return View();
        }

        public ActionResult Update(string idSneaker)
        {
            //string pathLink = Path.Combine(@"C:\Users\HP\Máy tính\Thư mục mới (2)\1");
            //DirectoryInfo DIRLink = new DirectoryInfo(Path.Combine(pathLink));
            //if (DIRLink.Exists)
            //{
            //    DIRLink.Delete(true);
            //}
            List<SneakerType> sneakerTypes = ADOHelper.Instance.ExecuteReader<SneakerType>("select * from tbl_SneakerType");
            List<SelectListItem> SneakerTypes = new List<SelectListItem>();
            foreach (var item in sneakerTypes)
            {
                SneakerTypes.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
            }
            Session["SneakerTypes"] = SneakerTypes;
            Sneaker sneaker = _SneakerService.GetSneaker(idSneaker);
            return View(sneaker);
        }

        [HttpPost]
        public ActionResult Update(FormCollection fc, List<HttpPostedFileBase> Images, List<HttpPostedFileBase> ImagesDetails)
        {
            string name, strPrice, strDiscount, strType;
            name = fc["Name"].ToString();
            strPrice = fc["Price"].ToString();
            strDiscount = fc["Discount"].ToString();
            bool kt = true;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(strPrice) || string.IsNullOrEmpty(strDiscount))
                kt = false;
            //if (Images.Any(x => x == null))
            //    kt = false;

            name = Utilities.Instance.ChuanHoaChuoi(name);
            string id = Utilities.Instance.CreateIDSneaker(name);
            if (kt)
            {
                Sneaker sneaker = _SneakerService.GetSneaker(id);
                if (sneaker == null)
                {
                    ViewData["Error"] = "Sản phẩm không tồn tại !!!";
                    return View();
                }
                strType = fc["IDSneakerType"].ToString();
                ADOHelper.Instance.ExecuteNonQuery(@"UPDATE tbl_Sneaker
                                                    SET Name=@para_0, Price=@para_1, Discount=@para_2, IDSneakerType=@para_3
                                                    WHERE ID=@para_4", new object[] { name, strPrice, strDiscount, strType, id });
                if(Images != null)
                { 
                    if(Images.Count > 0 && Images.FirstOrDefault() != null)
                        ADOHelper.Instance.ExecuteNonQuery(@"delete tbl_CoverImage where IDSneaker = @para_0", new object[] { id });
                }
                if (ImagesDetails != null)
                {
                    if(ImagesDetails.Count > 0 && ImagesDetails.FirstOrDefault() != null)
                        ADOHelper.Instance.ExecuteNonQuery(@"delete tbl_DetailsImage where IDSneaker = @para_0", new object[] { id });
                }

                string pathLink = Path.Combine(Server.MapPath(@"~\Content\img\Link\"), id);
                DirectoryInfo DIRLink = new DirectoryInfo(Path.Combine(pathLink));
                if (!DIRLink.Exists)
                {
                    DIRLink.Create();
                }
                pathLink += @"\\";
                foreach (var Image in Images)
                {
                    if (Image != null && Image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(Image.FileName);
                        var path = Path.Combine(pathLink, fileName);
                        Image.SaveAs(path);
                        ADOHelper.Instance.ExecuteNonQuery(@"INSERT INTO tbl_CoverImage
                                                                VALUES (@para_0, @para_1)", new object[] { id, fileName });
                    }
                }

                if (ImagesDetails.Count > 0)
                {
                    string pathDetails = Path.Combine(Server.MapPath(@"~\Content\img\Details\"), id);
                    DirectoryInfo DIRDetails = new DirectoryInfo(Path.Combine(pathDetails));
                    if (!DIRDetails.Exists)
                    {
                        DIRDetails.Create();
                    }
                    pathDetails += @"\\";
                    foreach (var Image in ImagesDetails)
                    {
                        if (Image != null && Image.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(Image.FileName);
                            var path = Path.Combine(pathDetails, fileName);
                            Image.SaveAs(path);
                            ADOHelper.Instance.ExecuteNonQuery(@"INSERT INTO tbl_DetailsImage
                                                                VALUES (@para_0, @para_1)", new object[] { id, fileName });
                        }
                    }
                }
                Session["UpdateProduct"] = "true";
                return RedirectToAction("Products", "Admin");
            }
            ViewData["Error"] = "Không được bỏ trống thông tin !!!";
            return View(_SneakerService.GetSneaker(id));
        }

    }
}