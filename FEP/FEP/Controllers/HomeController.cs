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
using FEP.Utility;
using System.Data.SqlClient;
using System.Data;

namespace FEP.Controllers
{
    public class HomeController : Controller
    {
        static ISneakerData _NHibernateData = new NHibernateData();
        static SneakerService _SneakerService = new SneakerService(_NHibernateData);
        static int IDTypeNike = _SneakerService.GetIDSneakerType("Nike");
        static int IDTypeAdidas = _SneakerService.GetIDSneakerType("Adidas");
        static int IDTypeLuxury = _SneakerService.GetIDSneakerType("Luxury");
        static int IDTypeMLB = _SneakerService.GetIDSneakerType("MLB");
        static int IDTypeDep = _SneakerService.GetIDSneakerType("Dép");
        static CartService _CartService = new CartService();
        static SneakerManagementAPIController api = new SneakerManagementAPIController();

        // GET: Home
        public ActionResult FEP()
        {
            List<Sneaker> _Sneakers = _SneakerService.getAll();
            ViewBag.Nikes = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeNike).Take(8).ToList();
            ViewBag.Adidas = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeAdidas).Take(4).ToList();
            ViewBag.MLBs = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeMLB).Take(4).ToList();
            ViewBag.Deps = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeDep).Take(4).ToList();
            ViewBag.Luxurys = _Sneakers.FindAll(x => x.IDSneakerType == IDTypeLuxury).Take(3).ToList();


            Session["IDTypeNike"] = IDTypeNike;
            Session["IDTypeAdidas"] = IDTypeAdidas;
            Session["IDTypeLuxury"] = IDTypeLuxury;
            Session["IDTypeMLB"] = IDTypeMLB;
            Session["IDTypeDep"] = IDTypeDep;
            return View();
        }

        public ActionResult Products(int? page, int idSneakerType)
        {
            Session["IDTypeNike"] = IDTypeNike;
            Session["IDTypeAdidas"] = IDTypeAdidas;
            Session["IDTypeLuxury"] = IDTypeLuxury;
            Session["IDTypeMLB"] = IDTypeMLB;
            Session["IDTypeDep"] = IDTypeDep;
            var pageNumber = (page ?? 1);
            ViewBag.pageSize = 8;
            var sneaker = _SneakerService.getAll().FindAll(x => x.IDSneakerType == idSneakerType).ToList();
            ViewBag.idSneakerType = idSneakerType;
            return View(sneaker.ToPagedList((int)pageNumber, (int)ViewBag.pageSize));
        }

        public ActionResult ProductDetails(string idSneaker)
        {
            List<Sneaker> _Sneakers = _SneakerService.getAll();
            List<int> sizeInventory = _SneakerService.GetSizeInventory(idSneaker);
            Sneaker s = _Sneakers.Find(x => x.ID == idSneaker);
            if(!_SneakerService.CheckInventory(sizeInventory))
            {
                return RedirectToAction("Error", "Error", new { sneaker = s });
            }
            List<int> inventory = _SneakerService.GetInventory(idSneaker);

            ViewBag.Sneaker = s;
            ViewBag.SPTT = _Sneakers.FindAll(x => x.IDSneakerType == s.IDSneakerType).Take(8).ToList();
            List<string> detailsPic = _SneakerService.GetDetailsPicture(idSneaker);
            if(detailsPic == null || detailsPic.Count == 0)
            {
                detailsPic = _SneakerService.GetCoverPicture(idSneaker);
            }
            ViewBag.DetailsImage = detailsPic;
            ViewBag.Sizes = _SneakerService.GetSizes();
            ViewBag.InventorySize = sizeInventory;
            ViewBag.Inventory = inventory;
            ViewBag.SneakerService = _SneakerService;
            return View();
        }

        public ActionResult Payment(int idClient)
        {
            List<Sneaker> _Sneakers = _SneakerService.getAll();
            List<Cart> carts = Session["ToPay"] as List<Cart>;
            Session["PayList"] = carts;
            Session["ListSneaker"] = _Sneakers;
            Session["Client"] = api.GetClient(idClient);
            Session["City"] = HelperData.Instance.GetCities();
            Session["District"] = HelperData.Instance.GetDistricts();
            Session["Ward"] = HelperData.Instance.GetWards();
            return View();
        }
        [HttpPost]
        public ActionResult Payment(FormCollection fc)
        {
            int idWard;
            string addressDetails = fc["address"];
            string name, phone;
            name = fc["Name"];
            phone = fc["Phone"];
            if (!int.TryParse(fc["Ward"], out idWard) || string.IsNullOrEmpty(addressDetails) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || !Utilities.Instance.CheckPhone(phone))
            {
                ViewData["Error"] = "Lỗi";
                return View();
            }
            List<Cart> list = Session["PayList"] as List<Cart>;
            if(list.Count <= 0)
            {
                return RedirectToAction("Error", "Error");
            }    
            int idBill = 0;
            int idClient = list.FirstOrDefault().IDClient;
            foreach (var item in list)
            {
                if(idBill == 0)
                {
                    try
                    {
                        idBill = ADOHelper.Instance.ExecuteScalar(@"declare @idBill int
                                exec sp_InsertBill @idClient = @para_0, @iDWardTransport = @para_1, @addressDetails = @para_2, @name = @para_3, @phone = @para_4, @idBill = @idBill output
                                select @idBill as 'IDBILL'", new object[] { idClient, idWard, addressDetails, name, phone });
                    }
                    catch
                    {
                        idBill = 0;
                    }
                }
                if(idBill > 0)
                {
                    try
                    {
                        ADOHelper.Instance.ExecuteNonQuery("exec sp_InsertBillDetails @idBill=@para_0, @idSize=@para_1, @idSneaker=@para_2, @amountBuy=@para_3", new object[] { idBill, item.IDSize, item.IDSneaker, item.AmountBuy });
                        _CartService.DeleteCart(idClient, item.IDSneaker, item.IDSize);
                    }
                    catch
                    {
                        ADOHelper.Instance.ExecuteNonQuery("exec sp_DeteleBill @idBill = @para_0", new object[] { idBill });
                        idBill = 0;
                    }
                }    
            }
            
            return RedirectToAction("Bill", "User", new { idClient=idClient });
        }
        public ActionResult PaymentOne(int idClient, string idSneaker, int idSize)
        {
            List<Cart> carts = new List<Cart>();
            Cart cart = _CartService.GetCarts().SingleOrDefault(x => x.IDClient == idClient && x.IDSize == idSize && x.IDSneaker == idSneaker);
            if (cart != null)
                carts.Add(cart);
            if (carts.Count <= 0)
                return RedirectToAction("Error", "Error");
            Session["ToPay"] = carts;
            return RedirectToAction("Payment", "Home", new { idClient = idClient });
        }
        public ActionResult PaymentList(int idClient)
        {
            List<Cart> carts = _CartService.GetCarts().Where(x => x.IDClient == idClient).ToList();
            int countRemove = carts.RemoveAll(x => _SneakerService.CheckAmountInventory(x.IDSneaker, _SneakerService.GetSize(x.IDSize), x.AmountBuy) == false);
            if (carts.Count <= 0)
                return RedirectToAction("Error", "Error");
            Session["ToPay"] = carts;
            if (countRemove > 0)
                Session["Remove"] = countRemove;
            return RedirectToAction("Payment", "Home", new { idClient = idClient });
        }
        public ActionResult PaymentNow(FormCollection fc)
        {
            string idUser = fc["idclient"];
            string idSneaker = fc["idsneaker"];
            string size = fc["size"];
            string amount = fc["amount"];
            if (string.IsNullOrEmpty(idUser) || api.GetClient(int.Parse(idUser)) == null)
            {
                Session["idsneaker"] = idSneaker;
                return RedirectToAction("Login", "Account");
            }
            if (_SneakerService.CheckAmountInventory(idSneaker, int.Parse(size), int.Parse(amount)))
            {
                Cart cart = new Cart(int.Parse(idUser), idSneaker, _SneakerService.GetIDSize(int.Parse(size)), int.Parse(amount));
                List<Cart> carts = new List<Cart>();
                carts.Add(cart);
                Session["ToPay"] = carts;
                return RedirectToAction("Payment", "Home", new { idClient = int.Parse(idUser) });
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }
           
        }

        public ActionResult Search(string txtSearch)
        {
            if (txtSearch == null)
                return RedirectToAction("Error", "Error404");
            List<Sneaker> sneakers = _SneakerService.getAll();
            List<Sneaker> sneakersSearch = sneakers.Where(x => x.ID.ToString().ToUpper().Contains(txtSearch.ToUpper()) || x.Name.ToString().ToUpper().Contains(txtSearch.ToUpper())).ToList();
            Session["SneakersSearch"] = sneakersSearch;
            return RedirectToAction("SearchPages", "Home");
        }

        public ActionResult SearchPages(int? page)
        {
            var pageNumber = (page ?? 1);
            ViewBag.pageSize = 8;
            List<Sneaker> sneakers = Session["SneakersSearch"] as List<Sneaker>;
            if (sneakers == null)
                return RedirectToAction("Error", "Error");
            Session["SneakersSearch"] = sneakers;
            return View(sneakers.ToPagedList((int)pageNumber, (int)ViewBag.pageSize));
        }
    }
}