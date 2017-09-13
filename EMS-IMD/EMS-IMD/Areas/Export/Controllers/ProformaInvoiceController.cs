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
    public class ProformaInvoiceController : Controller
    {
        ProformaInvoiceDAO primaryDAO = new ProformaInvoiceDAO();

        //
        // GET: /Export/ProformaInvoice/
        public ActionResult frmProformaInvoice()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [HttpPost]
        public ActionResult OperationsMode(ProformaInvoiceMstBEL master)
        {
            try
            {
                if (primaryDAO.SaveUpdate(master, Session["UserID"].ToString()))
                {
                    return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmRole");
            }
            catch (Exception e)
            {
                if (e.Message.Substring(0, 9) == "ORA-00001")
                    return Json(new { Status = "Error:ORA-00001,Data already exists!" });//Unique Identifier.
                else if (e.Message.Substring(0, 9) == "ORA-02292")
                    return Json(new { Status = "Error:ORA-02292,Data already exists!" });//Child Record Found.
                else if (e.Message.Substring(0, 9) == "ORA-12899")
                    return Json(new { Status = "Error:ORA-12899,Data Value Too Large!" });//Value Too Large.
                else
                    return Json(new { Status = "! Error : Error Code:" + e.Message.Substring(0, 9) });//Other Wise Error Found

            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProformaInvoiceList()
        {
            //Session["BuyerID"] = "1";

            var data = primaryDAO.GetProformaInvoiceList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetProformaInvoiceForPackingGrid(string buyerCode, string companyCode)
        {
            var data = primaryDAO.GetProformaInvoiceForPackingGrid(buyerCode, companyCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
		
		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetProductListForPackingByProformaID(string ProInvMstSlNo)
        {
            var data = primaryDAO.GetProductListForPackingByProformaID(ProInvMstSlNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProformaInvoiceListForReport(string BuyerCode, string CompanyCode, string FromDate, string ToDate)
        {
            var data = primaryDAO.GetProformaInvoiceListForReport(BuyerCode, CompanyCode, FromDate, ToDate);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

	}
}