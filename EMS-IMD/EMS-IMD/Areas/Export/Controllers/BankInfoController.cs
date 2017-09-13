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
    public class BankInfoController : Controller
    {
        BankInfoDAO bankInfoDAO = new BankInfoDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/BankInfo/
        public ActionResult frmBankInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetBankInfo()
        {
            var data = bankInfoDAO.GetBankInfoList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(BankInfoBEL master)
        {
            try
            {
                if (bankInfoDAO.SaveUpdate(master))
                {
                    return Json(new { ID = bankInfoDAO.MaxID, Mode = bankInfoDAO.IUMode, Status = "Yes" });
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