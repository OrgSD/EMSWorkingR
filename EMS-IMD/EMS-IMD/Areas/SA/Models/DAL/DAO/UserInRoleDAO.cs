using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EMS_IMD.Areas.SA.Models.BEL;
using EMS_IMD.DAL.Gateway;
using Systems.Universal;

namespace EMS_IMD.Areas.SA.Models.DAL.DAO
{
    public class UserInRoleDAO : ReturnData
    {
        DBConnection  dbConn=new DBConnection();
        Encryption encryption = new Encryption();
        // SaHelper saHelper = new SaHelper();
        DBHelper saHelper = new DBHelper();
        public bool SaveUpdate(UserInRoleBEL master)
        {
            try
            {
                string Qry = "Select MAX(UserID) ID from Sa_UserInRole";
                var tuple = saHelper.ProcedureExecuteTFn5(dbConn.SAConnStrReader(), Qry, "Sa_UserInRole_SSP", "p_RoleID", "p_UserID", "p_EmpID", "p_NewPassword", "p_IsActive", master.RoleID, encryption.Encrypt(master.UserID), master.EmpID, encryption.Encrypt(master.Password), master.IsActive.ToString());
                             
                if (tuple.Item1)
                {
                    MaxID = tuple.Item2;
                    IUMode = tuple.Item3;                    
                    if (master.BuyerID != "" && master.BuyerID != null)
                    {
                    EmpBuyerMapping(master);
                    }
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }

        private Boolean EmpBuyerMapping(UserInRoleBEL master)
        {
            bool isTrue = false;
            string QryDelete = "Delete from SA_EMP_BUYER_MAPPING Where Emp_ID='" + master.EmpID + "'";
            if (saHelper.CmdExecute(dbConn.SAConnStrReader(), QryDelete))
            {                   
            string Str = master.BuyerID.Replace("[", "").Replace("]", "").Replace("\"", "");
            String[] SubStr = Str.Split(',');
            for (int i = 0; i < SubStr.Length; i ++)
            {
                string Qry = "Insert Into SA_EMP_BUYER_MAPPING(Buyer_ID,Emp_ID) Values ('" + SubStr[i].ToString() + "','" + master.EmpID + "')";          
                if(saHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                {
                 isTrue = true;
                }
                }
            }
            else
            {
                string Str = master.BuyerID.Replace("[", "").Replace("]", "").Replace("\"", "");
                String[] SubStr = Str.Split(',');
                for (int i = 0; i < SubStr.Length; i++)
                {
                    string Qry = "Insert Into SA_EMP_BUYER_MAPPING(Buyer_ID,Emp_ID) Values ('" + SubStr[i].ToString() + "','" + master.EmpID + "')";
                    if (saHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {
                        isTrue = true;
                    }
                }
            }

            return isTrue;          
        }

        public List<UserInRoleBEL> GetUserInRoleList()
        {
            string Qry = "SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,GetName(ur.EmpID,'EM') EmpName,u.NewPassword,u.OldPassword,ur.IsActive FROM Sa_UserInRole ur, Sa_UserCredential u,Sa_Role r Where ur.UserID=u.UserID and ur.RoleID=r.RoleID and ur.RoleID>='" + HttpContext.Current.Session["RoleID"].ToString() + "' Order by ur.RoleID,ur.EmpID";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),
                        UserID = encryption.Decrypt(row["UserID"].ToString()),                     
                        EmpID = row["EmpID"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        Password = encryption.Decrypt(row["NewPassword"].ToString()),                        
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;
        }

        public List<UserInRoleBEL> GetEmployeeList()
        {
            string Qry = "Select EmpID,EmpName From Sa_Employee -- where Upper(STATUS)=Upper('true')";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        EmpID = row["EmpID"].ToString(),
                        EmpName = row["EmpName"].ToString()
                       

                    }).ToList();
            return item;
        }
        public List<UserInRoleBEL> GetEmployeeNotYetAssignedList()
        {
            string Qry = "Select a.EmpID,a.EmpName,a.Type,a.YetAssigned from "+
                        " (Select b.EmpID,b.EmpName ,b.Type,'false' YetAssigned from(Select EmpID,EmpName,'EM' Type From Sa_Employee Union all Select Buyer_Code EmpID,Buyer_Name EmpName,'BR' Type from BUYER_INFO Where Upper(STATUS)=Upper('Active')) b where b.EmpID Not in (Select EmpID from Sa_UserInRole Where Upper(isActive)=Upper('true')) " +
                        " Union all Select EmpID,GetName(EmpID,'EM') EmpName,DECODE(Length(EmpID),5,'EM','BR') Type, 'true' YetAssigned From Sa_UserInRole Where Upper(isActive)=Upper('true') ) a Order by  CASE WHEN upper(a.YetAssigned) = upper('false') THEN 1 ELSE 2 END,CASE WHEN a.Type='EM' THEN 1 ELSE 2 END, a.YetAssigned,a.EmpName ";
          
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        EmpID = row["EmpID"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        DataOwner = row["Type"].ToString(),
                        YetAssigned = row["YetAssigned"].ToString()

                    }).ToList();
            return item;
        }

        public List<UserInRoleBEL> GetBuyerList()
        {
            string Qry = "Select BUYER_CODE,BUYER_NAME From BUYER_INFO Where Upper(STATUS) = Upper('Active')";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        BuyerID = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString()


                    }).ToList();
            return item;
        }
       public List<UserInRoleBEL> GetUserList()
        {
            string Qry = "Select UserID,EmpID,GetName(EmpID,'EM') EmpName From Sa_UserInRole Where Upper(IsActive)=Upper('true')";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        UserID = encryption.Decrypt(row["UserID"].ToString()),
                        EmpName = row["EmpName"].ToString()


                    }).ToList();
            return item;
        }

       public List<UserInRoleBEL> GetBuyerYetAssignedList(string EmpID)
       {
           string Qry =" select a.BUYER_ID,a.BuyerName,a.YetAssigned from (Select BUYER_ID,GetName(BUYER_ID,'BR') BuyerName,'true' YetAssigned  From SA_EMP_BUYER_MAPPING where Emp_ID='" + EmpID + "' Union all "+
                       " Select BUYER_CODE BUYER_ID,Buyer_Name BuyerName ,'false' YetAssigned From Buyer_Info where BUYER_CODE Not In (Select distinct BUYER_ID SA_EMP_BUYER_MAPPING from SA_EMP_BUYER_MAPPING where Emp_ID='" + EmpID + "')) a "+
                       " Order by  CASE WHEN Upper(a.YetAssigned) = upper('true') THEN 1 ELSE 2 END, a.YetAssigned,a.BuyerName ";
           
    
           DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
           List<UserInRoleBEL> item;

           item = (from DataRow row in dt.Rows
                   select new UserInRoleBEL
                   {
                       BuyerID = row["BUYER_ID"].ToString(),
                       BuyerName = row["BuyerName"].ToString(),
                       YetAssigned = row["YetAssigned"].ToString()

                   }).ToList();
           return item;
       }
    }
}