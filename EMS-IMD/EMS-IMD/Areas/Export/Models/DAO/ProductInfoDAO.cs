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
    public class ProductInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public List<ProductInfoBEL> GetProductList()
        {
            string Qry = "SELECT A.PRODUCT_CODE, A.PRODUCT_NAME, A.PACK_SIZE, A.STRENGTH, A.GENERIC_CODE, E.GENERIC_NAME, " +
                        "A.THERAPEUTIC_CLASS_CODE, F.THERAPEUTIC_CLASS_NAME, A.BRAND, A.QTY_PER_PACK, " +
                        "A.REGISTRATION_NO, A.COMPANY_CODE, B.COMPANY_NAME, A.COUNTRY_CODE, C.COUNTRY_NAME, A.BUYER_CODE, D.BUYER_NAME,  " +
                        "A.STATUS " +
                        "FROM PRODUCT_INFO A, COMPANY_INFO B, COUNTRY_INFO C, BUYER_INFO D,  " +
                        "GENERIC_INFO E, THERAPEUTIC_CLASS_INFO F " +
                        "WHERE A.COMPANY_CODE = B.COMPANY_CODE " +
                        "AND A.COUNTRY_CODE = C.COUNTRY_CODE " +
                        "AND A.BUYER_CODE = D.BUYER_CODE " +
                        "AND A.GENERIC_CODE = E.GENERIC_CODE " +
                        "AND A.THERAPEUTIC_CLASS_CODE = F.THERAPEUTIC_CLASS_CODE ";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProductInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProductInfoBEL
                    {
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        PackSize = row["PACK_SIZE"].ToString(),
                        Strength = row["STRENGTH"].ToString(),
                        GenericCode = row["GENERIC_CODE"].ToString(),
                        GenericName = row["GENERIC_NAME"].ToString(),
                        TherapeuticClassCode = row["THERAPEUTIC_CLASS_CODE"].ToString(),
                        TherapeuticClassName = row["THERAPEUTIC_CLASS_NAME"].ToString(),
                        Brand = row["BRAND"].ToString(),
                        QtyPerPack = row["QTY_PER_PACK"].ToString(),
                        RegistrationNo = row["REGISTRATION_NO"].ToString(),
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        CompanyName = row["COMPANY_NAME"].ToString(),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ProductInfoBEL> GetActiveProductList()
        {
            string Qry = "SELECT A.PRODUCT_CODE, A.PRODUCT_NAME, A.PACK_SIZE, A.STRENGTH, A.GENERIC_CODE, E.GENERIC_NAME, " +
                        "A.THERAPEUTIC_CLASS_CODE, F.THERAPEUTIC_CLASS_NAME, A.BRAND, A.QTY_PER_PACK, " +
                        "A.REGISTRATION_NO, A.COMPANY_CODE, B.COMPANY_NAME, A.COUNTRY_CODE, C.COUNTRY_NAME, A.BUYER_CODE, D.BUYER_NAME,  " +
                        "A.STATUS " +
                        "FROM PRODUCT_INFO A, COMPANY_INFO B, COUNTRY_INFO C, BUYER_INFO D,  " +
                        "GENERIC_INFO E, THERAPEUTIC_CLASS_INFO F " +
                        "WHERE A.COMPANY_CODE = B.COMPANY_CODE " +
                        "AND A.COUNTRY_CODE = C.COUNTRY_CODE " +
                        "AND A.BUYER_CODE = D.BUYER_CODE " +
                        "AND A.GENERIC_CODE = E.GENERIC_CODE " +
                        "AND A.THERAPEUTIC_CLASS_CODE = F.THERAPEUTIC_CLASS_CODE " +
                        "AND A.STATUS = 'Active'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProductInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProductInfoBEL
                    {
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        PackSize = row["PACK_SIZE"].ToString(),
                        Strength = row["STRENGTH"].ToString(),
                        GenericCode = row["GENERIC_CODE"].ToString(),
                        GenericName = row["GENERIC_NAME"].ToString(),
                        TherapeuticClassCode = row["THERAPEUTIC_CLASS_CODE"].ToString(),
                        TherapeuticClassName = row["THERAPEUTIC_CLASS_NAME"].ToString(),
                        Brand = row["BRAND"].ToString(),
                        QtyPerPack = row["QTY_PER_PACK"].ToString(),
                        RegistrationNo = row["REGISTRATION_NO"].ToString(),
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        CompanyName = row["COMPANY_NAME"].ToString(),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ProductInfoBEL> GetProductComboList()
        {
            string Qry = "SELECT PRODUCT_CODE, PRODUCT_NAME FROM PRODUCT_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProductInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProductInfoBEL
                    {
                        value = row["PRODUCT_CODE"].ToString(),
                        text = row["PRODUCT_NAME"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ProductInfoBEL> GetProductListForPurchaseOrder(string orderTypeCode, string priceTypeCode, string buyerCode)
        {
            //string Qry = "SELECT A.PRODUCT_CODE, A.PRODUCT_NAME, A.PACK_SIZE, A.QTY_PER_PACK, C.PRODUCT_PRICE, C.CURRENCY_CODE, E.CURRENCY_NAME, A.COMPANY_CODE, D.COMPANY_NAME " +
            //                "FROM PRODUCT_INFO A, PRODUCT_PRICE_MST B, PRODUCT_PRICE_DTL C, COMPANY_INFO D, CURRENCY_INFO E " +
            //                "WHERE A.PRODUCT_CODE = B.PRODUCT_CODE " +
            //                "AND A.COMPANY_CODE = D.COMPANY_CODE " +
            //                "AND B.PRICE_MST_SLNO = C.PRICE_MST_SLNO " +
            //                "AND C.CURRENCY_CODE = E.CURRENCY_CODE " +
            //                "AND C.ORDER_TYPE_CODE = '" + orderTypeCode + "' " +
            //                "AND C.PRICE_TYPE_CODE = '" + priceTypeCode + "' " +
            //                "AND B.BUYER_CODE = '" + buyerCode + "' ";

            string Qry = "SELECT A.PRODUCT_CODE, A.PRODUCT_NAME, A.PACK_SIZE, A.QTY_PER_PACK, C.PRODUCT_PRICE, C.CURRENCY_CODE, E.CURRENCY_NAME, A.COMPANY_CODE, D.COMPANY_NAME " +
                        "FROM PRODUCT_INFO A, PRODUCT_PRICE_MST B, PRODUCT_PRICE_DTL C, COMPANY_INFO D, CURRENCY_INFO E  " +
                        "WHERE A.PRODUCT_CODE = B.PRODUCT_CODE  " +
                        "AND A.COMPANY_CODE = D.COMPANY_CODE  " +
                        "AND B.PRICE_MST_SLNO = C.PRICE_MST_SLNO  " +
                        "AND C.CURRENCY_CODE = E.CURRENCY_CODE  " +
                        "AND TO_DATE(SYSDATE ,'DD/MM/RRRR') BETWEEN TO_DATE(EFFECT_START_DATE,'DD/MM/RRRR') AND  TO_DATE(EFFECT_END_DATE ,'DD/MM/RRRR') " +
                        "AND C.ORDER_TYPE_CODE = '" + orderTypeCode + "' AND C.PRICE_TYPE_CODE = '" + priceTypeCode + "' AND B.BUYER_CODE = '" + buyerCode + "' ";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProductInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProductInfoBEL
                    {
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        PackSize = row["PACK_SIZE"].ToString(),
                        QtyPerPack = row["QTY_PER_PACK"].ToString(),
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        PricePerPack = row["PRODUCT_PRICE"].ToString(),
                        CurrencyCode = row["CURRENCY_CODE"].ToString(),
                        CurrencyName = row["CURRENCY_NAME"].ToString(),
                    }).ToList();
            return item;
        }

         public bool SaveUpdate(ProductInfoBEL master, string userID)
        {
            try
            {
                string chking = "SELECT * FROM PRODUCT_INFO WHERE PRODUCT_CODE = '"+ master.ProductCode +"'";
                DataTable dtcheck = dbHelper.GetDataTable(dbConn.SAConnStrReader(), chking);

                string Qry = "";
                //if (master.ProductCode == null || master.ProductCode.Length == 0)
                if(dtcheck.Rows.Count == 0)
                {
                    //MaxID = idGenerated.getMAXID("PRODUCT_INFO", "PRODUCT_CODE", "fm000000");
                    IUMode = "I";
                    Qry = "Insert into PRODUCT_INFO(PRODUCT_CODE,PRODUCT_NAME, PACK_SIZE, STRENGTH, GENERIC_CODE, THERAPEUTIC_CLASS_CODE, BRAND, QTY_PER_PACK, REGISTRATION_NO, COMPANY_CODE, BUYER_CODE, COUNTRY_CODE, STATUS, ENTERED_BY, ENTERED_DATE, ENTERED_TERMINAL ) " +
                          "Values('" + master.ProductCode + "', '" + master.ProductName + "', '" + master.PackSize + "', '" + master.Strength + "','" + master.GenericCode + "','" + master.TherapeuticClassCode + "','" + master.Brand + "','" + master.QtyPerPack + "','" + master.RegistrationNo + "','" + master.CompanyCode + "','" + master.BuyerCode + "','" + master.CountryCode + "','" + master.Status + "', '" + userID + "',(TO_DATE('" + CurrentDate + "','dd/MM/yyyy')),'" + idGenerated.GetLanIPAddress() + "')";
                }
                else
                {//U for Insert
                    MaxID = master.ProductCode;
                    IUMode = "U";
                    Qry = "Update PRODUCT_INFO set PRODUCT_NAME='" + master.ProductName + "', PACK_SIZE='" + master.PackSize + "', STRENGTH='" + master.Strength + "', GENERIC_CODE='" + master.GenericCode + "', THERAPEUTIC_CLASS_CODE='" + master.TherapeuticClassCode + "'," +
                          "BRAND='" + master.Brand + "', QTY_PER_PACK='" + master.QtyPerPack + "', REGISTRATION_NO='" + master.RegistrationNo + "', COMPANY_CODE='" + master.CompanyCode + "', BUYER_CODE='" + master.BuyerCode + "', COUNTRY_CODE='" + master.CountryCode + "', STATUS='" + master.Status + "' Where PRODUCT_CODE='" + MaxID + "'";
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