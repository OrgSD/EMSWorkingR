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
    public class TherapeuticClassInfoController : Controller
    {
        
        TherapeuticClassInfoDAO theraputicClassInfoDAO = new TherapeuticClassInfoDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/TherapeuticClassInfo/
        public ActionResult frmTherapeuticClassInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTherapeuticInfo()
        {
            var data = theraputicClassInfoDAO.GetTherapeuticInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(TherapeuticClassInfoBEL master)
        {
            try
            {
                if (theraputicClassInfoDAO.SaveUpdate(master))
                {
                    return Json(new { ID = theraputicClassInfoDAO.MaxID, Mode = theraputicClassInfoDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmTherapeuticClassInfo");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }
       
	}
}