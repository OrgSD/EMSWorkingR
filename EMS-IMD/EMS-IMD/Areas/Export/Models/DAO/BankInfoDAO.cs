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
    public class BankInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<BankInfoBEL> GetBankInfoList()
        {
            string Qry = "SELECT * from BANK_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BankInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BankInfoBEL
                    {
                        BankCode = row["BANK_CODE"].ToString(),
                        BankName = row["BANK_NAME"].ToString(),
                        BankAddress = row["ADDRESS"].ToString(),
                        Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public bool SaveUpdate(BankInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.BankCode == null || master.BankCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("BANK_INFO", "BANK_CODE", "fm000");
                    IUMode = "I";
                    Qry = "INSERT INTO BANK_INFO (BANK_CODE,BANK_NAME,ADDRESS,STATUS) VALUES('" + MaxID + "', '" + master.BankName + "' , '" + master.BankAddress + "' , '" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.BankCode;
                    IUMode = "U";
                    Qry = "UPDATE BANK_INFO SET BANK_NAME = '" + master.BankName + "', ADDRESS = '" + master.BankAddress + "',STATUS = '" + master.Status + "' WHERE BANK_CODE = '" + master.BankCode + "'";
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