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
    public class SalesCollectionDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();


        public bool SaveUpdate(SalesCollectionBEL master)
        {
            try
            {
                string Qry = "";
                if (String.IsNullOrEmpty(master.SalesCollSlNo))
                {
                    MaxID = idGenerated.getMAXSL("SALES_COLLECTION", "SALES_COLL_SLNO").ToString();
                    IUMode = "I";
                    Qry = "Insert into SALES_COLLECTION(SALES_COLL_SLNO, SALES_COLL_DATE, COMM_INVOICE_NO, COMM_INVOICE_DATE, BUYER_CODE, COLL_AMOUNT, BANK_CHARGE, FDBC_NO, FDBC_DATE, BANK_CODE) " +
                          "Values(" + MaxID + ", '" + master.SalesCollDate + "', '" + master.CommInvoiceNo + "', TO_DATE('" + master.CommInvoiceDate + "','dd/MM/yyyy'),'" + master.BuyerCode + "','" + master.CollAmount + "','" + master.BankCharge + "','" + master.FdbcNo + "', TO_DATE('" + master.FdbcDate + "','dd/MM/yyyy'),'" + master.BankCode + "')";
                }
                else
                {//U for Insert
                    //MaxID = master.SalesCollSlNo;
                    //IUMode = "U";
                    //Qry = "Update SALES_COLLECTION set NOTIFY_PARTY_NAME='" + master.NotifyPartyName + "', ADDRESS='" + master.Address + "', CONTACT_NO='" + master.ContactNo + "', CONTACT_PERSON='" + master.ContactPerson + "', EMAIL_ID='" + master.EmailID + "'," +
                    //      "FAX_NO='" + master.FaxNo + "', BUYER_CODE='" + master.BuyerCode + "', STATUS='" + master.Status + "' Where NOTIFY_PARTY_CODE ='" + MaxID + "'";
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

        public List<CommercialInvoiceFinalizeBEL> GetCommercialInvoiceForCollection()
        {
            string Qry = "Select   COMMM_INV_FINAL_MST_SLNO, COMM_INVOICE_NO, COMM_INVOICE_DATE, NET_INVOICE_AMOUNT FROM COMM_INVOICE_FINAL_MST ";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<CommercialInvoiceFinalizeBEL> item;

            item = (from DataRow row in dt.Rows
                    select new CommercialInvoiceFinalizeBEL
                    {
                        CommInvFinalMstSl = Convert.ToInt64(row["COMMM_INV_FINAL_MST_SLNO"].ToString()),
                        CommInvoiceNo = row["COMM_INVOICE_NO"].ToString(),
                        CommInvoiceDate = Convert.ToDateTime(row["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        NetInvAmount = row["NET_INVOICE_AMOUNT"].ToString(),
                    }).ToList();
            return item;
        }

        //public List<SalesCollectionBEL> GetSalesCollectionList()
        //{
        //    string Qry = "Select COMMM_INV_FINAL_MST_SLNO, COMM_INVOICE_NO, COMM_INVOICE_DATE, NET_INVOICE_AMOUNT FROM COMM_INVOICE_FINAL_MST ";
        //    DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
        //    List<SalesCollectionBEL> item;

        //    item = (from DataRow row in dt.Rows
        //            select new SalesCollectionBEL
        //            {
        //                CommInvFinalMstSl = Convert.ToInt64(row["COMMM_INV_FINAL_MST_SLNO"].ToString()),
        //                CommInvoiceNo = row["COMM_INVOICE_NO"].ToString(),
        //                CommInvoiceDate = Convert.ToDateTime(row["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
        //                NetInvAmount = row["NET_INVOICE_AMOUNT"].ToString(),
        //            }).ToList();
        //    return item;
        //}


        public List<SalesCollectionBEL> GetPreviousAmount(string CommInvMstSlNo)
        {
            string Qry = "Select COMM_INVOICE_NO, NVL(TOT_COLL_AMOUNT, 0) TOT_COLL_AMOUNT,  NVL(TOT_BANK_CHARGE, 0) TOT_BANK_CHARGE " +
                        "From SALES_COLLECTION_VUE " +
                        "Where COMM_INVOICE_NO = " + CommInvMstSlNo + "";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<SalesCollectionBEL> item;

            item = (from DataRow row in dt.Rows
                    select new SalesCollectionBEL
                    {
                       PrevRecv = row["TOT_COLL_AMOUNT"].ToString(),
                       PrevBank = row["TOT_BANK_CHARGE"].ToString(),
                    }).ToList();
            return item;
        }

    }
}