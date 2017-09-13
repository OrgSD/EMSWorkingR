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
    public class ProductPriceDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<ProductPriceMasterBEL> GetProductPriceList()
        {
            string Qry = "Select A.PRICE_MST_SLNO, A.PRICE_REVISION_NO, A.PRICE_REVISION_DATE, E.PRODUCT_CODE, E.PRODUCT_NAME, " +
                            "A.EFFECT_START_DATE, A.EFFECT_END_DATE, A.EFFECT_STATUS, B.BUYER_CODE, B.BUYER_NAME, " +
                            "C.COUNTRY_CODE, C.COUNTRY_NAME, A.REMARKS " +
                            "From Product_Price_MST a, Buyer_info B, Country_info c, Product_info E " +
                            "Where A.BUYER_CODE = B.BUYER_CODE  " +
                            "and A.COUNTRY_CODE = C.COUNTRY_CODE " +
                            "and A.PRODUCT_CODE = E.PRODUCT_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProductPriceMasterBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProductPriceMasterBEL
                    {
                        PriceMstSlno = row["PRICE_MST_SLNO"].ToString(),
                        PriceRevisionNo = row["PRICE_REVISION_NO"].ToString(),
                        PriceRevisionDate = Convert.ToDateTime(row["PRICE_REVISION_DATE"]).ToString("dd/MM/yyyy"),
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        EffectStartDate = Convert.ToDateTime(row["EFFECT_START_DATE"]).ToString("dd/MM/yyyy"),
                        EffectEndDate = Convert.ToDateTime(row["EFFECT_END_DATE"]).ToString("dd/MM/yyyy"),
                        EffectStatus = row["EFFECT_STATUS"].ToString(),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        Remarks = row["REMARKS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ProductPriceDetailBEL> GetPriceDetails(string masterID)
        {
            string Qry = "SELECT  PRICE_DTL_SLNO,PRICE_MST_SLNO,ORDER_TYPE_CODE,FN_ORDER_TYPE_NAME(ORDER_TYPE_CODE) ORDER_TYPE_NAME, PRICE_TYPE_CODE,FN_PRICE_TYPE_NAME(PRICE_TYPE_CODE) PRICE_TYPE_NAME,CURRENCY_CODE,FN_CURRENCY_NAME(CURRENCY_CODE)CURRENCY_NAME,PRODUCT_PRICE,LC_PRICE  "+
                         " FROM PRODUCT_PRICE_DTL WHERE PRICE_MST_SLNO = '" + masterID + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProductPriceDetailBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProductPriceDetailBEL
                    {
                        PriceMstSlNo = row["PRICE_MST_SLNO"].ToString(),
                        PriceDtlSlNo = row["PRICE_DTL_SLNO"].ToString(),                        
                        OrderTypeCode = row["ORDER_TYPE_CODE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                        CurrencyCode = row["CURRENCY_CODE"].ToString(),
                        CurrencyName = row["CURRENCY_NAME"].ToString(),
                        PriceTypeCode = row["PRICE_TYPE_CODE"].ToString(),
                        PriceTypeName = row["PRICE_TYPE_NAME"].ToString(),                 
                        ProductPrice = row["PRODUCT_PRICE"].ToString(),
                        LCPrice = row["LC_PRICE"].ToString(),
                
                    }).ToList();
            return item;
        }

        public bool SaveUpdate(ProductPriceMasterBEL master)
        {
            try
            {
                bool isTrue = false;

                string MaxSL = "";
                if (String.IsNullOrEmpty(master.PriceMstSlno))
                {

                    MaxSL = idGenerated.getMAXSL("PRODUCT_PRICE_MST", "PRICE_MST_SLNO").ToString();
                    MaxID= master.PriceRevisionNo = idGenerated.getMAXID("PRODUCT_PRICE_MST", "PRICE_REVISION_NO", "fm00000");
                    IUMode = "I";
                    string Qry = "Insert into PRODUCT_PRICE_MST(PRICE_MST_SLNO,PRODUCT_CODE, PRICE_REVISION_NO ,PRICE_REVISION_DATE, EFFECT_START_DATE, EFFECT_END_DATE, EFFECT_STATUS, COMPANY_CODE, BUYER_CODE, COUNTRY_CODE, REMARKS) " +
                            "Values('" + MaxSL + "', '" + master.ProductCode + "', '" + master.PriceRevisionNo + "',TO_DATE('" + master.PriceRevisionDate + "','dd/MM/yyyy'), TO_DATE('" + master.EffectStartDate + "','dd/MM/yyyy') ,TO_DATE('" + master.EffectEndDate + "','dd/MM/yyyy'),'" + master.EffectStatus + "','" + master.CompanyCode + "','" + master.BuyerCode + "','" + master.CountryCode + "','" + master.Remarks + "')";
                    
                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {
                        if (master.PricingDetailList != null)
                        {
                            foreach (ProductPriceDetailBEL det in master.PricingDetailList)
                            {
                               string dtlMaxID = idGenerated.getMAXID("PRODUCT_PRICE_DTL", "PRICE_DTL_SLNO", "fm00000");

                                string subQry = "INSERT INTO PRODUCT_PRICE_DTL(PRICE_DTL_SLNO, PRICE_MST_SLNO, ORDER_TYPE_CODE, PRICE_TYPE_CODE, CURRENCY_CODE, PRODUCT_PRICE, LC_PRICE) " +
                                    "VALUES ('" + dtlMaxID + "','" + MaxSL + "','" + det.OrderTypeCode + "','" + det.PriceTypeCode + "','" + det.CurrencyCode + "','" + det.ProductPrice + "','" + det.LCPrice + "')";
                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
                                }
                            }
                        }
                    }
                }
                else
                {//U for Insert
                    MaxSL = master.PriceMstSlno;
                    IUMode = "U";
                    string Qry = "Update PRODUCT_PRICE_MST set PRODUCT_CODE='" + master.ProductCode + "', PRICE_REVISION_DATE=(TO_DATE('" + master.PriceRevisionDate + "','dd/MM/yyyy')), EFFECT_START_DATE= (TO_DATE('" + master.EffectStartDate + "','dd/MM/yyyy')), EFFECT_END_DATE= (TO_DATE('" + master.EffectEndDate + "','dd/MM/yyyy')), EFFECT_STATUS='" + master.EffectStatus + "'," +
                          "COMPANY_CODE='" + master.CompanyCode + "', BUYER_CODE='" + master.BuyerCode + "', COUNTRY_CODE='" + master.CountryCode + "', REMARKS='" + master.Remarks + "' Where PRICE_MST_SLNO='" + MaxSL + "'";
                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {

                        if (master.PricingDetailList != null)
                        {
                            foreach (ProductPriceDetailBEL det in master.PricingDetailList)
                            {

                                if (!String.IsNullOrEmpty(det.PriceDtlSlNo))
                                {
                                    MaxSL = det.PriceDtlSlNo;
                                    string subQry = "UPDATE PRODUCT_PRICE_DTL SET ORDER_TYPE_CODE = '" + det.OrderTypeCode + "', PRICE_TYPE_CODE = '" + det.PriceTypeCode + "', CURRENCY_CODE = '" + det.CurrencyCode + "', PRODUCT_PRICE = '" + det.ProductPrice + "', LC_PRICE = '" + det.LCPrice + "' WHERE PRICE_DTL_SLNO = '" + MaxSL + "'";
                                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                    {
                                        isTrue = true;
                                    }
                                }
                                else
                                {
                                    string dtlMaxID = idGenerated.getMAXID("PRODUCT_PRICE_DTL", "PRICE_DTL_SLNO", "fm00000");
                                    string subQry = "INSERT INTO PRODUCT_PRICE_DTL(PRICE_DTL_SLNO, PRICE_MST_SLNO, ORDER_TYPE_CODE, PRICE_TYPE_CODE, CURRENCY_CODE, PRODUCT_PRICE, LC_PRICE) " +
                                        "VALUES ('" + dtlMaxID + "','" + master.PriceMstSlno + "','" + det.OrderTypeCode + "','" + det.PriceTypeCode + "','" + det.CurrencyCode + "','" + det.ProductPrice + "','" + det.LCPrice + "')";
                                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                    {
                                        isTrue = true;
                                    }
                                }
                            }
                        }
                    }
                }
                return isTrue;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }
    }
}