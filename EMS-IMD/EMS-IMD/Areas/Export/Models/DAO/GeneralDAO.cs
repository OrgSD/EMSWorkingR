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
    public class GeneralDAO
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<PriceTypeInfoBEL> GetPriceTypeList()
        {
            string Qry = "SELECT PRICE_TYPE_CODE, PRICE_TYPE_NAME, STATUS FROM PRICE_TYPE_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PriceTypeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PriceTypeInfoBEL
                    {
                        PriceTypeCode = row["PRICE_TYPE_CODE"].ToString(),
                        PriceTypeName = row["PRICE_TYPE_NAME"].ToString(),
                        value = row["PRICE_TYPE_CODE"].ToString(),
                        text = row["PRICE_TYPE_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<OrderTypeInfoBEL> GetOrderTypeList()
        {
            string Qry = "SELECT ORDER_TYPE_CODE, ORDER_TYPE_NAME, STATUS FROM ORDER_TYPE_INFO ORDER BY ORDER_TYPE_NAME";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<OrderTypeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new OrderTypeInfoBEL
                    {
                        OrderTypeCode = row["ORDER_TYPE_CODE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                        value = row["ORDER_TYPE_CODE"].ToString(),
                        text = row["ORDER_TYPE_NAME"].ToString(),
                        // Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<BranchInfoBEL> GetBranchByBank(string bankCode)
        {
            string Qry = "SELECT BRANCH_CODE, BRANCH_NAME FROM BRANCH_INFO " +
                          "WHERE BANK_CODE = '" + bankCode + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BranchInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BranchInfoBEL
                    {
                        BranchCode = row["BRANCH_CODE"].ToString(),
                        BranchName = row["BRANCH_NAME"].ToString(),
                    }).ToList();
            return item;
        }


        public List<FormWiseDocTypeBEL> GetDocumentType(string docType)
        {
            string Qry = "SELECT DOCUMENT_CODE, DOCUMENT_NAME FROM FORM_WISE_DOC_DETAILS " +
                          "WHERE FORM_TYPE = '" + docType + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<FormWiseDocTypeBEL> item;

            item = (from DataRow row in dt.Rows
                    select new FormWiseDocTypeBEL
                    {
                        DocCode = row["DOCUMENT_CODE"].ToString(),
                        DocName = row["DOCUMENT_NAME"].ToString(),
                    }).ToList();
            return item;
        }

    }
}