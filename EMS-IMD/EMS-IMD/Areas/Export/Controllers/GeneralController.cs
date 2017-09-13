using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.Areas.Export.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EMS_IMD.Areas.Export.Controllers
{
    public class GeneralController : Controller
    {
        GeneralDAO primaryDAO = new GeneralDAO();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPriceType()
        {
            var data = primaryDAO.GetPriceTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetOrderType()
        {
            var data = primaryDAO.GetOrderTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetBranchByBank(string bankCode)
        {
            var data = primaryDAO.GetBranchByBank(bankCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetDocumentType(string formType)
        {
            var data = primaryDAO.GetDocumentType(formType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


	}
}