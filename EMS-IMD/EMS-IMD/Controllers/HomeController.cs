using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS_IMD.DAL.Gateway;
using EMS_IMD.Models;
using EMS_IMD.Universal.Gateway;
using Systems.Universal;
using Systems.Controllers;

namespace EMS_IMD.Controllers
{
    public class HomeController : ControllerController
    {
       
        public ActionResult frmHome()
        {
            if (Session["UserID"] != null)
            {          
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }
  
    }
       

}