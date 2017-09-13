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
    public class ShipmentInfoController : Controller
    {
        ShipmentInfoDAO primaryDAO = new ShipmentInfoDAO();

        //
        // GET: /Export/PostShipmentInfo/
        public ActionResult frmShipmentInfo()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "LoginRegistration");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCommercialInvoListForShipment()
        {
            var data = primaryDAO.GetCommercialInvoListForShipment();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetProformaListForShipment(string CommInvoiceFinalMstSlNo)
        {
            var data = primaryDAO.GetProformaListForShipment(CommInvoiceFinalMstSlNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetShipmentForRegulatory()
        {
            var data = primaryDAO.GetShipmentForRegulatory();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult OperationsMode(ShipmentMstBEL master)
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

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UploadBLDoc()
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

                        var fileName = "ShpIn_" + DateTime.Now.ToString("ddMMyyyy_hmm") + ".pdf";
                        var path = Path.Combine(Server.MapPath("~/UploadFiles/BLDoc"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        filePath = "~/UploadFiles/BLDoc/" + fileName;
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
        public async Task<JsonResult> UploadEXPDoc()
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

                        var fileName = "ShpIn_" + DateTime.Now.ToString("ddMMyyyy_hmm") + ".pdf";
                        var path = Path.Combine(Server.MapPath("~/UploadFiles/EXPDoc"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        filePath = "~/UploadFiles/EXPDoc/" + fileName;
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
        public async Task<JsonResult> UploadPaymentAdvice()
        {
            try
            {
                var filePath = "";

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        //fileName = "ShpIn_" + DateTime.Now.ToString("ddMMyyyy_hmm") + "_" + DocType + ".pdf";
                        var fileName = "ShpIn_" + DateTime.Now.ToString("ddMMyyyy_hmm") + ".pdf";
                        var path = Path.Combine(Server.MapPath("~/UploadFiles/PaymentAdvice"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        filePath = "~/UploadFiles/PaymentAdvice/" + fileName;
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
        public async Task<JsonResult> UploadOtherDoc(string DocType)
        {
            try
            {
                var filePath = "";

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        var fileName = "ShpIn_" + DateTime.Now.ToString("ddMMyyyy_hmm") + "_" + DocType + ".pdf";
                        var path = Path.Combine(Server.MapPath("~/UploadFiles/ShipOther"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        filePath = "~/UploadFiles/ShipOther/" + fileName;
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
    }
}