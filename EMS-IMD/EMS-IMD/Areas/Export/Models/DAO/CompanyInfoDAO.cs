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
    public class CompanyInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public List<CompanyInfoBEL> GetCompanyList()
        {
            string Qry = "SELECT COMPANY_CODE,COMPANY_NAME,ADDRESS, CONTACT_NO, EMAIL_ID from COMPANY_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<CompanyInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new CompanyInfoBEL
                    {
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        CompanyName = row["COMPANY_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString()
                    }).ToList();
            return item;
        }

        public bool SaveUpdate(CompanyInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.CompanyCode == null || master.CompanyCode.Length == 0)
                {
                    MaxID = idGenerated.getMAXID("COMPANY_INFO", "COMPANY_CODE", "fm000");
                    IUMode = "I";
                    Qry = "Insert into COMPANY_INFO(COMPANY_CODE,COMPANY_NAME, ADDRESS, EMAIL_ID, CONTACT_NO) Values('" + MaxID + "', '" + master.CompanyName + "', '" + master.Address + "', '" + master.EmailID + "','" + master.ContactNo + "')";
                }
                else
                {//U for Insert
                    MaxID = master.CompanyCode;
                    IUMode = "U";
                    Qry = "Update COMPANY_INFO set COMPANY_NAME='" + master.CompanyName + "', ADDRESS='" + master.Address + "', EMAIL_ID='" + master.EmailID + "', CONTACT_NO='" + master.ContactNo + "' Where COMPANY_CODE='" + master.CompanyCode + "'";
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