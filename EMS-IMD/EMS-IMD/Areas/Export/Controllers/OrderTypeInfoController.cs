using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.Areas.Export.Models.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EMS_IMD.Areas.Export.Controllers
{
    public class OrderTypeInfoController : Controller
    {
        OrderTypeInfoDAO primaryDAO = new OrderTypeInfoDAO();

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetOrderType()
        {
            var data = primaryDAO.GetOrderTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

       
	}
}