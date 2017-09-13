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
    public class BranchInfoController : Controller
    {
        BranchInfoDAO branchInfoDAO = new BranchInfoDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/BranchInfo/
        public ActionResult frmBranchInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetBranchInfo()
        {
            var data = branchInfoDAO.GetBranchInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(BranchInfoBEL master)
        {
            try
            {
                if (branchInfoDAO.SaveUpdate(master))
                {
                    return Json(new { ID = branchInfoDAO.MaxID, Mode = branchInfoDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmBankInfo");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }

	}
}