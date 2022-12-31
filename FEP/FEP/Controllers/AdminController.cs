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
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(strPrice) || string.IsNullOrEmpty(strDiscount) || Images.Count <= 0)
                kt = false;
            if (kt)
            {
                name = Utilities.Instance.ChuanHoaChuoi(name);
                string id = Utilities.Instance.CreateIDSneaker(name);
                Sneaker sneaker = _SneakerService.GetSneaker(id);
                if(sneaker != null)
                {
                    ViewData["Error"] = "Có rồi";
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
                }

                if(ImagesDetails.Count > 0)
                {
                    string pathDetails = Path.Combine(Server.MapPath(@"~\Content\img\Details\"), id);
                    DirectoryInfo DIRDetails = new DirectoryInfo(Path.Combine(pathDetails));
                    if (!DIRDetails.Exists)
                    {
                        DIRDetails.Create();
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
                }
                Session["AddProduct"] = "true";
                return RedirectToAction("Products", "Admin");
            }
            ViewData["Error"] = "Rỗng";
            return View();
        }

        public ActionResult Update(string idSneaker)
        {
            return View();
        }

    }
}