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
    public class BuyerInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<BuyerInfoBEL> GetBuyerList()
        {
            string Qry = "SELECT A.BUYER_CODE, A.BUYER_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO, NVL(A.NOTIFICATION_DAY, 0) NOTIFICATION_DAY, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.COUNTRY_CODE, B.COUNTRY_NAME, A.STATUS " + 
                         "from BUYER_INFO A, COUNTRY_INFO B " +
                         "WHERE A.COUNTRY_CODE = B.COUNTRY_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BuyerInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BuyerInfoBEL
                    {
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        NotificationDay = Convert.ToInt32(row["NOTIFICATION_DAY"].ToString()),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<BuyerInfoBEL> GetActiveBuyerList()
        {
            string Qry = "SELECT A.BUYER_CODE, A.BUYER_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO, NVL(A.NOTIFICATION_DAY, 0) NOTIFICATION_DAY, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.COUNTRY_CODE, B.COUNTRY_NAME, A.STATUS " +
                         "from BUYER_INFO A, COUNTRY_INFO B " +
                         "WHERE A.COUNTRY_CODE = B.COUNTRY_CODE" + 
                         "AND A.STATUS = 'Active'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BuyerInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BuyerInfoBEL
                    {
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        NotificationDay = Convert.ToInt32(row["NOTIFICATION_DAY"].ToString()),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<BuyerInfoBEL> GetBuyerListByUserID(string buyerList)
        {
            string Qry = "SELECT A.BUYER_CODE, A.BUYER_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO, NVL(A.NOTIFICATION_DAY, 0) NOTIFICATION_DAY, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.COUNTRY_CODE, B.COUNTRY_NAME, A.STATUS " +
                         "from BUYER_INFO A, COUNTRY_INFO B " +
                         "WHERE A.COUNTRY_CODE = B.COUNTRY_CODE " +
                         "AND A.BUYER_CODE in ("+ buyerList + ") " +
                         "AND A.STATUS = 'Active'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BuyerInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BuyerInfoBEL
                    {
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        NotificationDay = Convert.ToInt32(row["NOTIFICATION_DAY"].ToString()),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public BuyerInfoBEL GetIndividualBuyer(string buyerCode)
        {
            string Qry = "SELECT A.BUYER_CODE, A.BUYER_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO, NVL(A.NOTIFICATION_DAY, 0) NOTIFICATION_DAY, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.COUNTRY_CODE, B.COUNTRY_NAME, A.STATUS " +
                         "from BUYER_INFO A, COUNTRY_INFO B " +
                         "WHERE A.COUNTRY_CODE = B.COUNTRY_CODE " +
                         "AND A.BUYER_CODE = '"+ buyerCode +"' " ;
                        
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            BuyerInfoBEL item = new BuyerInfoBEL();

            item = (from DataRow row in dt.Rows
                    select new BuyerInfoBEL
                    {
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        NotificationDay = Convert.ToInt32(row["NOTIFICATION_DAY"].ToString()),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).FirstOrDefault();
            return item;
        }
		
		public bool SaveUpdate(BuyerInfoBEL master)
        {
            try
            {
                string Qry = "";

                if (master.BuyerCode == null || master.BuyerCode.Length == 0)
                {
                    //MaxID = idGenerated.getMAXID("BUYER_INFO", "BUYER_CODE", "fm000");
                    IUMode = "I";
                    Qry = "Insert into BUYER_INFO(BUYER_CODE,BUYER_NAME, ADDRESS, CONTACT_NO, CONTACT_PERSON, EMAIL_ID, FAX_NO, NOTIFICATION_DAY, TERMS_CONDITION, TERMS_OF_PAYMENT, COUNTRY_CODE, STATUS ) " +
                            "Values('" + master.BuyerCode + "', '" + master.BuyerName + "', '" + master.Address + "', '" + master.ContactNo + "','" + master.ContactPerson + "','" + master.EmailID + "','" + master.FaxNo + "','" + master.NotificationDay + "','" + master.TermsCondition + "','" + master.TermsOfPayment + "','" + master.CountryCode + "','" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.BuyerCode;
                    IUMode = "U";
                    Qry = "Update BUYER_INFO set BUYER_NAME='" + master.BuyerName + "', ADDRESS='" + master.Address + "', CONTACT_NO='" + master.ContactNo + "', CONTACT_PERSON='" + master.ContactPerson + "', EMAIL_ID='" + master.EmailID + "'," +
                            "FAX_NO='" + master.FaxNo + "', NOTIFICATION_DAY='" + master.NotificationDay + "', TERMS_CONDITION='" + master.TermsCondition + "', TERMS_OF_PAYMENT='" + master.TermsOfPayment + "', COUNTRY_CODE='" + master.CountryCode + "', STATUS='" + master.Status + "' Where BUYER_CODE='" + MaxID + "'";
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


        public List<BuyerInfoBEL> GetBuyerListForCombo()
        {
            string Qry = "SELECT A.BUYER_CODE, A.BUYER_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO, NVL(A.NOTIFICATION_DAY, 0) NOTIFICATION_DAY, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.COUNTRY_CODE, B.COUNTRY_NAME, A.STATUS " +
                         "from BUYER_INFO A, COUNTRY_INFO B " +
                         "WHERE A.COUNTRY_CODE = B.COUNTRY_CODE " +
                       //  "AND A.BUYER_CODE in (" + buyerList + ") " +
                         "AND A.STATUS = 'Active'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BuyerInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BuyerInfoBEL
                    {
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        NotificationDay = Convert.ToInt32(row["NOTIFICATION_DAY"].ToString()),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

    }
}