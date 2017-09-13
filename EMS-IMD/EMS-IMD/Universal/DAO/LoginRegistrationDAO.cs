using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EMS_IMD.DAL.Gateway;
using EMS_IMD.Models;
using Systems.Universal;

namespace EMS_IMD.DAL.DAO
{

    public class LoginRegistrationDAO
    {
        
        DBConnection dbConn = new DBConnection();      
        DBHelper dbHelper = new DBHelper();
        public List<LoginRegistrationModel> CheckUserCredential()
        {

            HttpContext.Current.Session["Conn"] = dbConn.SAConnStrReader();
            //string Qry = "SELECT UserID,NewPassword,OldPassword FROM  Sa_UserCredential";
            string Qry = "SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,"+
           // " --dbHR.dbo.GetName(ur.EmpID,'ER') EmpName,dbHR.dbo.GetName(ur.EmpID,'EE') SupervisorID,dbHR.dbo.GetName(dbHR.dbo.GetName(ur.EmpID,'EE'),'ER') SupervisorName,dbHR.dbo.GetName(ur.EmpID,'EE d') Designation,dbHR.dbo.GetName(ur.EmpID,'EE ed') EmploymentDate,"+
            " u.NewPassword,u.OldPassword,ur.IsActive FROM Sa_UserInRole ur, Sa_UserCredential u,Sa_Role r Where ur.UserID=u.UserID and ur.RoleID=r.RoleID and upper(ur.IsActive)=upper('true') ";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<LoginRegistrationModel> item;

            item = (from DataRow row in dt.Rows
                    select new LoginRegistrationModel
                    {
                        UserID = row["UserID"].ToString(),
                        Password = row["NewPassword"].ToString(),
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),
                        EmpID = row["EmpID"].ToString(),
                        //EmpName = row["EmpName"].ToString(),
                        //SupervisorID = row["SupervisorID"].ToString(),
                        //SupervisorName = row["SupervisorName"].ToString(),
                        //Designation = row["Designation"].ToString(),
                        //EmploymentDate = row["EmploymentDate"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;



        }
        public List<LoginRegistrationModel> ValidUserDefaults(string UserID)
        {
            string Qry = "SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,GetName(ur.EmpID,'EM') EmpName,dbHR.dbo.GetName(ur.EmpID,'EE') SupervisorID,dbHR.dbo.GetName(dbHR.dbo.GetName(ur.EmpID,'EE'),'ER') SupervisorName,u.NewPassword,u.OldPassword,ur.IsActive FROM Sa_UserInRole as ur, Sa_UserCredential as u,Sa_Role r Where ur.UserID=u.UserID and ur.RoleID=r.RoleID and ur.IsActive='1' and ur.UserID='"+UserID+"'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<LoginRegistrationModel> item;

            item = (from DataRow row in dt.Rows
                    select new LoginRegistrationModel
                    {
                        UserID = row["UserID"].ToString(),
                        Password = row["NewPassword"].ToString(),
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),                                         
                        EmpID = row["EmpID"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        SupervisorID = row["SupervisorID"].ToString(),
                        SupervisorName = row["SupervisorName"].ToString(),                     
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;            
        }

        public bool MenuPopulate(string UserID)
        {
            if (dbHelper.ProcedureExecuteFn1(dbConn.SAConnStrReader(), "", "Sa_Menu_SP", "pUserID", UserID))
            {                    
                return true;
            }
            return false;
        }

        public void EmpBuyerMappingDetails(string EmpID, string EmpName)
        {
           
            HttpContext.Current.Session["IsEmpBuyerMapping"] = "No";
            string strID = "";
            string strName = "";
            string strIDForQry = "";
            string Qry = "Select BUYER_ID,GetName(BUYER_ID,'BR') Buyer  from SA_EMP_BUYER_MAPPING Where EMP_ID='" + EmpID + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);

           if (dt.Rows.Count>0)
           {
            HttpContext.Current.Session["IsEmpBuyerMapping"] = "Yes";
            if (dt.Rows.Count > 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        strID = strID + dt.Rows[i][0].ToString();
                        strName = strName + dt.Rows[i][1].ToString();
                        strIDForQry = strIDForQry + dt.Rows[i][0].ToString() + "'";
                     
                    }
                    else
                    {                      
                        strID = strID + dt.Rows[i][0].ToString()+",";
                        strName = strName + dt.Rows[i][1].ToString() + ",";
                        strIDForQry = strIDForQry + "'" + dt.Rows[i][0].ToString() + "','";
                    }
                }
            }
            if (dt.Rows.Count == 1)
            {
                strID = strID  + dt.Rows[0][0].ToString();
                strName = strName + dt.Rows[0][1].ToString();
                strIDForQry = strIDForQry + "'" + dt.Rows[0][0].ToString() + "'";
            }
           }
          if(strID!="" && strID!=null)
          {
              HttpContext.Current.Session["BuyerID"] = strID;
              HttpContext.Current.Session["BuyerName"] = strName;
              HttpContext.Current.Session["BuyerIDForQry"] = strIDForQry;
          }
          else
           {
               HttpContext.Current.Session["BuyerID"] = EmpID;
               HttpContext.Current.Session["BuyerName"] = EmpName;
               HttpContext.Current.Session["BuyerIDForQry"] = "'" + strIDForQry + "'";

           }         

        }







    }
}
