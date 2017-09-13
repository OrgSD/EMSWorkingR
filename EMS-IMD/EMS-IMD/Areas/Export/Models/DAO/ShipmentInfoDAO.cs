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
    public class ShipmentInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public List<CommercialInvoiceAndBuyerDetailsBEL> GetCommercialInvoListForShipment()
        {
            string Qry = "SELECT A.COMMM_INV_FINAL_MST_SLNO, A.COMM_INVOICE_NO, A.COMM_INVOICE_DATE, B.BUYER_CODE, B.BUYER_NAME, C.COUNTRY_CODE, C.COUNTRY_NAME " +
                        "FROM COMM_INVOICE_FINAL_MST A, BUYER_INFO B, COUNTRY_INFO C " +
                        "WHERE A.BUYER_CODE = B.BUYER_CODE " +
                        "AND A.COUNTRY_CODE = C.COUNTRY_CODE " +
                        "AND (A.STATUS != 'Deleted' OR A.STATUS is null)";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<CommercialInvoiceAndBuyerDetailsBEL> item;

            item = (from DataRow row in dt.Rows
                    select new CommercialInvoiceAndBuyerDetailsBEL
                    {
                        CommInvoiceFinalMstSlNo = row["COMMM_INV_FINAL_MST_SLNO"].ToString(),
                        CommInvoiceNo = row["COMM_INVOICE_NO"].ToString(),
                        CommInvoiceDate = Convert.ToDateTime(row["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                    }).ToList();
            return item;
        }

        public List<ShipmentDtlBEL> GetProformaListForShipment(string masterID)
        {
            string Qry = "SELECT C.PROFORMA_INVOICE_NO, C.PROFORMA_INVOICE_DATE, D.PACKING_LIST_SLNO, D.PACKING_DATE " +
                         "FROM COMM_INVOICE_FINAL_MST A, COMM_INVOICE_FINAL_DTL B " +
                         "LEFT JOIN PROFORMA_INVOICE_MST C ON B.PROFORMA_INVOICE_NO = C.PROFORMA_INVOICE_NO " +
                         "LEFT JOIN PACKING_LIST_MST D ON B.PACKING_LIST_SLNO = D.PACKING_LIST_SLNO " +
                         "WHERE A.COMMM_INV_FINAL_MST_SLNO = B.COMMM_INV_FINAL_MST_SLNO " +
                         "AND A.COMMM_INV_FINAL_MST_SLNO = " + masterID + "";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<ShipmentDtlBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ShipmentDtlBEL
                    {
                        ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                        ProformaInvoiceDate = Convert.ToDateTime(row["PROFORMA_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        PackingListDate = Convert.ToDateTime(row["PACKING_DATE"]).ToString("dd/MM/yyyy"),
                        PackingListSlNo = row["PACKING_LIST_SLNO"].ToString(),
                    }).ToList();
            return item;
        }

        public bool SaveUpdate(ShipmentMstBEL master, string userID)
        {
            try
            {
                bool isTrue = false;

                if (String.IsNullOrEmpty(master.ShipmentMstSlNo))
                {

                    MaxID = idGenerated.getMAXSL("SHIPMENT_MST", "SHIPMENT_MST_SLNO").ToString();

                    IUMode = "I";
                    string Qry = "Insert into SHIPMENT_MST(SHIPMENT_MST_SLNO, SHIPMENT_DATE, COMM_INVOICE_NO, COMM_INVOICE_DATE, BUYER_CODE, COUNTRY_CODE, BL_NO, BL_DATE, BL_SCAN_REF, EXP_NO, EXP_DATE, EXP_SCAN_REF, PAYMENT_ADVAICE_SCAN_REF, INSURANCE_STATUS, REMARKS, ENTERED_BY, ENTERED_DATE, ENTERED_TERMINAL) " +
                            "Values(" + MaxID + ", TO_DATE('" + master.ShipmentDate + "','dd/MM/yyyy'), '" + master.CommInvoiceNo + "', TO_DATE('" + master.CommInvoiceDate + "','dd/MM/yyyy'),'" + master.BuyerCode + "','" + master.CountryCode + "','" + master.BlNo + "',TO_DATE('" + master.BlDate + "','dd/MM/yyyy'), '" + master.BlScanRef + "', '" + master.ExpNo + "', TO_DATE('" + master.ExpDate + "','dd/MM/yyyy'), '" + master.ExpScanRef + "', '" + master.PaymentAdviceScanRef + "', '" + master.InsuranceStatus + "','" + master.Remarks + "', '" + userID + "',(TO_DATE('" + CurrentDate + "','dd/MM/yyyy')),'" + idGenerated.GetLanIPAddress() + "')";

                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {
                        if (master.ShipmentDetailList != null)
                        {
                            foreach (ShipmentDtlBEL det in master.ShipmentDetailList)
                            {
                                string dtlMaxID = idGenerated.getMAXSL("SHIPMENT_DTL", "SHIPMENT_DTL_SLNO").ToString();

                                string subQry = "INSERT INTO SHIPMENT_DTL(SHIPMENT_DTL_SLNO, SHIPMENT_MST_SLNO, PACKING_LIST_SLNO, PACKING_LIST_DATE, PROFORMA_INVOICE_NO, PROFORMA_INVOICE_DATE) " +
                                    "VALUES (" + dtlMaxID + "," + MaxID + "," + det.PackingListSlNo + ", TO_DATE('" + det.PackingListDate + "','dd/MM/yyyy'), '" + det.ProformaInvoiceNo + "',TO_DATE('" + det.ProformaInvoiceDate + "','dd/MM/yyyy'))";
                                
                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
                                }
                            }
                        }


                        if (master.ShipmentDocumentList != null)
                        {
                            foreach (ShipmentDocumentBEL det in master.ShipmentDocumentList)
                            {
                                string dtlMaxID = idGenerated.getMAXSL("SHIPMENT_DOCUMENT", "SHIPMENT_DOC_SLNO").ToString();

                                string subQry = "INSERT INTO SHIPMENT_DOCUMENT(SHIPMENT_DOC_SLNO, SHIPMENT_MST_SLNO, DOCUMENT_NAME, REMARKS, DOC_REF) " +
                                    "VALUES (" + dtlMaxID + "," + MaxID + ",'" + det.DocumentName + "', '" + det.DocumentRemarks + "', '" + det.DocRef + "')";

                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
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

        public List<CommercialInvoiceAndBuyerDetailsBEL> GetShipmentForRegulatory()
        {
            string Qry = "SELECT S.SHIPMENT_MST_SLNO, S.SHIPMENT_DATE, A.COMM_INVOICE_NO, A.COMM_INVOICE_DATE, B.BUYER_CODE, B.BUYER_NAME, C.COUNTRY_CODE, C.COUNTRY_NAME " +
                        "FROM SHIPMENT_MST S, COMM_INVOICE_FINAL_MST A, BUYER_INFO B, COUNTRY_INFO C " +
                        "WHERE A.BUYER_CODE = B.BUYER_CODE " +
                        "AND S.COMM_INVOICE_NO = A.COMM_INVOICE_NO " +
                        "AND A.COUNTRY_CODE = C.COUNTRY_CODE ";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<CommercialInvoiceAndBuyerDetailsBEL> item;

            item = (from DataRow row in dt.Rows
                    select new CommercialInvoiceAndBuyerDetailsBEL
                    {
                        ShipmentMstSlNo = row["SHIPMENT_MST_SLNO"].ToString(),
                        ShipmentDate = Convert.ToDateTime(row["SHIPMENT_DATE"]).ToString("dd/MM/yyyy"),
                        CommInvoiceNo = row["COMM_INVOICE_NO"].ToString(),
                        CommInvoiceDate = Convert.ToDateTime(row["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                    }).ToList();
            return item;
        }
   
    }
}