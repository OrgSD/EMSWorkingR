using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.Areas.Export.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EMS_IMD.Areas.Export.Controllers
{
    public class PriceTypeInfoController : Controller
    {
        PriceTypeInfoDAO primaryDAO = new PriceTypeInfoDAO();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPriceType()
        {
            var data = primaryDAO.GetPriceTypeComboList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}