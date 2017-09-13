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
    public class ConsigneeInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();


        public List<ConsigneeInfoBEL> GetConsigneeList()
        {
            string Qry = "SELECT A.BUYER_CODE, B.BUYER_NAME, A.CONSIGNEE_CODE, A.CONSIGNEE_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO , A.STATUS " +
                         "from  CONSIGNEE_INFO A, BUYER_INFO B " +
                         "WHERE A.BUYER_CODE = B.BUYER_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ConsigneeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ConsigneeInfoBEL
                    {
                        ConsigneeCode = row["CONSIGNEE_CODE"].ToString(),
                        ConsigneeName = row["CONSIGNEE_NAME"].ToString(),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        Status = row["STATUS"].ToString(),                        
                    }).ToList();
            return item;
        }

        public List<ConsigneeInfoBEL> GetActiveConsigneeList()
        {
            string Qry = "SELECT A.BUYER_CODE, B.BUYER_NAME, A.CONSIGNEE_CODE, A.CONSIGNEE_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO , A.STATUS " +
                         "from  CONSIGNEE_INFO A, BUYER_INFO B " +
                         "WHERE A.BUYER_CODE = B.BUYER_CODE " +
                         "AND A.STATUS = 'Active'"; 
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ConsigneeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ConsigneeInfoBEL
                    {
                        ConsigneeCode = row["CONSIGNEE_CODE"].ToString(),
                        ConsigneeName = row["CONSIGNEE_NAME"].ToString(),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ConsigneeInfoBEL> GetConsigneeByBuyer(string BuyerCode)
        {
            string Qry = "SELECT A.CONSIGNEE_CODE, A.CONSIGNEE_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO , A.STATUS " +
                         "from  CONSIGNEE_INFO A " +
                         "WHERE A.BUYER_CODE = '" + BuyerCode + "' " +
                         "AND A.STATUS = 'Active'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ConsigneeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ConsigneeInfoBEL
                    {
                        ConsigneeCode = row["CONSIGNEE_CODE"].ToString(),
                        ConsigneeName = row["CONSIGNEE_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }


        public bool SaveUpdate(ConsigneeInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.ConsigneeCode == null || master.ConsigneeCode.Length == 0)
                {
                    MaxID = idGenerated.getMAXID("CONSIGNEE_INFO", "CONSIGNEE_CODE", "fm000");
                    IUMode = "I";
                    Qry = "Insert into CONSIGNEE_INFO(CONSIGNEE_CODE, CONSIGNEE_NAME, ADDRESS, CONTACT_NO, CONTACT_PERSON, EMAIL_ID, FAX_NO, BUYER_CODE, STATUS ) " +
                          "Values('" + MaxID + "', '" + master.ConsigneeName + "', '" + master.Address + "', '" + master.ContactNo + "','" + master.ContactPerson + "','" + master.EmailID + "','" + master.FaxNo + "','" + master.BuyerCode + "','" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.ConsigneeCode;
                    IUMode = "U";
                    Qry = "Update CONSIGNEE_INFO set CONSIGNEE_NAME='" + master.ConsigneeName + "', ADDRESS='" + master.Address + "', CONTACT_NO='" + master.ContactNo + "', CONTACT_PERSON='" + master.ContactPerson + "', EMAIL_ID='" + master.EmailID + "'," +
                          "FAX_NO='" + master.FaxNo + "', BUYER_CODE='" + master.BuyerCode + "', STATUS='" + master.Status + "' Where CONSIGNEE_CODE ='" + MaxID + "'";
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