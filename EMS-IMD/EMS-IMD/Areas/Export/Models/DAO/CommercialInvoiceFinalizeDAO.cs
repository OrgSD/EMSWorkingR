using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.DAL.Gateway;
using EMS_IMD.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.DAO
{
    public class CommercialInvoiceFinalizeDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        bool isTrue = false;
        public List<CommercialInvoiceFinalizeBEL> GetInvoiceInfo(string BuyerList)
        {

            string Qry = " Select * From VW_COMMERCIAL_INVOICE_PORT Where BUYER_CODE in (" + BuyerList + ")";

            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialInvoiceFinalizeBEL> getData = new List<CommercialInvoiceFinalizeBEL>();
            while (reader.Read())
            {
                CommercialInvoiceFinalizeBEL modelData = new CommercialInvoiceFinalizeBEL();
                //modelData.CommInvFinalMstSl = Convert.ToInt64(reader["COMMM_INV_FINAL_MST_SLNO"].ToString());
                modelData.CommInvoiceNo = reader["COMM_INVOICE_NO"].ToString();
                modelData.BuyerCode = reader["BUYER_CODE"].ToString();
                modelData.BuyerName = reader["BUYER_NAME"].ToString();
                modelData.CountryCode = reader["COUNTRY_CODE"].ToString();
                modelData.CountryName = reader["COUNTRY_NAME"].ToString();
                //modelData.CommInvoMSTSlno = Convert.ToInt64(reader["COMMM_INVOICE_MST_SLNO"].ToString());
                //modelData.CommInvoDTLSlno = Convert.ToInt64(reader["COMM_INVOICE_DTL_SLNO"].ToString());
                modelData.DestinationPort = reader["DESTINATION_PORT"].ToString();
                modelData.DestinationPortName = reader["DESTINATION_PORT_NAME"].ToString();
                modelData.Loadingport = reader["LOADING_PORT"].ToString();
                modelData.LoadingportName = reader["LOADING_PORT_NAME"].ToString();
                modelData.ModeOfShipment = reader["MODE_OF_SHIPMENT"].ToString();
                modelData.ModeOfShipmentName = reader["MODE_OF_SHIPMENT_NAME"].ToString();
                modelData.CommInvoiceDate = Convert.ToDateTime(reader["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                //modelData.NetInvoiceAmount = Convert.ToInt32(reader["NET_INVOICE_AMOUNT"].ToString());

                //if (!reader.IsDBNull(19))
                //{
                //    modelData.OrderQtyInPack = reader.GetInt32(19);
                //}
                //else
                //{
                //    modelData.OrderQtyInPack = 0;
                //}
                getData.Add(modelData);
            }
            con.Close();
            return getData;
        }

        public List<CommercialFinalInvoiceProductDetail> GetProductInfo(string CommInvoiceNo)
        {

            string Qry = " Select * From VW_COMMERCIAL_INVOICE_PRODUCT Where COMM_INVOICE_NO = '" + CommInvoiceNo + "' ";

            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialFinalInvoiceProductDetail> getData = new List<CommercialFinalInvoiceProductDetail>();
            while (reader.Read())
            {
                CommercialFinalInvoiceProductDetail modelData = new CommercialFinalInvoiceProductDetail();
                modelData.CommInvoiceNo = reader["COMM_INVOICE_NO"].ToString();
                modelData.CommInvoiceDate = reader["COMM_INVOICE_DATE"].ToString();
                modelData.ProductCode = reader["PRODUCT_CODE"].ToString();
                modelData.ProductName = reader["PRODUCT_NAME"].ToString();
                modelData.BuyerCode = reader["BUYER_CODE"].ToString();
                modelData.ProductName = reader["PRODUCT_NAME"].ToString();
                modelData.ProductName = reader["PRODUCT_NAME"].ToString();
                modelData.PackSize = reader["PACK_SIZE"].ToString();
                modelData.OrderQtyInPack = reader["PO_QTY_IN_PACK"].ToString();
                modelData.OrderQtyInPcs = reader["PO_QTY_IN_PCS"].ToString();
                
                //modelData.OrderQtyInPack = Convert.ToInt32(reader["PO_QTY_IN_PACK"].ToString());
                
                //modelData.CommInvoMSTSlno = Convert.ToInt64(reader["COMMM_INVOICE_MST_SLNO"].ToString());
                //modelData.CommInvoDTLSlno = Convert.ToInt64(reader["COMM_INVOICE_DTL_SLNO"].ToString());
                modelData.InvoiceQtyInPack = reader["INVOICE_QTY_IN_PACK"].ToString();
                modelData.InvoiceQtyInPcs = reader["INVOICE_QTY_IN_PCS"].ToString();
               
                modelData.PricePerPack = Convert.ToDecimal(reader["PRICE_PER_PACK"].ToString());
                modelData.Currency = reader["CURRENCY"].ToString();
                modelData.TotalAmount = Convert.ToDecimal(reader["TOTAL_AMOUNT"].ToString());
                modelData.ConversionRate = Convert.ToDecimal(reader["CONVERSION_RATE"].ToString());
                modelData.TotalAmountBDT = Convert.ToDecimal(reader["TOTAL_AMOUNT_IN_BDT"].ToString());

                //modelData.NetInvoiceAmount = Convert.ToInt32(reader["NET_INVOICE_AMOUNT"].ToString());

                //if (!reader.IsDBNull(19))
                //{
                //    modelData.OrderQtyInPack = reader.GetInt32(19);
                //}
                //else
                //{
                //    modelData.OrderQtyInPack = 0;
                //}
                getData.Add(modelData);
            }
            con.Close();
            return getData;
        }
        public List<CommercialFinalInvoiceProductDetail> GetProductInfoSearch()
        {

            string Qry = " Select c.*,D.PRODUCT_NAME From COMM_INVOICE_FINAL_PRODUCT_DTL c LEFT JOIN PRODUCT_INFO d ON  D.PRODUCT_CODE = C.PRODUCT_CODE ";

            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialFinalInvoiceProductDetail> getData = new List<CommercialFinalInvoiceProductDetail>();
            while (reader.Read())
            {
                CommercialFinalInvoiceProductDetail modelData = new CommercialFinalInvoiceProductDetail();
                modelData.CommInvFinalMstSl = Convert.ToInt64(reader["COMMM_INV_FINAL_MST_SLNO"].ToString());
                modelData.CommInvoiceFinalProdSlno = Convert.ToInt64(reader["COMM_INV_FINAL_PROD_SLNO"].ToString());
                //modelData.CommInvoiceNo = reader["COMM_INVOICE_NO"].ToString();
                //modelData.CommInvoiceNo = reader["COMM_INVOICE_NO"].ToString();
                //modelData.CommInvoiceDate = reader["COMM_INVOICE_DATE"].ToString();
                modelData.ProductCode = reader["PRODUCT_CODE"].ToString();
                modelData.ProductName = reader["PRODUCT_NAME"].ToString();
                //modelData.BuyerCode = reader["BUYER_CODE"].ToString();
                //modelData.ProductName = reader["PRODUCT_NAME"].ToString();
                //modelData.ProductName = reader["PRODUCT_NAME"].ToString();
                //modelData.PackSize = reader["PACK_SIZE"].ToString();
                modelData.OrderQtyInPack =reader["PO_QTY_IN_PACK"].ToString();
                modelData.OrderQtyInPcs = reader["PO_QTY_IN_PCS"].ToString();

                //modelData.OrderQtyInPack = Convert.ToInt32(reader["PO_QTY_IN_PACK"].ToString());

                //modelData.CommInvoMSTSlno = Convert.ToInt64(reader["COMMM_INVOICE_MST_SLNO"].ToString());
                //modelData.CommInvoDTLSlno = Convert.ToInt64(reader["COMM_INVOICE_DTL_SLNO"].ToString());
                modelData.InvoiceQtyInPack = reader["INVOICE_QTY_IN_PACK"].ToString();
                modelData.InvoiceQtyInPcs = reader["INVOICE_QTY_IN_PCS"].ToString();

                modelData.PricePerPack = Convert.ToDecimal(reader["PRICE_PER_PACK"].ToString());
                modelData.Currency = reader["CURRENCY"].ToString();
                modelData.TotalAmount = Convert.ToDecimal(reader["TOTAL_AMOUNT"].ToString());
                modelData.ConversionRate = Convert.ToDecimal(reader["CONVERSION_RATE"].ToString());
                modelData.TotalAmountBDT = Convert.ToDecimal(reader["TOTAL_AMOUNT_IN_BDT"].ToString());

                //modelData.NetInvoiceAmount = Convert.ToInt32(reader["NET_INVOICE_AMOUNT"].ToString());

                //if (!reader.IsDBNull(19))
                //{
                //    modelData.OrderQtyInPack = reader.GetInt32(19);
                //}
                //else
                //{
                //    modelData.OrderQtyInPack = 0;
                //}
                getData.Add(modelData);
            }
            con.Close();
            return getData;
        }

        public List<CommercialInvoiceFinalizeBEL> InvoiceInfoSearch(string BuyerList)
        {

            //string Qry = " Select * From COMM_INVOICE_FINAL_MST";
            //string Qry = "Select b.*,C.COUNTRY_NAME,D.BUYER_NAME,E.DESTINATION_PORT_NAME,F.LOADING_PORT_NAME,G.TRANSPORT_MODE_NAME From COMM_INVOICE_FINAL_MST b LEFT JOIN COUNTRY_INFO c ON b.COUNTRY_CODE = c.COUNTRY_CODE  LEFT JOIN BUYER_INFO d ON B.BUYER_CODE = d.BUYER_CODE LEFT JOIN DESTINATION_PORT_INFO e ON B.DESTINATION_PORT = E.DESTINATION_PORT_CODE LEFT JOIN LOADING_PORT_INFO f ON B.LOADING_PORT = f.LOADING_PORT_CODE LEFT JOIN TRANSPORT_MODE_INFO g ON B.MODE_OF_SHIPMENT = G.TRANSPORT_MODE_CODE Where BUYER_CODE in (" + BuyerList + ") ";
            string Qry = "Select   b.COMMM_INV_FINAL_MST_SLNO,b.COMM_INVOICE_NO,TO_CHAR (b.COMM_INVOICE_DATE, 'dd-MM-yyyy') COMM_INVOICE_DATE,b.BUYER_CODE,b.COUNTRY_CODE ," +
                        " b.EXP_NO,TO_CHAR (b.EXP_DATE, 'dd-MM-yyyy') EXP_DATE,b.LC_NO,TO_CHAR (b.LC_DATE, 'dd-MM-yyyy') LC_DATE,b.HS_CODE,b.BANK_CODE, "+
                        " b.BRANCH_CODE,b.MODE_OF_SHIPMENT,b.DESTINATION_PORT,b.LOADING_PORT,C.COUNTRY_NAME,D.BUYER_NAME,E.DESTINATION_PORT_NAME, "+
                        " F.LOADING_PORT_NAME,G.TRANSPORT_MODE_NAME From COMM_INVOICE_FINAL_MST b LEFT JOIN COUNTRY_INFO c ON b.COUNTRY_CODE = c.COUNTRY_CODE  "+
                        " LEFT JOIN BUYER_INFO d ON B.BUYER_CODE = d.BUYER_CODE LEFT JOIN DESTINATION_PORT_INFO e ON B.DESTINATION_PORT = E.DESTINATION_PORT_CODE "+
                        " LEFT JOIN LOADING_PORT_INFO f ON B.LOADING_PORT = f.LOADING_PORT_CODE " +
                        " LEFT JOIN TRANSPORT_MODE_INFO g ON B.MODE_OF_SHIPMENT = G.TRANSPORT_MODE_CODE Where b.BUYER_CODE in (" + BuyerList + ")";
            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialInvoiceFinalizeBEL> getData = new List<CommercialInvoiceFinalizeBEL>();
            while (reader.Read())
            {
                CommercialInvoiceFinalizeBEL modelData = new CommercialInvoiceFinalizeBEL();
                //modelData.CommInvFinalMstSl = Convert.ToInt64(reader["COMMM_INV_FINAL_MST_SLNO"].ToString());
                modelData.CommInvFinalMstSl = Convert.ToInt64(reader["COMMM_INV_FINAL_MST_SLNO"].ToString());
                modelData.CommInvoiceNo = reader["COMM_INVOICE_NO"].ToString();
                modelData.BuyerCode = reader["BUYER_CODE"].ToString();
                modelData.BuyerName = reader["BUYER_NAME"].ToString();
                modelData.CountryCode = reader["COUNTRY_CODE"].ToString();
                modelData.CountryName = reader["COUNTRY_NAME"].ToString();
                modelData.ExpNo = reader["EXP_NO"].ToString();
                modelData.ExpDate = Convert.ToDateTime(reader["EXP_DATE"]).ToString("dd/MM/yyyy");
                modelData.LcNo = reader["LC_NO"].ToString();
                modelData.LcDate = Convert.ToDateTime(reader["LC_DATE"]).ToString("dd/MM/yyyy");
                modelData.HsCode = reader["HS_CODE"].ToString();
                //modelData.LcDate = reader["LC_DATE"].ToString();
                //modelData.CommInvoMSTSlno = Convert.ToInt64(reader["COMMM_INVOICE_MST_SLNO"].ToString());
                //modelData.CommInvoDTLSlno = Convert.ToInt64(reader["COMM_INVOICE_DTL_SLNO"].ToString());
                modelData.DestinationPort = reader["DESTINATION_PORT"].ToString();
                modelData.DestinationPortName = reader["DESTINATION_PORT_NAME"].ToString();
                modelData.Loadingport = reader["LOADING_PORT"].ToString();
                modelData.LoadingportName = reader["LOADING_PORT_NAME"].ToString();
                modelData.ModeOfShipment = reader["MODE_OF_SHIPMENT"].ToString();
                modelData.ModeOfShipmentName = reader["TRANSPORT_MODE_NAME"].ToString();
                modelData.CommInvoiceDate = Convert.ToDateTime(reader["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                //modelData.NetInvoiceAmount = Convert.ToInt32(reader["NET_INVOICE_AMOUNT"].ToString());

                //if (!reader.IsDBNull(19))
                //{
                //    modelData.OrderQtyInPack = reader.GetInt32(19);
                //}
                //else
                //{
                //    modelData.OrderQtyInPack = 0;
                //}
                getData.Add(modelData);
            }
            con.Close();
            return getData;
        }

        private bool IsMSTExitsByCommInvoiceNo(string CommInvoiceNo)
        {
            bool isTrue = false;
            string Qry = "SELECT COMMM_INV_FINAL_MST_SLNO FROM COMM_INVOICE_FINAL_MST WHERE COMM_INVOICE_NO = '" + CommInvoiceNo + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }

        public Int64 GetMstSLNO(string CommInvoiceNo)
        {
            Int64 SL = 0;
            string Qry = "Select COMMM_INV_FINAL_MST_SLNO from COMM_INVOICE_FINAL_MST WHERE COMM_INVOICE_NO='" + CommInvoiceNo + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            if (dt.Rows.Count > 0)
            {
                SL = Convert.ToInt64(dt.Rows[0][0].ToString());
            }

            return SL;
        }

        private bool IsDTLExits(string ProductCode)
        {
            bool isTrue = false;
            string Qry = "SELECT COMM_INV_FINAL_PROD_SLNO FROM COMM_INVOICE_FINAL_PRODUCT_DTL WHERE PRODUCT_CODE = '" + ProductCode + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }

        public bool SaveUpdate(CommercialInvoiceFinalizeBEL master)
        {
            try
            {
                string qry = "";
                Int64? MstSl;
                Int64? PDtlSL;

                if (IsMSTExitsByCommInvoiceNo(master.CommInvoiceNo))
                {
                    MstSl = GetMstSLNO(master.CommInvoiceNo);
                    //qry = "Update COMM_INVOICE_MST Set COMM_INVOICE_DATE =(TO_DATE('" + master.CommInvoiceDate + "','dd/mm/yyyy')), BUYER_CODE = '" + master.BuyerCode + "', NET_INVOICE_AMOUNT = '" + master.NetInvoiceAmount + "' Where COMMM_INVOICE_MST_SLNO = '" + MstSl + "'";
                    qry = "Update COMM_INVOICE_FINAL_MST set COMM_INVOICE_NO = '" + master.CommInvoiceNo + "',COMM_INVOICE_DATE= (TO_DATE('" + master.CommInvoiceDate + "','dd-MM-yyyy')),BUYER_CODE = '" + master.BuyerCode + "',COUNTRY_CODE = '" + master.CountryCode + "',EXP_NO= '" + master.ExpNo + "',EXP_DATE = (TO_DATE('" + master.ExpDate + "','dd-MM-yyyy')),LC_NO = '" + master.LcNo + "',LC_DATE = (TO_DATE('" + master.LcDate + "','dd-MM-yyyy')), HS_CODE = '" + master.HsCode + "',BANK_CODE = '" + master.BankCode + "',BRANCH_CODE = '" + master.BranchCode + "',MODE_OF_SHIPMENT= '" + master.ModeOfShipment + "', DESTINATION_PORT = '" + master.DestinationPort + "', LOADING_PORT = '" + master.Loadingport + "', NET_INVOICE_AMOUNT = '" + master.NetInvAmount + "' Where COMMM_INV_FINAL_MST_SLNO = " + MstSl + "";

                    long abc = dbHelper.CmdExecute(qry);
                    if (abc > 0)
                    {
                        if (master.productList != null)
                        {
                            foreach (CommercialFinalInvoiceProductDetail detailModel in master.productList)
                            {
                                //if ((IsMSTExits(MstSL)) && (IsDTLExits(PackingList, MstSL)))
                                //if ((IsMSTExits(MstSl)) && (IsDTLExits(detailModel.PackingList, MstSl)))
                                if (IsDTLExits(detailModel.ProductCode))
                                {

                                    //string query = "Update COMM_INVOICE_FINAL_PRODUCT_DTL SET PACKING_LIST_SLNO='" + detailModel.PackingList + "',PACKING_LIST_DATE=(TO_DATE('" + detailModel.PackingListDate + "','dd/mm/yyyy')),PROFORMA_INVOICE_NO='" + detailModel.ProformaInvoiceNo + "', " +
                                    //    "PROFORMA_INVOICE_DATE=(TO_DATE('" + detailModel.ProformaInvoiceDate + "','dd/mm/yyyy')),PO_NO='" + detailModel.PONo + "',PO_DATE=(TO_DATE('" + detailModel.PODate + "','dd/mm/yyyy')),SAP_SO_NO='" + detailModel.SapSoNo + "'," +
                                    //    "SAP_SO_DATE=(TO_DATE('" + detailModel.SapSoDate + "','dd/mm/yyyy')),DESTINATION_PORT='" + detailModel.DestinationPort + "',LOADING_PORT=" + detailModel.LoadingPort + ",MODE_OF_SHIPMENT='" + detailModel.ModeOfShipment + "' Where COMM_INVOICE_DTL_SLNO=" + detailModel.CommInvoDTLSlno + "";
                                    //if (dbHelper.CmdExecute(query) > 0)
                                    //{
                                    //    isTrue = true;
                                    //}

                                }


                                else
                                {
                                    Int64 DtlSL = idGenerated.getMAXSL("COMM_INVOICE_FINAL_PRODUCT_DTL", "COMM_INV_FINAL_PROD_SLNO");

                                    string qry1 = "INSERT INTO COMM_INVOICE_FINAL_PRODUCT_DTL (COMM_INV_FINAL_PROD_SLNO,COMMM_INV_FINAL_MST_SLNO,PRODUCT_CODE,PO_QTY_IN_PACK,PO_QTY_IN_PCS,INVOICE_QTY_IN_PACK,INVOICE_QTY_IN_PCS,PRICE_PER_PACK,CURRENCY,TOTAL_AMOUNT,CONVERSION_RATE,TOTAL_AMOUNT_IN_BDT)" +

                            "VALUES(" + DtlSL + ", '" + MaxID + "','" + detailModel.ProductCode + "', '" + detailModel.OrderQtyInPack + "','" + detailModel.OrderQtyInPcs + "','" + detailModel.InvoiceQtyInPack + "','" + detailModel.InvoiceQtyInPcs + "','" + detailModel.PricePerPack + "','" + detailModel.Currency + "','" + detailModel.TotalAmount + "','" + detailModel.ConversionRate + "','" + detailModel.TotalAmountBDT + "')";

                                    dbHelper.CmdExecute(dbConn.SAConnStrReader(), qry1);
                                    //dbHelper.CmdExecute(dbConn.SAConnStrReader(), qry1);
                                    if (dbHelper.CmdExecute(qry1) > 0)
                                    {
                                        isTrue = true;
                                    }
                                }

                            }
                        }

                    }

                }

                else
                {
                    //I for Insert  
                    //mxSl = idGenerated.getMAXSL("DOCTOR_ID", "DOCTOR Where DOCTOR_ID not in (900000)");
                    master.CommInvFinalMstSl = idGenerated.getMAXSL("COMM_INVOICE_FINAL_MST", "COMMM_INV_FINAL_MST_SLNO");
                    MaxID = master.CommInvFinalMstSl.ToString();
                    //MaxID = idGenerated.getMAXID("COMM_INVOICE_MST", "COMM_INVOICE_NO", "fm000");
                    IUMode = "I";
                    //maxID = MaxID.ToString();
                    qry = "INSERT INTO COMM_INVOICE_FINAL_MST (COMMM_INV_FINAL_MST_SLNO,COMM_INVOICE_NO,COMM_INVOICE_DATE,BUYER_CODE,COUNTRY_CODE,EXP_NO,EXP_DATE,LC_NO,LC_DATE,HS_CODE,BANK_CODE,BRANCH_CODE,MODE_OF_SHIPMENT,DESTINATION_PORT,LOADING_PORT,NET_INVOICE_AMOUNT)" +

                    "VALUES(" + MaxID + ", '" + master.CommInvoiceNo + "', (TO_DATE('" + master.CommInvoiceDate + "','dd-MM-yyyy')), '" + master.BuyerCode + "','" + master.CountryCode + "','" + master.ExpNo + "',(TO_DATE('" + master.ExpDate + "','dd-MM-yyyy')),'" + master.LcNo + "',(TO_DATE('" + master.LcDate + "','dd-MM-yyyy')),'" + master.HsCode + "','" + master.BankCode + "','" + master.BranchCode + "','" + master.ModeOfShipment + "','" + master.DestinationPort + "', '" + master.Loadingport + "', '" + master.NetInvAmount + "')";


                    long abc = dbHelper.CmdExecute(qry);
                    if (abc > 0)
                    {
                        if (master.productList != null)
                        {



                            foreach (CommercialFinalInvoiceProductDetail qtyModel in master.productList)
                            {

                                Int64 DtlSL = idGenerated.getMAXSL("COMM_INVOICE_FINAL_PRODUCT_DTL", "COMM_INV_FINAL_PROD_SLNO");

                                string qry1 = "INSERT INTO COMM_INVOICE_FINAL_PRODUCT_DTL (COMM_INV_FINAL_PROD_SLNO,COMMM_INV_FINAL_MST_SLNO,PRODUCT_CODE,PO_QTY_IN_PACK,PO_QTY_IN_PCS,INVOICE_QTY_IN_PACK,INVOICE_QTY_IN_PCS,PRICE_PER_PACK,CURRENCY,TOTAL_AMOUNT,CONVERSION_RATE,TOTAL_AMOUNT_IN_BDT)" +

                        "VALUES(" + DtlSL + ", '" + MaxID + "','" + qtyModel.ProductCode + "', '" + qtyModel.OrderQtyInPack + "','" + qtyModel.OrderQtyInPcs + "','" + qtyModel.InvoiceQtyInPack + "','" + qtyModel.InvoiceQtyInPcs + "','" + qtyModel.PricePerPack + "','" + qtyModel.Currency + "','" + qtyModel.TotalAmount + "','" + qtyModel.ConversionRate + "','" + qtyModel.TotalAmountBDT + "')";

                                dbHelper.CmdExecute(dbConn.SAConnStrReader(), qry1);

                            }
                        }
                      
                    }



                }

                return true;

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }
    }
}