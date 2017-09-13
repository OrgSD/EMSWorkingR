using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.Areas.Export.Models.DAO;
using EMS_IMD.DAL.Gateway;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS_IMD.Areas.Export.Controllers
{
    public class CurrencyInfoController : Controller
    {
       
        CurrencyInfoDAO currencyDAO = new CurrencyInfoDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/CurrencyInfo/
        public ActionResult frmCurrencyInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCurrency()
        {
            var data = currencyDAO.GetCurrencyList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }       

        [HttpPost]
        public ActionResult OperationsMode(CurrencyInfoBEL master)
        {
            try
            {
                if (currencyDAO.SaveUpdate(master))
                {
                    return Json(new { ID = currencyDAO.MaxID, Mode = currencyDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmCurrencyInfo");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }

	}
}