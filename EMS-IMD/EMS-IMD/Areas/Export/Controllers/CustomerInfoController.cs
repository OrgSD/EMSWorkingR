using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS_IMD.Areas.Export.Controllers
{
    public class CustomerInfoController : Controller
    {
        //
        // GET: /Export/CustomerInfo/
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }
	}
}