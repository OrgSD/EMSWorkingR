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
    public class ProformaInvoiceDAO : ReturnData
    {

        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public List<ProformaInvoiceMstBEL> GetProformaInvoiceList()
        {
            string Qry = "SELECT A.PO_NO, A.PRO_INV_MST_SLNO,  A.PROFORMA_INVOICE_NO, A.PROFORMA_INVOICE_DATE, A.PO_DATE, A.BUYER_CODE, B.BUYER_NAME, B.ADDRESS, A.ORDER_TYPE, O.ORDER_TYPE_NAME, A.DESTINATION_PORT, D.DESTINATION_PORT_NAME, A.LOADING_PORT, A.BANK_CODE, A.BRANCH_CODE, " +
                        "L.DESTINATION_PORT_NAME AS LOADING_PORT_NAME, A.INCOTERMS_CODE, I.INCOTERM_NAME, A.MODE_OF_SHIPMENT,  T.TRANSPORT_MODE_NAME, A.VALIDITY_OF_PROFORMA, A.TERMS_OF_PAYMENT, A.APPROVED_STATUS, A.APPROVED_REMARKS, " +
                        "A.TRANSHIPMENT, A.REMARKS, A.PARTIAL_SHIPMENT, A.PAYMENT_TYPE, A.CONSIGNEE_CODE, C.CONSIGNEE_NAME, A.NOTIFY_PARTY_CODE, N.NOTIFY_PARTY_NAME, A.COMPANY_CODE, COM.COMPANY_NAME, A.SAP_SO_NO, A.SAP_SO_DATE " +

                        "FROM PROFORMA_INVOICE_MST A,  INCOTERM_INFO I, ORDER_TYPE_INFO O, CONSIGNEE_INFO C, NOTIFY_PARTY_INFO N, " +
                        "TRANSPORT_MODE_INFO T, DESTINATION_PORT_INFO D, DESTINATION_PORT_INFO L, COMPANY_INFO COM, BUYER_INFO B " +

                        "WHERE A.INCOTERMS_CODE = I.INCOTERM_CODE  " +
                        "AND A.ORDER_TYPE = O.ORDER_TYPE_CODE  " +
                        "AND A.BUYER_CODE = B.BUYER_CODE " +
                        "AND A.COMPANY_CODE = COM.COMPANY_CODE " +
                        "AND A.NOTIFY_PARTY_CODE = N.NOTIFY_PARTY_CODE " +
                        "AND A.CONSIGNEE_CODE = C.CONSIGNEE_CODE "+
                        "AND A.MODE_OF_SHIPMENT = T.TRANSPORT_MODE_CODE  " +
                        "AND A.DESTINATION_PORT = D.DESTINATION_PORT_CODE  " +
                        "AND A.LOADING_PORT = L.DESTINATION_PORT_CODE " + 
                        "AND LTRIM(RTRIM(A.APPROVED_STATUS)) IS NULL";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProformaInvoiceMstBEL> item = new List<ProformaInvoiceMstBEL>();

            item = (from DataRow row in dt.Rows
                    select new ProformaInvoiceMstBEL
                    {
                        ProInvMstSlNo = row["PRO_INV_MST_SLNO"].ToString(),
                        ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                        ProformaInvoiceDate = Convert.ToDateTime(row["PROFORMA_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        PoNo = row["PO_NO"].ToString(),
                        PoDate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        BuyerAddress = row["ADDRESS"].ToString(),
                        BankCode = row["BANK_CODE"].ToString(),
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        CompanyName = row["COMPANY_NAME"].ToString(),
                        ConsigneeCode = row["CONSIGNEE_CODE"].ToString(),
                        ConsigneeName = row["CONSIGNEE_NAME"].ToString(),
                        BranchCode = row["BRANCH_CODE"].ToString(),
                        TransshipmentCode = row["TRANSHIPMENT"].ToString(),
                        PartialShipment = row["PARTIAL_SHIPMENT"].ToString(),
                        PaymentType = row["PAYMENT_TYPE"].ToString(),
                        IncoTermCode = row["INCOTERMS_CODE"].ToString(),
                        IncoTermName = row["INCOTERM_NAME"].ToString(),
                        OrderTypeCode = row["ORDER_TYPE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                        DestinationPortCode = row["DESTINATION_PORT"].ToString(),
                        DestinationPortName = row["DESTINATION_PORT_NAME"].ToString(),
                        LoadingPortCode = row["LOADING_PORT"].ToString(),
                        LoadingPortName = row["LOADING_PORT_NAME"].ToString(),
                        TransportModeCode = row["MODE_OF_SHIPMENT"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        NotifyDays = row["VALIDITY_OF_PROFORMA"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        Remarks = row["REMARKS"].ToString(),
                        SAPSoNo =row["SAP_SO_NO"].ToString(),
                        NotifyPartyCode = row["NOTIFY_PARTY_CODE"].ToString(),
                        NotifyPartyName = row["NOTIFY_PARTY_NAME"].ToString(),
                        ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                        ApprovedRemarks = row["APPROVED_REMARKS"].ToString(),
                        SAPSoDate = row["SAP_SO_DATE"].ToString() == "" ? "" : Convert.ToDateTime(row["SAP_SO_DATE"]).ToString("dd/MM/yyyy"),
                    }).ToList();
            return item;
        }

        public List<ProformaInvoiceMstBEL> GetProformaInvoiceForPackingGrid(string buyerCode, string companyCode)
        {
            string Qry = "SELECT A.PO_NO, A.PRO_INV_MST_SLNO, A.PROFORMA_INVOICE_NO, A.PROFORMA_INVOICE_DATE, A.PO_DATE, A.BUYER_CODE, B.BUYER_NAME, B.ADDRESS, A.ORDER_TYPE, O.ORDER_TYPE_NAME, A.DESTINATION_PORT, D.DESTINATION_PORT_NAME, A.LOADING_PORT, A.BANK_CODE, A.BRANCH_CODE, " +
                        "L.DESTINATION_PORT_NAME AS LOADING_PORT_NAME, A.INCOTERMS_CODE, I.INCOTERM_NAME, A.MODE_OF_SHIPMENT,  T.TRANSPORT_MODE_NAME, A.VALIDITY_OF_PROFORMA, A.TERMS_OF_PAYMENT, A.SAP_SO_NO, A.SAP_SO_DATE,   " +
                        "A.TRANSHIPMENT, A.REMARKS, A.PARTIAL_SHIPMENT, A.PAYMENT_TYPE, A.CONSIGNEE_CODE, C.CONSIGNEE_NAME, A.COMPANY_CODE, COM.COMPANY_NAME " +

                        "FROM PROFORMA_INVOICE_MST A,  INCOTERM_INFO I, ORDER_TYPE_INFO O, CONSIGNEE_INFO C, " +
                        "TRANSPORT_MODE_INFO T, DESTINATION_PORT_INFO D, DESTINATION_PORT_INFO L, COMPANY_INFO COM, BUYER_INFO B " +

                        "WHERE A.INCOTERMS_CODE = I.INCOTERM_CODE  " +
                        "AND A.ORDER_TYPE = O.ORDER_TYPE_CODE  " +
                        "AND A.BUYER_CODE = B.BUYER_CODE " +
                        "AND A.COMPANY_CODE = COM.COMPANY_CODE " +
                        "AND A.CONSIGNEE_CODE = C.CONSIGNEE_CODE " +
                        "AND A.MODE_OF_SHIPMENT = T.TRANSPORT_MODE_CODE  " +
                        "AND A.DESTINATION_PORT = D.DESTINATION_PORT_CODE  " +
                        "AND A.LOADING_PORT = L.DESTINATION_PORT_CODE " +
                        "AND A.APPROVED_STATUS = 'Approved' " +
                        "AND A.COMPANY_CODE = '"+ companyCode +"' " +
                        "AND A.BUYER_CODE = '"+ buyerCode +"' ";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProformaInvoiceMstBEL> item = new List<ProformaInvoiceMstBEL>();

            item = (from DataRow row in dt.Rows
                    select new ProformaInvoiceMstBEL
                    {
                        ProInvMstSlNo = row["PRO_INV_MST_SLNO"].ToString(),
                        ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                        ProformaInvoiceDate = Convert.ToDateTime(row["PROFORMA_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        PoNo = row["PO_NO"].ToString(),
                        PoDate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        SAPSoNo = row["SAP_SO_NO"].ToString(),
                        SAPSoDate = row["SAP_SO_DATE"].ToString() == "" ? "" : Convert.ToDateTime(row["SAP_SO_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        BuyerAddress = row["ADDRESS"].ToString(),
                        BankCode = row["BANK_CODE"].ToString(),
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        CompanyName = row["COMPANY_NAME"].ToString(),
                        ConsigneeCode = row["CONSIGNEE_CODE"].ToString(),
                        ConsigneeName = row["CONSIGNEE_NAME"].ToString(),
                        BranchCode = row["BRANCH_CODE"].ToString(),
                        TransshipmentCode = row["TRANSHIPMENT"].ToString(),
                        PartialShipment = row["PARTIAL_SHIPMENT"].ToString(),
                        PaymentType = row["PAYMENT_TYPE"].ToString(),
                        IncoTermCode = row["INCOTERMS_CODE"].ToString(),
                        IncoTermName = row["INCOTERM_NAME"].ToString(),
                        OrderTypeCode = row["ORDER_TYPE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                        DestinationPortCode = row["DESTINATION_PORT"].ToString(),
                        DestinationPortName = row["DESTINATION_PORT_NAME"].ToString(),
                        LoadingPortCode = row["LOADING_PORT"].ToString(),
                        LoadingPortName = row["LOADING_PORT_NAME"].ToString(),
                        TransportModeCode = row["MODE_OF_SHIPMENT"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        NotifyDays = row["VALIDITY_OF_PROFORMA"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        Remarks = row["REMARKS"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ProformaInvoiceDtlBEL> GetProductListForPackingByProformaID(string masterID)
        {
            string Qry = "SELECT A.PRO_INV_MST_SLNO, M.PROFORMA_INVOICE_NO, A.PRODUCT_CODE, B.PRODUCT_NAME, B.QTY_PER_PACK, A.PI_QTY_IN_PACK " +
                         "FROM PROFORMA_INVOICE_DTL A, PRODUCT_INFO B, PROFORMA_INVOICE_MST M " +
                         "WHERE A.PRO_INV_MST_SLNO = M.PRO_INV_MST_SLNO " +
                         "AND A.PRODUCT_CODE = B.PRODUCT_CODE " +
                         "AND A.PRO_INV_MST_SLNO in (" + masterID + ")";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProformaInvoiceDtlBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ProformaInvoiceDtlBEL
                    {
                        ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                        ProInvMstSlNo = row["PRO_INV_MST_SLNO"].ToString(),
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        NoOfPack = row["PI_QTY_IN_PACK"].ToString(),
                        QtyPerPack = row["QTY_PER_PACK"].ToString(),
                        PackingQtyInPack = row["PI_QTY_IN_PACK"].ToString(),
                    }).ToList();
            return item;
        }

        public bool SaveUpdate(ProformaInvoiceMstBEL master, string userID)
        {
            try
            {
                bool isTrue = false;
                string appBy = "";
                string appDate = "''";
                string appTerm = "";

                if (String.IsNullOrEmpty(master.ProInvMstSlNo))
                {
                    if (master.ApprovedStatus == "Approved" || master.ApprovedStatus == "Cancel")
                    {
                        appBy = userID;
                        appDate = "(TO_DATE('" + CurrentDate + "','dd/MM/yyyy'))";
                        appTerm = idGenerated.GetLanIPAddress();
                    }

                    MaxID = idGenerated.getMAXID("PROFORMA_INVOICE_MST", "PRO_INV_MST_SLNO", "fm00000");
                    master.ProformaInvoiceNo = idGenerated.getMAXID("PROFORMA_INVOICE_MST", "PROFORMA_INVOICE_NO", "fm00000");
                    IUMode = "I";
                    string Qry = "Insert into PROFORMA_INVOICE_MST(PRO_INV_MST_SLNO,PROFORMA_INVOICE_NO, PROFORMA_INVOICE_DATE ,PO_NO, PO_DATE, BUYER_CODE, COMPANY_CODE, CONSIGNEE_CODE, NOTIFY_PARTY_CODE, ORDER_TYPE, PRICE_TYPE, " +
                                "INCOTERMS_CODE,SAP_SO_NO,SAP_SO_DATE,MODE_OF_SHIPMENT,LOADING_PORT,DESTINATION_PORT,TRANSHIPMENT,PARTIAL_SHIPMENT, BANK_CODE, BRANCH_CODE, PAYMENT_TYPE, TERMS_OF_PAYMENT, VALIDITY_OF_PROFORMA,TOTAL_PI_AMOUNT, " +
                                "TOTAL_PI_AMOUNT_IN_BDT, REMARKS, ENTERED_BY, ENTERED_DATE, ENTERED_TERMINAL, APPROVED_STATUS, APPROVED_BY, APPROVED_DATE, APPROVED_REMARKS, APPROVED_TERMINAL ) " +
                            "Values(" + MaxID + ", '" + master.ProformaInvoiceNo + "', TO_DATE('" + master.ProformaInvoiceDate + "','dd/MM/yyyy'), '" + master.PoNo + "',TO_DATE('" + master.PoDate + "','dd/MM/yyyy'),'" + master.BuyerCode + "','" + master.CompanyCode + "','" + master.ConsigneeCode + "','" + master.NotifyPartyCode + "','" + master.OrderTypeCode + "','" + master.PriceTypeCode + "', " +
                            "'" + master.IncoTermCode + "', '" + master.SAPSoNo + "', TO_DATE('" + master.SAPSoDate + "','dd/MM/yyyy'), '" + master.TransportModeCode + "', '" + master.LoadingPortCode + "', '" + master.DestinationPortCode + "', '" + master.TransshipmentCode + "', '" + master.PartialShipment + "', '" + master.BankCode + "', '" + master.BranchCode + "', '" + master.PaymentType + "', '" + master.TermsOfPayment + "', '" + master.NotifyDays + "','" + master.TotalPIAmount + "', " +
                            "'" + master.TotalPIAmountBDT + "','" + master.Remarks + "', '" + userID + "',(TO_DATE('" + CurrentDate + "','dd/MM/yyyy')),'" + idGenerated.GetLanIPAddress() + "', '" + master.ApprovedStatus + "', '" + appBy + "' , "+ appDate +", '"+ master.ApprovedRemarks +"', '"+ appTerm +"' ) ";

                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {
                        if (master.ProformaDetailList != null)
                        {
                            foreach (ProformaInvoiceDtlBEL det in master.ProformaDetailList)
                            {
                                string dtlMaxID = idGenerated.getMAXID("PROFORMA_INVOICE_DTL", "PRO_INV_DTL_SLNO", "fm00000");

                                string subQry = "INSERT INTO PROFORMA_INVOICE_DTL(PRO_INV_DTL_SLNO, PRO_INV_MST_SLNO, PRODUCT_CODE, COMPANY_CODE, PI_QTY_IN_PACK, PI_QTY_IN_PCS, PRICE_PER_PACK, CURRENCY, TOTAL_AMOUNT, CONVERSION_RATE, TOTAL_AMOUNT_IN_BDT) " +
                                    "VALUES (" + dtlMaxID + "," + MaxID + ",'" + det.ProductCode + "','" + det.CompanyCode + "','" + det.NoOfPack + "','" + det.NoOfPcs + "','" + det.PricePerPack + "', '" + det.CurrencyCode + "', '" + det.TotalAmount + "', '" + det.ConversionRate + "','" + det.TotalAmountBDT + "')";
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

                    if (master.ApprovedStatus == "Approved" || master.ApprovedStatus == "Cancel")
                    {
                        appBy = userID;
                        appDate = "(TO_DATE('" + CurrentDate + "','dd/MM/yyyy'))";
                        appTerm = idGenerated.GetLanIPAddress();
                    }

                    MaxID = master.ProInvMstSlNo;
                    IUMode = "U";
                    string Qry = "Update PROFORMA_INVOICE_MST set CONSIGNEE_CODE='" + master.ConsigneeCode + "', NOTIFY_PARTY_CODE='" + master.NotifyPartyCode + "', TRANSHIPMENT= '" + master.TransshipmentCode + "', PARTIAL_SHIPMENT = '" + master.PartialShipment + "', BANK_CODE = '" + master.BankCode + "', BRANCH_CODE='" + master.BranchCode + "'," +
                          "COMPANY_CODE='" + master.CompanyCode + "', SAP_SO_NO='" + master.SAPSoNo + "', SAP_SO_DATE= (TO_DATE('" + master.SAPSoDate + "','dd/MM/yyyy')), PAYMENT_TYPE='" + master.PaymentType + "', TERMS_OF_PAYMENT='" + master.TermsOfPayment + "', VALIDITY_OF_PROFORMA='" + master.NotifyDays + "', REMARKS='" + master.Remarks + "', UPDATE_BY='" + userID + "', UPDATE_DATE= (TO_DATE('" + CurrentDate + "','dd/MM/yyyy')), UPDATE_TERMINAL='" + idGenerated.GetLanIPAddress() + "', " +
                          "APPROVED_REMARKS='" + master.ApprovedRemarks + "', APPROVED_BY='" + appBy + "', APPROVED_DATE= " + appDate + ", APPROVED_TERMINAL='" + appTerm + "', APPROVED_STATUS='" + master.ApprovedStatus + "' " + 
                          "Where PRO_INV_MST_SLNO= " + MaxID + "";
                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {

                        //if (master.ProformaDetailList != null)
                        //{
                        //    foreach (ProformaInvoiceDtlBEL det in master.ProformaDetailList)
                        //    {

                        //        if (!String.IsNullOrEmpty(det.ProInvDtlSlNo))
                        //        {
                        //            //MaxID = det.ProInvDtlSlNo;
                        //            //string subQry = "UPDATE PRODUCT_PRICE_DTL SET ORDER_TYPE_CODE = '" + det.OrderTypeCode + "', PRICE_TYPE_CODE = '" + det.PriceTypeCode + "', CURRENCY_CODE = '" + det.CurrencyCode + "', PRODUCT_PRICE = " + det.ProductPrice + ", LC_PRICE = " + det.LCPrice + " WHERE PRICE_DTL_SLNO = '" + MaxID + "'";
                        //            //if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                        //            //{
                        //            //    isTrue = true;
                        //            //}
                        //        }
                        //        else
                        //        {
                        //            //string dtlMaxID = idGenerated.getMAXID("PROFORMA_INVOICE_DTL", "PRO_INV_DTL_SLNO", "fm00000");
                        //            //string subQry = "INSERT INTO PROFORMA_INVOICE_DTL(PRO_INV_DTL_SLNO, PRO_INV_MST_SLNO, PRODUCT_CODE, COMPANY_CODE, PI_QTY_IN_PACK, PI_QTY_IN_PCS, PRICE_PER_PACK, CURRENCY, TOTAL_AMOUNT, CONVERSION_RATE, TOTAL_AMOUNT_IN_BDT) " +
                        //            //    "VALUES (" + dtlMaxID + "," + MaxID + ",'" + det.ProductCode + "','" + det.CompanyCode + "','" + det.NoOfPack + "','" + det.NoOfPcs + "','" + det.PricePerPack + "', '" + det.CurrencyCode + "', '" + det.TotalAmount + "', '" + det.ConversionRate + "','" + det.TotalAmountBDT + "')";
                        //            //if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                        //            //{
                        //            //    isTrue = true;
                        //            //}
                        //        }
                        //    }
                        //}

                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }

        public List<ProformaInvoiceMstBEL> GetProformaInvoiceListForReport(string buyerCode, string companyCode, string fromDate, string toDate)
        {
            string Qry = "SELECT A.PO_NO, A.PRO_INV_MST_SLNO, A.PROFORMA_INVOICE_NO, A.PROFORMA_INVOICE_DATE, A.PO_DATE, A.BUYER_CODE, B.BUYER_NAME, B.ADDRESS, A.ORDER_TYPE, O.ORDER_TYPE_NAME, A.DESTINATION_PORT, D.DESTINATION_PORT_NAME, A.LOADING_PORT, A.BANK_CODE, A.BRANCH_CODE, " +
                        "L.DESTINATION_PORT_NAME AS LOADING_PORT_NAME, A.INCOTERMS_CODE, I.INCOTERM_NAME, A.MODE_OF_SHIPMENT,  T.TRANSPORT_MODE_NAME, A.VALIDITY_OF_PROFORMA, A.TERMS_OF_PAYMENT, A.SAP_SO_NO, A.SAP_SO_DATE,   " +
                        "A.TRANSHIPMENT, A.REMARKS, A.PARTIAL_SHIPMENT, A.PAYMENT_TYPE, A.CONSIGNEE_CODE, C.CONSIGNEE_NAME, A.COMPANY_CODE, COM.COMPANY_NAME " +

                        "FROM PROFORMA_INVOICE_MST A,  INCOTERM_INFO I, ORDER_TYPE_INFO O, CONSIGNEE_INFO C, " +
                        "TRANSPORT_MODE_INFO T, DESTINATION_PORT_INFO D, DESTINATION_PORT_INFO L, COMPANY_INFO COM, BUYER_INFO B " +

                        "WHERE A.INCOTERMS_CODE = I.INCOTERM_CODE  " +
                        "AND A.ORDER_TYPE = O.ORDER_TYPE_CODE  " +
                        "AND A.BUYER_CODE = B.BUYER_CODE " +
                        "AND A.COMPANY_CODE = COM.COMPANY_CODE " +
                        "AND A.CONSIGNEE_CODE = C.CONSIGNEE_CODE " +
                        "AND A.MODE_OF_SHIPMENT = T.TRANSPORT_MODE_CODE  " +
                        "AND A.DESTINATION_PORT = D.DESTINATION_PORT_CODE  " +
                        "AND A.LOADING_PORT = L.DESTINATION_PORT_CODE " +
                        "AND A.APPROVED_STATUS = 'Approved' " +
                        "AND A.COMPANY_CODE = '" + companyCode + "' " +
                        "AND A.BUYER_CODE = '" + buyerCode + "' " +
                        "AND A.PROFORMA_INVOICE_DATE >= TO_DATE('" + fromDate + "', 'dd-MM-yyyy') " +
                        "AND TO_DATE('" + toDate + "', 'dd-MM-yyyy') <= A.PROFORMA_INVOICE_DATE";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ProformaInvoiceMstBEL> item = new List<ProformaInvoiceMstBEL>();

            item = (from DataRow row in dt.Rows
                    select new ProformaInvoiceMstBEL
                    {
                        ProInvMstSlNo = row["PRO_INV_MST_SLNO"].ToString(),
                        ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                        ProformaInvoiceDate = Convert.ToDateTime(row["PROFORMA_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        PoNo = row["PO_NO"].ToString(),
                        PoDate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        SAPSoNo = row["SAP_SO_NO"].ToString(),
                        SAPSoDate = row["SAP_SO_DATE"].ToString() == "" ? "" : Convert.ToDateTime(row["SAP_SO_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        BuyerAddress = row["ADDRESS"].ToString(),
                        BankCode = row["BANK_CODE"].ToString(),
                        CompanyCode = row["COMPANY_CODE"].ToString(),
                        CompanyName = row["COMPANY_NAME"].ToString(),
                        ConsigneeCode = row["CONSIGNEE_CODE"].ToString(),
                        ConsigneeName = row["CONSIGNEE_NAME"].ToString(),
                        BranchCode = row["BRANCH_CODE"].ToString(),
                        TransshipmentCode = row["TRANSHIPMENT"].ToString(),
                        PartialShipment = row["PARTIAL_SHIPMENT"].ToString(),
                        PaymentType = row["PAYMENT_TYPE"].ToString(),
                        IncoTermCode = row["INCOTERMS_CODE"].ToString(),
                        IncoTermName = row["INCOTERM_NAME"].ToString(),
                        OrderTypeCode = row["ORDER_TYPE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                        DestinationPortCode = row["DESTINATION_PORT"].ToString(),
                        DestinationPortName = row["DESTINATION_PORT_NAME"].ToString(),
                        LoadingPortCode = row["LOADING_PORT"].ToString(),
                        LoadingPortName = row["LOADING_PORT_NAME"].ToString(),
                        TransportModeCode = row["MODE_OF_SHIPMENT"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        NotifyDays = row["VALIDITY_OF_PROFORMA"].ToString(),
                        TermsOfPayment = row["TERMS_OF_PAYMENT"].ToString(),
                        Remarks = row["REMARKS"].ToString(),
                    }).ToList();
            return item;
        }

    }
}