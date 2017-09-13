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
    public class CommercialInvoiceFinalizeController : Controller
    {
        CommercialInvoiceFinalizeDAO commercialInvoiceFinalizeDAO = new CommercialInvoiceFinalizeDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/ComercialInvoiceFinalize/
        public ActionResult frmCommercialInvoiceFinalize()
        {
            //if (Session["UserID"] != null)
            //{
            //    return View();
            //}
            //return RedirectToAction("Index", "LoginRegistration");
            return View();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult InvoiceInfo()
        {
           // Session["BuyerIDForQry"] = "'1'";
            var data = commercialInvoiceFinalizeDAO.GetInvoiceInfo(Session["BuyerIDForQry"].ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult InvoiceInfoSearch()
        {
            //Session["BuyerIDForQry"] = "'1'";
            var data = commercialInvoiceFinalizeDAO.InvoiceInfoSearch(Session["BuyerIDForQry"].ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProductInfoSearch()
        {
            var data = commercialInvoiceFinalizeDAO.GetProductInfoSearch();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProductInfo(string CommInvoiceNo)
        {
            var data = commercialInvoiceFinalizeDAO.GetProductInfo(CommInvoiceNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(CommercialInvoiceFinalizeBEL master)
        {
            try
            {
                if (commercialInvoiceFinalizeDAO.SaveUpdate(master))
                {
                    return Json(new { ID = commercialInvoiceFinalizeDAO.MaxID, Mode = commercialInvoiceFinalizeDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmCommercialInvoiceFinalize");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }

	}
}