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
    public class PackingListDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");

        public bool SaveUpdate(PackingListMstBEL master, string userID)
        {
            try
            {
                bool isTrue = false;

                string Qry = "";
                string dtlMaxID = "";
                if (master.PackingListSlNo == null || master.PackingListSlNo.Length == 0)
                {
                    MaxID = idGenerated.getMAXID("PACKING_LIST_MST", "PACKING_LIST_SLNO", "fm00000");
                    IUMode = "I";
                    Qry = "Insert into PACKING_LIST_MST(PACKING_LIST_SLNO, PACKING_DATE, COMPANY_CODE, BUYER_CODE, NO_OF_CTN, NO_OF_PACK, NET_WEIGHT, NET_WEIGHT_UNIT, GROSS_WEIGHT, GROSS_WEIGHT_UNIT, REMARKS, ENTERED_BY, ENTERED_DATE, ENTERED_TERMINAL) " +
                          "Values('" + MaxID + "', (TO_DATE('" + master.PackingDate + "','dd/MM/yyyy')), '" + master.CompanyCode + "', '" + master.BuyerCode + "','" + master.NoOfCtn + "','" + master.NoOfPack + "','" + master.NetWeight + "','" + master.NetWeightUnit + "','" + master.GrossWeight + "','" + master.GrossWeightUnit + "','" + master.Remarks + "','" + userID + "',(TO_DATE('" + CurrentDate + "','dd/MM/yyyy')),'" + idGenerated.GetLanIPAddress() + "')";

                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                    {                       
                         //Packing List Proforma Master Information
                        if (master.PackingProformaMSTList != null)
                        {                            
                            foreach (PackingListProformaMstBEL det in master.PackingProformaMSTList)
                            {
                                dtlMaxID = idGenerated.getMAXID("PACKING_LIST_PROFORMA", "PACKING_LIST_PI_SLNO", "fm00000");

                                string subQry = "INSERT INTO PACKING_LIST_PROFORMA(PACKING_LIST_PI_SLNO, PACKING_LIST_SLNO, PROFORMA_INVOICE_NO, PROFORMA_INVOICE_DATE, SAP_SO_NO, SAP_SO_DATE, PO_NO, PO_DATE) " +
                                    "VALUES (" + dtlMaxID + "," + MaxID + ",'" + det.ProformaInvoiceNo + "',(TO_DATE('" + det.ProformaInvoiceDate + "','dd/MM/yyyy')),'" + det.SAPSONo + "',(TO_DATE('" + det.SAPSODate + "','dd/MM/yyyy')),'" + det.PoNo + "', (TO_DATE('" + det.PoDate + "','dd/MM/yyyy')))";
                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
                                }
                            }
                        }

                         //Packing List Proforma Details Information
                        if (master.PackingProformaDTLList != null)
                        {
                            foreach (PackingListProformaDtlBEL det in master.PackingProformaDTLList)
                            {
                                string PIdtlMaxID = idGenerated.getMAXID("PACKING_LIST_PROFORMA_DTL", "PACKING_LIST_PI_DTL_SLNO", "fm00000");

                                string subQry = "INSERT INTO PACKING_LIST_PROFORMA_DTL(PACKING_LIST_PI_DTL_SLNO, PROFORMA_INVOICE_NO, PRODUCT_CODE, PI_QTY_IN_PACK, PI_QTY_IN_PCS, PACKING_QTY_IN_PACK, PACKING_QTY_IN_PCS, PACKING_LIST_SLNO) " +
                                    "VALUES (" + PIdtlMaxID + ",'" + det.PackingListPISlNo + "','" + det.ProductCode + "','" + det.PIQtyInPack + "','" + det.PIQtyInPcs + "','" + det.PackingQtyInPack + "','" + det.PackingQtyInPcs + "'," + MaxID + ")";
                                
                                if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                                {
                                    isTrue = true;
                                }
                            }
                        }

                     
                    }                
                }

                return isTrue;
                //else
                //{//U for Insert
                //    MaxID = master.CompanyCode;
                //    IUMode = "U";
                //    Qry = "Update COMPANY_INFO set COMPANY_NAME='" + master.CompanyName + "', ADDRESS='" + master.Address + "', EMAIL_ID='" + master.EmailID + "', CONTACT_NO='" + master.ContactNo + "' Where COMPANY_CODE='" + master.CompanyCode + "'";
                //}
                //if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), Qry))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }

        public bool FinalizeData(PackingListMstBEL master)
        {
            bool isTrue = false;

            //Packing List Product Details

            if (master.PackingProdDetailsList != null)
            {
                foreach (PackingListProdDetailsBEL det in master.PackingProdDetailsList)
                {
                    string PIdtlMaxID = idGenerated.getMAXID("PACKING_LIST_PRODUCT_DETAILS", "PACKING_LIST_PROD_SLNO", "fm00000");

                    string subQry = "INSERT INTO PACKING_LIST_PRODUCT_DETAILS(PACKING_LIST_PROD_SLNO, PACKING_LIST_SLNO, PRODUCT_CODE, MC_SLNO, NO_OF_MC, NO_OF_SC_PER_MC, PACK_PER_SC, PACKING_QTY_IN_PACK, BATCH_NO, MANUFACTURING_DATE, EXPIRY_DATE, LENGTH, WIDTH, HEIGHT, NET_WEIGHT, NET_WEIGHT_UNIT, GROSS_WEIGHT, GROSS_WEIGHT_UNIT) " +
                        "VALUES (" + PIdtlMaxID + "," + det.PackingListSlNo + ",'" + det.ProductCode + "','" + det.MCSlNo + "','" + det.NoOfMC + "','" + det.NoOfScPerMc + "','" + det.PackPerSc + "','" + det.PackingQtyInPack + "','" + det.BatchNo + "',(TO_DATE('" + det.ManufacturingDate + "','dd/MM/yyyy')),(TO_DATE('" + det.ExpiryDate + "','dd/MM/yyyy')),'" + det.Length + "','" + det.Width + "','" + det.Height + "','" + det.NetWeight + "','" + det.NetWeightUnit + "','" + det.GrossWeight + "','" + det.GrossWeightUnit + "')";
                    if (dbHelper.CmdExecute(dbConn.SAConnStrReader(), subQry))
                    {
                        isTrue = true;
                    }
                }
            }

            return isTrue;
        }

        public List<PackingListProformaDtlBEL> GetProformaProductListForFinalization(string packingMasterID)
        {
            string Qry = "SELECT B.PRODUCT_NAME, A.PRODUCT_CODE, SUM(NVL(A.PACKING_QTY_IN_PACK,0))  as PACKING_QTY  " +
                          "FROM PACKING_LIST_PROFORMA_DTL A, PRODUCT_INFO B " +
                          "WHERE A.PRODUCT_CODE = B.PRODUCT_CODE " +
                          //"AND A.PACKING_LIST_SLNO = " + 5 + " " +
                          "AND A.PACKING_LIST_SLNO = " + packingMasterID + " " +
                          "GROUP BY B.PRODUCT_NAME, A.PRODUCT_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PackingListProformaDtlBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PackingListProformaDtlBEL
                    {
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        PackingQtyInPack = row["PACKING_QTY"].ToString(),
                        Qty = row["PACKING_QTY"].ToString(),
                    }).ToList();
            return item;

        }

        public List<PackingListMstBEL> GetPackingListForCOA()
        {
            string Qry = "SELECT DISTINCT PACKING_LIST_SLNO, PACKING_DATE, COMPANY_NAME FROM VW_PACKING_LIST_PENDING_BATCH";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PackingListMstBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PackingListMstBEL
                    {
                        PackingListSlNo = row["PACKING_LIST_SLNO"].ToString(),
                        PackingDate = Convert.ToDateTime(row["PACKING_DATE"]).ToString("dd/MM/yyyy"),
                        CompanyName = row["COMPANY_NAME"].ToString()
                    }).ToList();
            return item;
        }


    }
}