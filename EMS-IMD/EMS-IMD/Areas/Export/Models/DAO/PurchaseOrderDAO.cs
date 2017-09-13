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
    public class PurchaseOrderDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public List<PurchaseOrderMstBEL> GetPurchaseOrderList(string buyerID)
        {
            string Qry = "SELECT A.PO_MST_SLNO, A.PO_NO, A.PO_DATE, A.BUYER_CODE, B.COUNTRY_CODE, B.BUYER_NAME,  B.EMAIL_ID, A.DELIVERY_ADDRESS, A.DELIVERY_DATE,  " +
                         "A.INCOTERM_CODE, A.DESTINATION_PORT, A.LOADING_PORT, A.NOTIFY_DAYS, A.ORDER_TYPE, TOTAL_PO_AMOUNT_IN_BDT, " +
                         "A.PRICE_TYPE, A.REMARKS, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.TOTAL_PO_AMOUNT, A.TRANSPORT_MODE, A.PO_STATUS " +
                         "FROM PURCHASE_ORDER_MST A, BUYER_INFO B " +
                         "WHERE A.BUYER_CODE = B.BUYER_CODE " +
                         "AND A.BUYER_CODE = '" + buyerID + "'" + 
                         "ORDER BY A.PO_NO DESC";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PurchaseOrderMstBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PurchaseOrderMstBEL
                    {
                        PoMstSlno = row["PO_MST_SLNO"].ToString(),
                        PoNo = row["PO_NO"].ToString(),
                        PoDate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),    
                        Email = row["EMAIL_ID"].ToString(),
                        DeliveryAddress = row["DELIVERY_ADDRESS"].ToString(),
                        DeliveryDate = Convert.ToDateTime(row["DELIVERY_DATE"]).ToString("dd/MM/yyyy"),
                        IncoTermCode = row["INCOTERM_CODE"].ToString(),
                        OrderTypeCode = row["ORDER_TYPE"].ToString(),
                        PriceTypeCode = row["PRICE_TYPE"].ToString(),
                        DestinationPortCode = row["DESTINATION_PORT"].ToString(),
                        LoadingPortCode = row["LOADING_PORT"].ToString(),                      
                        TransportModeCode = row["TRANSPORT_MODE"].ToString(),
                        NotifyDays = row["NOTIFY_DAYS"].ToString(),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        TotalPoAmount = row["TOTAL_PO_AMOUNT"].ToString(),
                        TotalPoAmountBDT = row["TOTAL_PO_AMOUNT_IN_BDT"].ToString(),
                        Remarks = row["REMARKS"].ToString(),
                        PoStatus = row["PO_STATUS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<PurchaseOrderMstBEL> GetIndividualPOByCustomerBuyer(string companyCode, string buyerCode)
        {
            string Qry = "SELECT DISTINCT(A.PO_NO), A.PO_MST_SLNO, A.PO_DATE, A.ORDER_TYPE, O.ORDER_TYPE_NAME, A.DESTINATION_PORT, D.DESTINATION_PORT_NAME, A.LOADING_PORT,  " +
                            "L.DESTINATION_PORT_NAME AS LOADING_PORT_NAME, A.INCOTERM_CODE, I.INCOTERM_NAME, A.PRICE_TYPE, P.PRICE_TYPE_NAME, A.TRANSPORT_MODE,  " +
                            "T.TRANSPORT_MODE_NAME, A.NOTIFY_DAYS, A.TERMS_OF_PAYMENT " +
                            "FROM VW_PO_LIST_FOR_PROFORMA A, INCOTERM_INFO I, ORDER_TYPE_INFO O, " +
                            "PRICE_TYPE_INFO P, TRANSPORT_MODE_INFO T, DESTINATION_PORT_INFO D, DESTINATION_PORT_INFO L " +
                            //"WHERE A.PO_MST_SLNO = B.PO_MST_SLNO " +
                            "WHERE A.INCOTERM_CODE = I.INCOTERM_CODE " +
                            "AND A.ORDER_TYPE = O.ORDER_TYPE_CODE " +
                            "AND A.PRICE_TYPE = P.PRICE_TYPE_CODE " +
                            "AND A.TRANSPORT_MODE = T.TRANSPORT_MODE_CODE " +
                            "AND A.DESTINATION_PORT = D.DESTINATION_PORT_CODE " +
                            "AND A.LOADING_PORT = L.DESTINATION_PORT_CODE " +
                            "AND A.BUYER_CODE = '" + buyerCode + "' " +
                            "AND A.COMPANY_CODE = '" + companyCode + "'"; 

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PurchaseOrderMstBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PurchaseOrderMstBEL
                    {
                        PoMstSlno = row["PO_MST_SLNO"].ToString(),
                        PoNo = row["PO_NO"].ToString(),
                        PoDate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        IncoTermCode = row["INCOTERM_CODE"].ToString(),
                        IncoTermName = row["INCOTERM_NAME"].ToString(),
                        OrderTypeCode = row["ORDER_TYPE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                        PriceTypeCode = row["PRICE_TYPE"].ToString(),
                        PriceTypeName = row["PRICE_TYPE_NAME"].ToString(),
                        DestinationPortCode = row["DESTINATION_PORT"].ToString(),
                        DestinationPortName = row["DESTINATION_PORT_NAME"].ToString(),
                        LoadingPortCode = row["LOADING_PORT"].ToString(),
                        LoadingPortName = row["LOADING_PORT_NAME"].ToString(),
                        TransportModeCode = row["TRANSPORT_MODE"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        NotifyDays = row["NOTIFY_DAYS"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                    }).ToList();
            return item;
        }

        public List<PurchaseOrderDtlBEL> GetProductList(string poNo, string companyCode)
        {
            string Qry = "SELECT A.PO_DTL_SLNO, A.PO_MST_SLNO, B.PRODUCT_NAME, B.PACK_SIZE, B.QTY_PER_PACK, A.PRODUCT_CODE, A.COMPANY_CODE, A.CURRENCY_CODE, C.CURRENCY_NAME, A.PO_QTY_IN_PACK, " +
                        "A.PRICE_PER_PACK, A.CONVERSION_RATE, A.TOTAL_AMOUNT_IN_BDT, A.PO_QTY_IN_PCS, A.TOTAL_AMOUNT " +
                        "FROM PURCHASE_ORDER_DTL A, PRODUCT_INFO B, CURRENCY_INFO C, PURCHASE_ORDER_MST D " +
                        "WHERE A.PRODUCT_CODE = B.PRODUCT_CODE " +
                        "AND A.CURRENCY_CODE = C.CURRENCY_CODE " +
                        "AND A.PO_MST_SLNO = D.PO_MST_SLNO " +
                        "AND D.PO_NO = '" + poNo + "'" + 
                        "AND A.COMPANY_CODE = '"+ companyCode +"' ";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PurchaseOrderDtlBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PurchaseOrderDtlBEL
                    {
                        PoMstSlno = row["PO_MST_SLNO"].ToString(),
                        PoDtlSlno = row["PO_DTL_SLNO"].ToString(),
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        CurrencyCode = row["CURRENCY_CODE"].ToString(),
                        CurrencyName = row["CURRENCY_NAME"].ToString(),                       
                        PackSize = row["PACK_SIZE"].ToString(),
                        QtyPerPack = row["QTY_PER_PACK"].ToString(),
                        NoOfPack = row["PO_QTY_IN_PACK"].ToString(),
                        NoOfPcs = row["PO_QTY_IN_PCS"].ToString(),
                        PricePerPack = row["PRICE_PER_PACK"].ToString(),
                        TotalAmount = row["TOTAL_AMOUNT"].ToString(),
                        TotalAmountBDT = row["TOTAL_AMOUNT_IN_BDT"].ToString(),
                        ConversionRate = row["CONVERSION_RATE"].ToString(),
                    }).ToList();
            return item;

        }

        public List<PurchaseOrderDtlBEL> GetPurchaseOrderDetails(string masterID)
        {
            string Qry = "SELECT A.PO_DTL_SLNO, A.PO_MST_SLNO, B.PRODUCT_NAME, B.PACK_SIZE, B.QTY_PER_PACK, A.PRODUCT_CODE, A.COMPANY_CODE, A.CURRENCY_CODE, C.CURRENCY_NAME, A.PO_QTY_IN_PACK, " +
                        "A.PRICE_PER_PACK, A.CONVERSION_RATE, A.TOTAL_AMOUNT_IN_BDT, A.PO_QTY_IN_PCS, A.TOTAL_AMOUNT " +
                        "FROM PURCHASE_ORDER_DTL A, PRODUCT_INFO B, CURRENCY_INFO C " +
                        "WHERE A.PRODUCT_CODE = B.PRODUCT_CODE " +
                        "AND A.CURRENCY_CODE = C.CURRENCY_CODE " +
                        "AND A.PO_MST_SLNO = '" + masterID + "'";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PurchaseOrderDtlBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PurchaseOrderDtlBEL
                    {
                        PoMstSlno = row["PO_MST_SLNO"].ToString(),
                        PoDtlSlno = row["PO_DTL_SLNO"].ToString(),
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        CurrencyCode = row["CURRENCY_CODE"].ToString(),
                        CurrencyName = row["CURRENCY_NAME"].ToString(),
                        PackSize = row["PACK_SIZE"].ToString(),
                        QtyPerPack = row["QTY_PER_PACK"].ToString(),
                        NoOfPack = row["PO_QTY_IN_PACK"].ToString(),
                        NoOfPcs = row["PO_QTY_IN_PCS"].ToString(),
                        PricePerPack = row["PRICE_PER_PACK"].ToString(),
                        TotalAmount = row["TOTAL_AMOUNT"].ToString(),
                        TotalAmountBDT = row["TOTAL_AMOUNT_IN_BDT"].ToString(),
                        ConversionRate = row["CONVERSION_RATE"].ToString(),
                    }).ToList();
            return item;

        }

        public bool SaveUpdate(PurchaseOrderMstBEL master, string userID)
        {
            try
            {
                bool isTrue = false;
                string mstSL = "";
                if (String.IsNullOrEmpty(master.PoNo))
                {
                    mstSL = idGenerated.getMAXSL("PURCHASE_ORDER_MST", "PO_MST_SLNO").ToString();
                    master.PoNo = idGenerated.getMAXID("PURCHASE_ORDER_MST", "PO_NO", "fm00000");
                    MaxID = master.PoNo;
                    MaxCode = mstSL;
                    IUMode = "I";
                    string Qry = "Insert into PURCHASE_ORDER_MST(PO_MST_SLNO, PO_NO, PO_DATE, BUYER_CODE, COUNTRY_CODE, DELIVERY_DATE, DELIVERY_ADDRESS, INCOTERM_CODE, ORDER_TYPE, PRICE_TYPE, DESTINATION_PORT, LOADING_PORT, TRANSPORT_MODE, NOTIFY_DAYS, TERMS_CONDITION, TERMS_OF_PAYMENT, TOTAL_PO_AMOUNT, TOTAL_PO_AMOUNT_IN_BDT, REMARKS, ENTERED_BY, ENTERED_DATE, ENTERED_TERMINAL, PO_STATUS ) " +
                            "Values('" + mstSL + "', '" + master.PoNo + "', (TO_DATE('" + master.PoDate + "','dd/MM/yyyy')), '" + master.BuyerCode + "','" + master.CountryCode + "', (TO_DATE('" + master.DeliveryDate + "','dd/MM/yyyy')) , '" + master.DeliveryAddress + "','" + master.IncoTermCode + "','" + master.OrderTypeCode + "','" + master.PriceTypeCode + "','" + master.DestinationPortCode + "','" + master.LoadingPortCode + "','" + master.TransportModeCode + "','" + master.NotifyDays + "','" + master.TermsCondition + "','" + master.TermsOfPayment + "','" + master.TotalPoAmount + "','" + master.TotalPoAmountBDT + "','" + master.Remarks + "', '" + userID + "',(TO_DATE('" + CurrentDate + "','dd/MM/yyyy')),'" + idGenerated.GetLanIPAddress() + "', '"+ master.PoStatus +"')";

                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {
                        if (master.PODetailList != null)
                        {
                            foreach (PurchaseOrderDtlBEL det in master.PODetailList)
                            {
                                string dtlMaxID = idGenerated.getMAXSL("PURCHASE_ORDER_DTL", "PO_DTL_SLNO").ToString();
                                string subQry = "INSERT INTO PURCHASE_ORDER_DTL(PO_DTL_SLNO, PO_MST_SLNO, PRODUCT_CODE, PO_QTY_IN_PACK, PO_QTY_IN_PCS, PRICE_PER_PACK, TOTAL_AMOUNT, CURRENCY_CODE, CONVERSION_RATE, TOTAL_AMOUNT_IN_BDT, COMPANY_CODE) " +
                                        "VALUES (" + dtlMaxID + "," + mstSL + ",'" + det.ProductCode + "','" + det.NoOfPack + "','" + det.NoOfPcs + "','" + det.PricePerPack + "','" + det.TotalAmount + "','" + det.CurrencyCode + "','" + det.ConversionRate + "','" + det.TotalAmountBDT + "', '" + det.CompanyCode + "')";
                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string qryCheck = "Select * from VW_PO_UPDATE_STATUS_CHECK Where PO_NO = '"+ master.PoNo +"'";

                    DataTable dtchk = dbHelper.GetDataTable(dbConn.SAConnStrReader(), qryCheck);

                    if (dtchk.Rows[0]["STATUS"].ToString().ToUpper() == "TRUE")
                    {
                        MaxID = master.PoNo;
                        MaxCode = master.PoMstSlno;
                        IUMode = "U";
                        string Qry = "Update PURCHASE_ORDER_MST set TOTAL_PO_AMOUNT_IN_BDT='" + master.TotalPoAmountBDT + "'," +
                              " DELIVERY_DATE=(TO_DATE('" + master.DeliveryDate + "','dd/MM/yyyy')), DELIVERY_ADDRESS='" + master.DeliveryAddress + "', INCOTERM_CODE='" + master.IncoTermCode + "', DESTINATION_PORT='" + master.DestinationPortCode + "', LOADING_PORT='" + master.LoadingPortCode + "', TRANSPORT_MODE='" + master.TransportModeCode + "', NOTIFY_DAYS='" + master.NotifyDays + "', TERMS_CONDITION='" + master.TermsCondition + "', TERMS_OF_PAYMENT='" + master.TermsOfPayment + "', TOTAL_PO_AMOUNT='" + master.TotalPoAmount + "', REMARKS='" + master.Remarks + "', PO_STATUS='" + master.PoStatus + "' Where  PO_NO='" + master.PoNo + "'";
                        if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                        {
                            if (master.PODetailList != null)
                            {
                                foreach (PurchaseOrderDtlBEL det in master.PODetailList)
                                {
                                    if (IsExists(master.PoMstSlno, det.ProductCode))
                                    {
                                        string subQry = "UPDATE PURCHASE_ORDER_DTL SET PO_QTY_IN_PACK = '" + det.NoOfPack + "', PO_QTY_IN_PCS = '" + det.NoOfPcs + "', PRICE_PER_PACK = '" + det.PricePerPack + "', TOTAL_AMOUNT = '" + det.TotalAmount + "', CONVERSION_RATE = '" + det.ConversionRate + "', TOTAL_AMOUNT_IN_BDT = '" + det.TotalAmountBDT + "' WHERE PRODUCT_CODE = '" + det.ProductCode + "' and PO_MST_SLNO = '" + master.PoMstSlno + "'";
                                        if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                        {
                                            isTrue = true;
                                        }
                                    }
                                    else
                                    {
                                        string dtlMaxID = idGenerated.getMAXSL("PURCHASE_ORDER_DTL", "PO_DTL_SLNO").ToString();
                                        string subQry = "INSERT INTO PURCHASE_ORDER_DTL(PO_DTL_SLNO, PO_MST_SLNO, PRODUCT_CODE, PO_QTY_IN_PACK, PO_QTY_IN_PCS, PRICE_PER_PACK, TOTAL_AMOUNT, CURRENCY_CODE, CONVERSION_RATE, TOTAL_AMOUNT_IN_BDT, COMPANY_CODE) " +
                                            "VALUES (" + dtlMaxID + "," + master.PoMstSlno + ",'" + det.ProductCode + "','" + det.NoOfPack + "','" + det.NoOfPcs + "','" + det.PricePerPack + "','" + det.TotalAmount + "','" + det.CurrencyCode + "','" + det.ConversionRate + "','" + det.TotalAmountBDT + "', '" + det.CompanyCode + "')";
                                        if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                        {
                                            isTrue = true;
                                        }
                                    }
                                }
                            }
                        }
                    }       /// End of Status Checking
                    
                }
                return isTrue;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }

        private bool IsExists(string PoMstSlno, string ProductCode)
        {
            bool isTrue = false;

            string Qry = "Select * from PURCHASE_ORDER_DTL Where PO_MST_SLNO = " + PoMstSlno + " AND PRODUCT_CODE = '" + ProductCode + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);

            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }


        //Created By Zahid
        public List<DestinationPortBEL> GetPortListByTransportMode(string transportModeCode)
        {
            string Qry = "SELECT DESTINATION_PORT_CODE, DESTINATION_PORT_NAME FROM DESTINATION_PORT_INFO " +
                        "WHERE TRANSPORT_MODE_CODE = '" + transportModeCode + "'" +
                        "ORDER BY DESTINATION_PORT_NAME";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<DestinationPortBEL> item;

            item = (from DataRow row in dt.Rows
                    select new DestinationPortBEL
                    {
                        PortCode = row["DESTINATION_PORT_CODE"].ToString(),
                        PortName = row["DESTINATION_PORT_NAME"].ToString(),
                    }).ToList();
            return item;
        }

        public List<PurchaseOrderMstBEL> GetPurchaseOrderListForReport(string fromDate, string toDate, string buyerCode)
        {
            string Qry = "SELECT A.PO_MST_SLNO, A.PO_NO, A.PO_DATE, A.BUYER_CODE, B.COUNTRY_CODE, B.BUYER_NAME,  B.EMAIL_ID, A.DELIVERY_ADDRESS, A.DELIVERY_DATE,  " +
                         "A.INCOTERM_CODE, A.DESTINATION_PORT, A.LOADING_PORT, A.NOTIFY_DAYS, A.ORDER_TYPE, TOTAL_PO_AMOUNT_IN_BDT, " +
                         "A.PRICE_TYPE, A.REMARKS, A.TERMS_CONDITION, A.TERMS_OF_PAYMENT, A.TOTAL_PO_AMOUNT, A.TRANSPORT_MODE " +
                         "FROM PURCHASE_ORDER_MST A, BUYER_INFO B " +
                         "WHERE A.BUYER_CODE = B.BUYER_CODE " +
                         "AND A.BUYER_CODE = '" + buyerCode + "'" +
                         "AND A.PO_DATE >= TO_DATE('"+ fromDate +"', 'dd-MM-yyyy') " +
                         "AND TO_DATE('" + toDate + "', 'dd-MM-yyyy') <=  A.PO_DATE " +
                         "ORDER BY A.PO_NO DESC";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PurchaseOrderMstBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PurchaseOrderMstBEL
                    {
                        PoMstSlno = row["PO_MST_SLNO"].ToString(),
                        PoNo = row["PO_NO"].ToString(),
                        PoDate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        Email = row["EMAIL_ID"].ToString(),
                        DeliveryAddress = row["DELIVERY_ADDRESS"].ToString(),
                        DeliveryDate = Convert.ToDateTime(row["DELIVERY_DATE"]).ToString("dd/MM/yyyy"),
                        IncoTermCode = row["INCOTERM_CODE"].ToString(),
                        OrderTypeCode = row["ORDER_TYPE"].ToString(),
                        PriceTypeCode = row["PRICE_TYPE"].ToString(),
                        DestinationPortCode = row["DESTINATION_PORT"].ToString(),
                        LoadingPortCode = row["LOADING_PORT"].ToString(),
                        TransportModeCode = row["TRANSPORT_MODE"].ToString(),
                        NotifyDays = row["NOTIFY_DAYS"].ToString(),
                        TermsCondition = row["TERMS_CONDITION"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        TotalPoAmount = row["TOTAL_PO_AMOUNT"].ToString(),
                        TotalPoAmountBDT = row["TOTAL_PO_AMOUNT_IN_BDT"].ToString(),
                        Remarks = row["REMARKS"].ToString(),
                    }).ToList();
            return item;
        }
    }
}