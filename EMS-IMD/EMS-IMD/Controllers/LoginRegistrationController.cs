using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS_IMD.DAL.DAO;
using EMS_IMD.Models;
using EMS_IMD.DAL.Gateway;
using Systems.Universal;

namespace EMS_IMD.Controllers
{
    public class LoginRegistrationController : Controller
    {
        LoginRegistrationDAO loginRegistrationDAO = new LoginRegistrationDAO();
        ExceptionHandler exceptionHandler = new ExceptionHandler();
        Encryption encryption = new Encryption();
       
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckUser(LoginRegistrationModel master)
        {
            try
            {
                if ((master.UserID != null) && (master.Password != null))
                {
                   // var verifiedUserCredential = loginRegistrationDAO.CheckUserCredential().Where(m => m.UserID.Equals(master.UserID) && m.Password.Equals(master.Password)).FirstOrDefault();
                    var verifiedUserCredential = loginRegistrationDAO.CheckUserCredential().Where(m => m.UserID.Equals(encryption.Encrypt(master.UserID)) && m.Password.Equals(encryption.Encrypt(master.Password))).FirstOrDefault();

                    if (verifiedUserCredential != null)
                    {
                        Session["UserID"] = verifiedUserCredential.UserID;
                        Session["enUserID"] = verifiedUserCredential.UserID;
                        Session["deUserID"] = encryption.Decrypt(verifiedUserCredential.UserID);
                        Session["RoleID"] = verifiedUserCredential.RoleID;
                        Session["RoleName"] = verifiedUserCredential.RoleName;
                        Session["EmpID"] = verifiedUserCredential.EmpID;
                        Session["EmpName"] = verifiedUserCredential.EmpID;//verifiedUserCredential.EmpName;

                        //Session["SupervisorID"] = verifiedUserCredential.SupervisorID;
                        //Session["SupervisorName"] = verifiedUserCredential.SupervisorName;
                        //Session["Designation"] = verifiedUserCredential.Designation;
                        //Session["EmploymentDate"] = verifiedUserCredential.EmploymentDate;


                        loginRegistrationDAO.EmpBuyerMappingDetails(verifiedUserCredential.EmpID, verifiedUserCredential.EmpName);

                 

                        bool IsTrue = loginRegistrationDAO.MenuPopulate(verifiedUserCredential.UserID);


                        string b = Session["BuyerID"].ToString();
                        return Json(new { Status = "success" });
                    }
                }

            }
            catch(Exception e)
            {
                return exceptionHandler.ErrorMsg(e);
            }
            return Json(new { Status = "failed" });
        }

       
    }
}
