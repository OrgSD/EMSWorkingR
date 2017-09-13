using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.Areas.Export.Models.DAO;
using EMS_IMD.DAL.Gateway;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace EMS_IMD.Areas.Export.Controllers
{
    public class CertificateofAnalysisController : Controller
    {
        CertificateofAnalysisDAO primaryDAO = new CertificateofAnalysisDAO();

        //
        // GET: /Export/CertificateofAnalysis/
        public ActionResult frmCertificateofAnalysis()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetPendingBatchNo(string PackingListSlNo)
        {
            var data = primaryDAO.GetPendingBatchNo(PackingListSlNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCOAList()
        {
            var data = primaryDAO.GetCOAList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> UploadCOA()
        {
            try
            {
                var filePath = "";

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        //var fileName = Path.GetFileName(file);

                        var fileName = "COA_" + DateTime.Now.ToString("ddMMyyyy_hmm") + ".pdf";
                        var path = Path.Combine(Server.MapPath("~/UploadFiles/COA"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                       filePath = "~/UploadFiles/COA/" + fileName;
                       filePath = filePath.Substring(1, filePath.Length - 1);
                      //  filePath = "/UploadFiles/COA/" + fileName;
                    }
                }
                return Json(filePath);
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }
        }


        [HttpPost]
        public ActionResult OperationsMode(COADetailsBEL master)
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

	}
}