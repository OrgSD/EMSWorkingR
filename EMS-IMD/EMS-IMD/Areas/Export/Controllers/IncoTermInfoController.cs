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
    public class IncoTermInfoController : Controller
    {
        //
        // GET: /Export/IncoTermInfo/
        IncoTermInfoDAO incoTermInfoDAO = new IncoTermInfoDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/TransportMode/
        public ActionResult frmIncoTermInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetIncoTermInfo()
        {
            var data = incoTermInfoDAO.GetIncoTermInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(IncoTermInfoBEL master)
        {
            try
            {
                if (incoTermInfoDAO.SaveUpdate(master))
                {
                    return Json(new { ID = incoTermInfoDAO.MaxID, Mode = incoTermInfoDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmIncoTermInfo");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }
	}
}