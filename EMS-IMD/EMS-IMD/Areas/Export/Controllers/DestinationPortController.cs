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
    public class DestinationPortController : Controller
    {
        DestinationPortDAO destinationPortDAO = new DestinationPortDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/DestinationPort/
        public ActionResult frmDestinationPort()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCountry()
        {
            var data = destinationPortDAO.GetCountryList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTransportMode()
        {
            var data = destinationPortDAO.GetTransportModeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDestinationPort()
        {
            var data = destinationPortDAO.GetDestinationPortList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(DestinationPortBEL master)
        {
            try
            {
                if (destinationPortDAO.SaveUpdate(master))
                {
                    return Json(new { ID = destinationPortDAO.MaxID, Mode = destinationPortDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmDestinationPort");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }
	}
}