﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FEP.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Forbidden()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
    }
}