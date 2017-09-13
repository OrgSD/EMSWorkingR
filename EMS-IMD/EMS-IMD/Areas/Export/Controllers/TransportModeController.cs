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
    public class TransportModeController : Controller
    {
        TransportModeDAO transportModeDAO = new TransportModeDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/TransportMode/
        public ActionResult frmTransportMode()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTransportMode()
        {
            var data = transportModeDAO.GetTransportModeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(TransportModeBEL master)
        {
            try
            {
                if (transportModeDAO.SaveUpdate(master))
                {
                    return Json(new { ID = transportModeDAO.MaxID, Mode = transportModeDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmTransportMode");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }

	}
}