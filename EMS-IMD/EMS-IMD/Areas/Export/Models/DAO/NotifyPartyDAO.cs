using EMS_IMD.DAL.Gateway;
using EMS_IMD.Universal.Gateway;
using EMS_IMD.Areas.Export.Models.BEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.DAO
{
    public class NotifyPartyDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<NotifyPartyBEL> GetNotifyPartyList()
        {
            string Qry = "SELECT A.BUYER_CODE, B.BUYER_NAME, A.NOTIFY_PARTY_CODE, A.NOTIFY_PARTY_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO , A.STATUS " +
                         "from  NOTIFY_PARTY_INFO A, BUYER_INFO B " +
                         "WHERE A.BUYER_CODE = B.BUYER_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<NotifyPartyBEL> item;

            item = (from DataRow row in dt.Rows
                    select new NotifyPartyBEL
                    {
                        NotifyPartyCode = row["NOTIFY_PARTY_CODE"].ToString(),
                        NotifyPartyName = row["NOTIFY_PARTY_NAME"].ToString(),
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

        public List<NotifyPartyBEL> GetActiveNotifyPartyList()
        {
            string Qry = "SELECT A.BUYER_CODE, B.BUYER_NAME, A.NOTIFY_PARTY_CODE, A.NOTIFY_PARTY_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO , A.STATUS " +
                         "from  NOTIFY_PARTY_INFO A, BUYER_INFO B " +
                         "WHERE A.BUYER_CODE = B.BUYER_CODE " +
                         "AND A.STATUS = 'Active'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<NotifyPartyBEL> item;

            item = (from DataRow row in dt.Rows
                    select new NotifyPartyBEL
                    {
                        NotifyPartyCode = row["NOTIFY_PARTY_CODE"].ToString(),
                        NotifyPartyName = row["NOTIFY_PARTY_NAME"].ToString(),
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

        public bool SaveUpdate(NotifyPartyBEL master)
        {
            try
            {
                string Qry = "";

                if (master.NotifyPartyCode == null || master.NotifyPartyCode.Length == 0)
                {
                    MaxID = idGenerated.getMAXID("NOTIFY_PARTY_INFO", "NOTIFY_PARTY_CODE", "fm000");
                    IUMode = "I";
                    Qry = "Insert into NOTIFY_PARTY_INFO(NOTIFY_PARTY_CODE, NOTIFY_PARTY_NAME, ADDRESS, CONTACT_NO, CONTACT_PERSON, EMAIL_ID, FAX_NO, BUYER_CODE, STATUS ) " +
                            "Values('" + MaxID + "', '" + master.NotifyPartyName + "', '" + master.Address + "', '" + master.ContactNo + "','" + master.ContactPerson + "','" + master.EmailID + "','" + master.FaxNo + "','" + master.BuyerCode + "','" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.NotifyPartyCode;
                    IUMode = "U";
                    Qry = "Update NOTIFY_PARTY_INFO set NOTIFY_PARTY_NAME='" + master.NotifyPartyName + "', ADDRESS='" + master.Address + "', CONTACT_NO='" + master.ContactNo + "', CONTACT_PERSON='" + master.ContactPerson + "', EMAIL_ID='" + master.EmailID + "'," +
                            "FAX_NO='" + master.FaxNo + "', BUYER_CODE='" + master.BuyerCode + "', STATUS='" + master.Status + "' Where NOTIFY_PARTY_CODE ='" + MaxID + "'";
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


        public List<NotifyPartyBEL> GetNotifyPartyByBuyer(string BuyerCode)
        {
            string Qry = "SELECT A.NOTIFY_PARTY_CODE, A.NOTIFY_PARTY_NAME, A.ADDRESS,A.CONTACT_NO, A.CONTACT_PERSON, A.EMAIL_ID, A.FAX_NO , A.STATUS " +
                         "from  NOTIFY_PARTY_INFO A " +
                         "WHERE A.BUYER_CODE = '" + BuyerCode + "' " +
                         "AND A.STATUS = 'Active'";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<NotifyPartyBEL> item;

            item = (from DataRow row in dt.Rows
                    select new NotifyPartyBEL
                    {
                        NotifyPartyCode = row["NOTIFY_PARTY_CODE"].ToString(),
                        NotifyPartyName = row["NOTIFY_PARTY_NAME"].ToString(),
                        Address = row["ADDRESS"].ToString(),
                        ContactNo = row["CONTACT_NO"].ToString(),
                        ContactPerson = row["CONTACT_PERSON"].ToString(),
                        EmailID = row["EMAIL_ID"].ToString(),
                        FaxNo = row["FAX_NO"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }



    }
}