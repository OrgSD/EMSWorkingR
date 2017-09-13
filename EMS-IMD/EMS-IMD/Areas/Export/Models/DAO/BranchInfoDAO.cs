using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.DAL.Gateway;
using EMS_IMD.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.DAO
{
    public class BranchInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<BranchInfoBEL> GetBranchInfoList()
        {
            string Qry = "SELECT b.*,C.BANK_NAME from BRANCH_INFO b, BANK_INFO c where B.BANK_CODE = C.BANK_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BranchInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BranchInfoBEL
                    {
                        BankCode = row["BANK_CODE"].ToString(),
                        BankName = row["BANK_NAME"].ToString(),
                        BranchCode = row["BRANCH_CODE"].ToString(),
                        BranchName = row["BRANCH_NAME"].ToString(),
                        BranchAddress = row["ADDRESS"].ToString(),
                        Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public bool SaveUpdate(BranchInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.BranchCode == null || master.BranchCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("BRANCH_INFO", "BRANCH_CODE", "fm000");
                    IUMode = "I";
                    Qry = "INSERT INTO BRANCH_INFO (BRANCH_CODE,BRANCH_NAME,ADDRESS,BANK_CODE,STATUS) VALUES('" + MaxID + "', '" + master.BranchName + "' , '" + master.BranchAddress + "' , '" + master.BankCode + "' , '" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.BranchCode;
                    IUMode = "U";
                    Qry = "UPDATE BRANCH_INFO SET BRANCH_NAME = '" + master.BranchName + "', ADDRESS = '" + master.BranchAddress + "', BANK_CODE = '" + master.BankCode + "', STATUS = '" + master.Status + "' WHERE BRANCH_CODE = '" + master.BranchCode + "'";
                }
                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                {
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
    }
}