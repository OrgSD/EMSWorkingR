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
    public class GenericInfoController : Controller
    {
        GenericInfoDAO genericInfoDAO = new GenericInfoDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/GenericInfo/
        public ActionResult frmGenericInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [HttpGet]
        public ActionResult GetGenericInfo()
        {
            var data = genericInfoDAO.GetGenericInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(GenericInfoBEL master)
        {
            try
            {
                if (genericInfoDAO.SaveUpdate(master))
                {
                    return Json(new { ID = genericInfoDAO.MaxID, Mode = genericInfoDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmGenericInfo");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }
	}
}