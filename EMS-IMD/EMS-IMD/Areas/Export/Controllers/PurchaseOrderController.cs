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
    public class PurchaseOrderController : Controller
    {
        PurchaseOrderDAO primaryDAO = new PurchaseOrderDAO();
        //
        // GET: /Export/PurchaseOrder/
        public ActionResult frmPurchaseOrder()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPOList()
        {

            var data = primaryDAO.GetPurchaseOrderList(Session["BuyerID"].ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetIndividualPO(string CompanyCode, string BuyerCode)
        {
            var data = primaryDAO.GetIndividualPOByCustomerBuyer(CompanyCode, BuyerCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult OperationsMode(PurchaseOrderMstBEL master)
        {
            try
            {
                if (primaryDAO.SaveUpdate(master, Session["UserID"].ToString()))
                {
                    return Json(new { ID = primaryDAO.MaxID, MaxCode = primaryDAO.MaxCode, Mode = primaryDAO.IUMode, Status = "Yes" });
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPurchaseDetails(string poNo, string companyCode)
        {
            var data = primaryDAO.GetProductList(poNo, companyCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPurchaseOrderDetails(string masterID)
        {
            var data = primaryDAO.GetPurchaseOrderDetails(masterID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPortListByTransportMode(string transportModeCode)
        {
            var data = primaryDAO.GetPortListByTransportMode(transportModeCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPurchaseOrderListForReport(string FromDate, string ToDate, string BuyerCode)
        {

            var data = primaryDAO.GetPurchaseOrderListForReport(FromDate, ToDate, BuyerCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

	}
}