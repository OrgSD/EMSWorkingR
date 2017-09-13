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
    public class RegulatoryDocumentDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<RegulatoryDocumentMstBEL> GetRegulatoryList()
        {
            string Qry = "SELECT  A.REG_DOC_MST_SLNO, A.SHIPMENT_MST_SLNO, A.SHIPMENT_DATE, A.COMM_INVOICE_NO, A.COMM_INVOICE_DATE, A.BUYER_CODE, B.BUYER_NAME " +
            "FROM REGULATORY_DOCUMENT_MST A, BUYER_INFO B " +
            "WHERE A.BUYER_CODE = B.BUYER_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<RegulatoryDocumentMstBEL> item;

            item = (from DataRow row in dt.Rows
                    select new RegulatoryDocumentMstBEL
                    {
                        RegDocMstSlNo = row["REG_DOC_MST_SLNO"].ToString(),
                        ShipmentMstSlNo = row["SHIPMENT_MST_SLNO"].ToString(),
                        ShipmentDate = Convert.ToDateTime(row["SHIPMENT_DATE"]).ToString("dd/MM/yyyy"),
                        CommInvoiceNo = row["COMM_INVOICE_NO"].ToString(),
                        CommInvoiceDate = Convert.ToDateTime(row["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                    }).ToList();
            return item;
        }


        public bool SaveUpdate(RegulatoryDocumentMstBEL master)
        {
            try
            {
                bool isTrue = false;

                //string MaxSL = "";
                if (String.IsNullOrEmpty(master.RegDocMstSlNo))
                {

                    MaxID = idGenerated.getMAXSL("REGULATORY_DOCUMENT_MST", "REG_DOC_MST_SLNO").ToString();
                    //MaxID = master.PriceRevisionNo = idGenerated.getMAXID("PRODUCT_PRICE_MST", "PRICE_REVISION_NO", "fm00000");
                    IUMode = "I";
                    string Qry = "Insert into REGULATORY_DOCUMENT_MST(REG_DOC_MST_SLNO,SHIPMENT_MST_SLNO, SHIPMENT_DATE ,COMM_INVOICE_NO, COMM_INVOICE_DATE, BUYER_CODE ) " +
                            "Values(" + MaxID + ", " + master.ShipmentMstSlNo + ", TO_DATE('" + master.ShipmentDate + "','dd/MM/yyyy'),  '" + master.CommInvoiceNo + "' ,TO_DATE('" + master.CommInvoiceDate + "','dd/MM/yyyy'),'" + master.BuyerCode + "')";

                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {
                        if (master.RegDocList != null)
                        {
                            foreach (RegulatoryDocumentDtlBEL det in master.RegDocList)
                            {
                                string dtlMaxID = idGenerated.getMAXSL("REGULATORY_DOCUMENT_DTL", "REG_DOC_DTL_SLNO").ToString();

                                string subQry = "INSERT INTO REGULATORY_DOCUMENT_DTL(REG_DOC_DTL_SLNO, REG_DOC_MST_SLNO, DOCUMENT_NAME, REMARKS, DOC_REF) " +
                                    "VALUES (" + dtlMaxID + "," + MaxID + ",'" + det.DocumentName + "','" + det.Remarks + "','" + det.DocRef + "')";
                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
                                }
                            }
                        }
                    }
                }
                //else
                //{//U for Insert
                //    MaxSL = master.PriceMstSlno;
                //    IUMode = "U";
                //    string Qry = "Update PRODUCT_PRICE_MST set PRODUCT_CODE='" + master.ProductCode + "', PRICE_REVISION_DATE=(TO_DATE('" + master.PriceRevisionDate + "','dd/MM/yyyy')), EFFECT_START_DATE= (TO_DATE('" + master.EffectStartDate + "','dd/MM/yyyy')), EFFECT_END_DATE= (TO_DATE('" + master.EffectEndDate + "','dd/MM/yyyy')), EFFECT_STATUS='" + master.EffectStatus + "'," +
                //          "COMPANY_CODE='" + master.CompanyCode + "', BUYER_CODE='" + master.BuyerCode + "', COUNTRY_CODE='" + master.CountryCode + "', REMARKS='" + master.Remarks + "' Where PRICE_MST_SLNO='" + MaxSL + "'";
                //    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                //    {

                //        if (master.PricingDetailList != null)
                //        {
                //            foreach (ProductPriceDetailBEL det in master.PricingDetailList)
                //            {

                //                if (!String.IsNullOrEmpty(det.PriceDtlSlNo))
                //                {
                //                    MaxSL = det.PriceDtlSlNo;
                //                    string subQry = "UPDATE PRODUCT_PRICE_DTL SET ORDER_TYPE_CODE = '" + det.OrderTypeCode + "', PRICE_TYPE_CODE = '" + det.PriceTypeCode + "', CURRENCY_CODE = '" + det.CurrencyCode + "', PRODUCT_PRICE = '" + det.ProductPrice + "', LC_PRICE = '" + det.LCPrice + "' WHERE PRICE_DTL_SLNO = '" + MaxSL + "'";
                //                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                //                    {
                //                        isTrue = true;
                //                    }
                //                }
                //                else
                //                {
                //                    string dtlMaxID = idGenerated.getMAXID("PRODUCT_PRICE_DTL", "PRICE_DTL_SLNO", "fm00000");
                //                    string subQry = "INSERT INTO PRODUCT_PRICE_DTL(PRICE_DTL_SLNO, PRICE_MST_SLNO, ORDER_TYPE_CODE, PRICE_TYPE_CODE, CURRENCY_CODE, PRODUCT_PRICE, LC_PRICE) " +
                //                        "VALUES ('" + dtlMaxID + "','" + master.PriceMstSlno + "','" + det.OrderTypeCode + "','" + det.PriceTypeCode + "','" + det.CurrencyCode + "','" + det.ProductPrice + "','" + det.LCPrice + "')";
                //                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                //                    {
                //                        isTrue = true;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                return isTrue;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }

    }
}