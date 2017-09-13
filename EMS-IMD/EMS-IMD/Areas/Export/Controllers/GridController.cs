using EMS_IMD.Areas.SA.Models.BEL;
using EMS_IMD.Areas.SA.Models.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS_IMD.Areas.Export.Controllers
{
    public class GridController : Controller
    {
        ModuleDAO primaryDAO = new ModuleDAO();
        // GET: /Export/Grid/
        public ActionResult frmGrid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetModule()
        {
            var data = primaryDAO.GetModuleList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModule2()
        {
            List<ModuleBEL> getAllLot = primaryDAO.GetModuleList(); 
            return Json(getAllLot, JsonRequestBehavior.AllowGet);
        }
	}
}