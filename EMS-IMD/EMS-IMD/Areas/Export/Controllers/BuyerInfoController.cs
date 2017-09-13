using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.Areas.Export.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EMS_IMD.Areas.Export.Controllers
{
    public class BuyerInfoController : Controller
    {
        BuyerInfoDAO primaryDAO = new BuyerInfoDAO();
        //
        // GET: /Export/BuyerInfo/
        public ActionResult frmBuyerInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetBuyer()
        {
            var data = primaryDAO.GetBuyerList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetBuyerForProforma()
        {

            var data = primaryDAO.GetBuyerListByUserID(Session["BuyerIDForQry"].ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetIndividualBuyer()
        {
            var data = primaryDAO.GetIndividualBuyer(Session["BuyerID"].ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult OperationsMode(BuyerInfoBEL master)
        {
            try
            {
                if (primaryDAO.SaveUpdate(master))
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
        public ActionResult GetBuyerForCombo()
        {

            var data = primaryDAO.GetBuyerListForCombo();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


	}
}