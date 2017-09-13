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
    public class CommercialInvoiceCreationController : Controller
    {
        CommercialInvoiceCreationDAO commercialInvoiceCreationDAO = new CommercialInvoiceCreationDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        //
        // GET: /Export/ComercialInvoiceCreation/
        public ActionResult frmCommercialInvoiceCreation()
        {
            //if (Session["UserID"] != null)
            //{
            //    return View();
            //}
            //return RedirectToAction("Index", "LoginRegistration");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPacking(string BuyerCode)
        {
            var data = commercialInvoiceCreationDAO.GetPackingList(BuyerCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPackingSearch()
        {
            var data = commercialInvoiceCreationDAO.GetPackingSearch();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProductSearch()
        {
            var productdata = commercialInvoiceCreationDAO.GetProductSearch();
            return Json(productdata, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetProduct(string PackNo)
        {
            var productdata = commercialInvoiceCreationDAO.GetProductList(PackNo);
            return Json(productdata, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetBuyer()
        {

            //Session["BuyerIDForQry"] = "'1'";
            var data = commercialInvoiceCreationDAO.GetBuyerList(Session["BuyerIDForQry"].ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSearch()
        {
            var data = commercialInvoiceCreationDAO.GetSearchData();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OperationsMode(CommercialInvoiceCreationBEL master)
        {
            try
            {
                if (commercialInvoiceCreationDAO.SaveUpdate(master, Session["UserID"].ToString()))
                {
                    return Json(new { ID = commercialInvoiceCreationDAO.MaxID, Mode = commercialInvoiceCreationDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmCommercialInvoiceCreation");
            }
            catch (Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
        }

    }
}