using EMS_IMD.Areas.Export.Models.BEL;
using EMS_IMD.DAL.Gateway;
using EMS_IMD.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.DAO
{
    public class CommercialInvoiceCreationDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();
        public string CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
        bool isTrue = false;


       public List<CommercialInvoicePackingList> GetPackingList(string BuyerCode)
       {
           string Qry = "SELECT * from VW_BUYER_WISE_PACKING_LIST Where BUYER_CODE = '" + BuyerCode + "' order by PACKING_LIST_SLNO ";
           DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
           List<CommercialInvoicePackingList> item;

           item = (from DataRow row in dt.Rows
                   select new CommercialInvoicePackingList
                   {

                       PackingList = row["PACKING_LIST_SLNO"].ToString(),
                       PackingListDate = row["PACKING_LIST_DATE"].ToString(),
                       ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                       //ProformaInvoiceDate = Convert.ToDateTime(row["PROFORMA_INVOICE_DATE"]).ToString("dd/MM/yyyy"),
                       ProformaInvoiceDate = row["PROFORMA_INVOICE_DATE"].ToString(),
                       PONo = row["PO_NO"].ToString(),
                      // PODate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                       PODate = row["PO_DATE"].ToString(),
                 
                       SapSoNo = row["SAP_SO_NO"].ToString(),
                       SapSoDate = row["SAP_SO_DATE"].ToString(),
                       ////SapSoDate = Convert.ToDateTime(row["SAP_SO_DATE"]).ToString("dd/MM/yyyy"),
                       DestinationPort = row["DESTINATION_PORT"].ToString(),
                       LoadingPort = row["LOADING_PORT"].ToString(),
                       ModeOfShipment = row["MODE_OF_SHIPMENT"].ToString(),
                       //Status = row["STATUS"].ToString()

                   }).ToList();

           return item;
       }

        public List<CommercialInvoicePackingList> GetPackingSearch()
        {
            //string Qry = "SELECT * from VW_BUYER_WISE_PACKING_LIST";



            //string Qry = "Select distinct COMM_INVOICE_DTL.* from COMM_INVOICE_DTL Inner Join COMM_INVOICE_MST on COMM_INVOICE_DTL.COMMM_INVOICE_MST_SLNO = COMM_INVOICE_MST.COMMM_INVOICE_MST_SLNO";
            string Qry = "Select distinct C.COMM_INVOICE_DTL_SLNO,C.COMMM_INVOICE_MST_SLNO,C.PACKING_LIST_SLNO,TO_CHAR (C.PACKING_LIST_DATE, 'dd-MM-yyyy') PACKING_LIST_DATE,C.PROFORMA_INVOICE_NO,TO_CHAR (C.PROFORMA_INVOICE_DATE, 'dd-MM-yyyy') PROFORMA_INVOICE_DATE,C.PO_NO,TO_CHAR (C.PO_DATE, 'dd-MM-yyyy') PO_DATE,C.SAP_SO_NO,TO_CHAR (C.SAP_SO_DATE, 'dd-MM-yyyy') SAP_SO_DATE,C.DESTINATION_PORT,C.LOADING_PORT,C.MODE_OF_SHIPMENT from COMM_INVOICE_DTL C Inner Join COMM_INVOICE_MST on C.COMMM_INVOICE_MST_SLNO = COMM_INVOICE_MST.COMMM_INVOICE_MST_SLNO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<CommercialInvoicePackingList> item;

            item = (from DataRow row in dt.Rows
                    select new CommercialInvoicePackingList
                    {

                        //BuyerCode = row["BUYER_CODE"].ToString(),
                        //BuyerName = row["BuyerName"].ToString(),
                        CommInvoDTLSlno = Convert.ToInt64(row["COMM_INVOICE_DTL_SLNO"].ToString()),
                        //CommInvoMSTSlno = Convert.ToInt64(row["CommInvoMSTSlno"].ToString()),
                        //PackingListNo = Convert.ToInt32(row["PACKING_LIST_SLNO"].ToString()),
                        //PackingListNo =Convert.ToInt32(row["PACKING_LIST_SLNO"].ToString()),
                        PackingList = row["PACKING_LIST_SLNO"].ToString(),
                        PackingListDate = row["PACKING_LIST_DATE"].ToString(),
                        ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
                        ProformaInvoiceDate = row["PROFORMA_INVOICE_DATE"].ToString(),
                        PONo = row["PO_NO"].ToString(),
                        PODate = row["PO_DATE"].ToString(),
                        ////PODate = Convert.ToDateTime(row["PO_DATE"]).ToString("dd/MM/yyyy"),
                        SapSoNo = row["SAP_SO_NO"].ToString(),
                        SapSoDate =row["SAP_SO_DATE"].ToString(),
                        //SapSoDate = Convert.ToDateTime(row["SAP_SO_DATE"]).ToString("dd/MM/yyyy"),
                        DestinationPort = row["DESTINATION_PORT"].ToString(),
                        LoadingPort = row["LOADING_PORT"].ToString(),
                        ModeOfShipment = row["MODE_OF_SHIPMENT"].ToString(),
                        //Status = row["STATUS"].ToString()

                    }).ToList();

            return item;
        }

        


        //public CommercialInvoiceProductDetail GetProductList(List<CommercialInvoiceProductDetail> data)
        //{
        //    string Qry = "Select * from VW_PACKING_LIST_PRODUCT where PACKING_LIST_SLNO = '" + data + "'";
        //    OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
        //    con.Open();
        //    OracleCommand cmd = new OracleCommand(Qry, con);
        //    OracleDataReader reader = cmd.ExecuteReader();
        //    CommercialInvoiceProductDetail modelData = new CommercialInvoiceProductDetail();
        //    while (reader.Read())
        //    {
        //        modelData.PackingListNo = reader.GetInt32(0);
        //        modelData.PackingListDate = reader["PACKING_LIST_DATE"].ToString();
        //        modelData.ProductCode = reader["PRODUCT_CODE"].ToString();
        //        modelData.InvoiceQtyInPack = reader.GetDecimal(3);
        //        modelData.InvoiceQtyInPcs = reader.GetDecimal(4);
        //        modelData.PackingQtyInPack = reader.GetDecimal(5);
        //        modelData.PackingQtyInPcs = reader.GetDecimal(6);
                       
        //    }
        //    con.Close();
        //    return modelData;
        //}



        public List<BuyerInfoBEL> GetBuyerList(string BuyerList)
        {
            string Qry = "SELECT * from BUYER_INFO Where BUYER_CODE in (" + BuyerList + ")";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<BuyerInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new BuyerInfoBEL
                    {
                        BuyerCode = row["BUYER_CODE"].ToString(),
                        BuyerName = row["BUYER_NAME"].ToString()
                       
                        //Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }



        //public List<CommercialInvoiceSearch> GetSearchData()
        //{
        //    //string Qry = "SELECT * from BUYER_INFO";
        //    string Qry = "SELECT c.COMMM_INVOICE_MST_SLNO,d.COMM_INVOICE_DTL_SLNO,c.COMM_INVOICE_NO,c.COMM_INVOICE_DATE,C.BUYER_CODE,F.BUYER_NAME,C.NET_INVOICE_AMOUNT," +
        //                " D.PACKING_LIST_SLNO,D.PACKING_LIST_DATE,D.PO_NO,D.PO_DATE,D.PROFORMA_INVOICE_DATE," +
        //               " D.PROFORMA_INVOICE_NO,D.SAP_SO_NO,D.SAP_SO_DATE,D.DESTINATION_PORT,D.LOADING_PORT,D.MODE_OF_SHIPMENT," +
        //               " E.COMM_INVOICE_PROD_SLNO,E.PO_QTY_IN_PACK,E.PO_QTY_IN_PCS,E.INVOICE_QTY_IN_PACK,E.INVOICE_QTY_IN_PCS," +
        //               " E.PRICE_PER_PACK,E.TOTAL_AMOUNT,E.CURRENCY FROM COMM_INVOICE_MST c " +
        //               " LEFT JOIN COMM_INVOICE_DTL d ON c.COMMM_INVOICE_MST_SLNO = d.COMMM_INVOICE_MST_SLNO " +
        //               " LEFT JOIN BUYER_INFO f ON f.BUYER_CODE = C.BUYER_CODE " +
        //               " left join COMM_INVOICE_PRODUCT_DTL e on c.COMMM_INVOICE_MST_SLNO = e.COMMM_INVOICE_MST_SLNO";
        //    DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
        //    List<CommercialInvoiceSearch> item;

        //    item = (from DataRow row in dt.Rows
        //            select new CommercialInvoiceSearch
        //            {
        //                BuyerCode = row["BUYER_CODE"].ToString(),
        //                BuyerName = row["BUYER_NAME"].ToString(),
        //                CommInvoMSTSlno = Convert.ToInt64(row["COMMM_INVOICE_MST_SLNO"].ToString()),
        //                CommInvoDTLSlno = Convert.ToInt64(row["COMM_INVOICE_DTL_SLNO"].ToString()),
        //                CommInvoiceNo = row["COMM_INVOICE_NO"].ToString(),
        //                CommInvoiceDate = Convert.ToDateTime(row["COMM_INVOICE_DATE"]).ToString(),
        //                NetInvoiceAmount = Convert.ToInt32(row["NET_INVOICE_AMOUNT"].ToString()),
        //                ProformaInvoiceNo = row["PROFORMA_INVOICE_NO"].ToString(),
        //                ProformaInvoiceDate = Convert.ToDateTime(row["PROFORMA_INVOICE_DATE"]).ToString(),
        //                PackingList = row["PACKING_LIST_SLNO"].ToString(),
        //                PackingListDate = row["PACKING_LIST_DATE"].ToString(),
        //                PONo = row["PO_NO"].ToString(),
        //                PODate = row["PO_DATE"].ToString(),
        //                SapSoNo = row["SAP_SO_NO"].ToString(),
        //                SapSoDate = row["SAP_SO_DATE"].ToString(),
        //                DestinationPort = row["DESTINATION_PORT"].ToString(),
        //                LoadingPort = row["LOADING_PORT"].ToString(),
        //                ModeOfShipment = row["MODE_OF_SHIPMENT"].ToString(),
        //                CommInvoiceProdSlno = Convert.ToInt64(row["COMM_INVOICE_PROD_SLNO"].ToString()),
        //                OrderQtyInPack = Convert.ToInt32(row["PO_QTY_IN_PACK"].ToString()),
        //                OrderQtyInPcs = Convert.ToInt32(row["PO_QTY_IN_PCS"].ToString()),
                        
        //                InvoiceQtyInPack = Convert.ToInt32(row["INVOICE_QTY_IN_PACK"].ToString()),
        //                InvoiceQtyInPcs = Convert.ToInt32(row["INVOICE_QTY_IN_PCS"].ToString()),
        //                //PricePerPack = Convert.ToDecimal(row["PRICE_PER_PACK"].ToString()),
        //                //TotalAmount = Convert.ToDecimal(row["TOTAL_AMOUNT"].ToString()),
        //                //Currency = row["CURRENCY"].ToString()

        //                //Status = row["STATUS"].ToString()

        //            }).ToList();
        //    return item;
        //}





        //private int CheckRelative(string CommInvoiceNo, long CommInvoiceProdSlno)
        //{
        //    using (OracleConnection con = new OracleConnection(dbConn.SAConnStrReader()))
        //    {
        //        string query = "Select Count(*) from COMM_INVOICE_PRODUCT_DTL Where COMM_INVOICE_PROD_SLNO = " + CommInvoiceProdSlno + "";
        //        OracleCommand cmd = new OracleCommand(query, con);
        //        con.Open();

        //        object o = cmd.ExecuteScalar();
        //        int total = 0;
        //        if (o != null)
        //        {
        //            total = Convert.ToInt32(o.ToString());
        //        }
        //        return total;
        //    }
        //}





        public bool SaveUpdate(CommercialInvoiceCreationBEL master, string userID)
        {
            try
            {
                string qry = "";
                Int64? MstSl;
                Int64? PDtlSL;

                if (IsMSTExitsByBuyerCode(master.CommInvoiceNo))
                
                {
                    MstSl = GetMstSLNO(master.CommInvoiceNo);

                    qry = "Update COMM_INVOICE_MST Set COMM_INVOICE_DATE =(TO_DATE('" + master.CommInvoiceDate + "','dd/mm/yyyy')), BUYER_CODE = '" + master.BuyerCode + "', NET_INVOICE_AMOUNT = '" + master.NetInvoiceAmount + "',UPDATE_BY= '" + userID + "', UPDATE_DATE =(TO_DATE('" + CurrentDate + "','dd-MM-yyyy')), UPDATE_TERMINAL = '" + idGenerated.GetLanIPAddress() + "'  Where COMMM_INVOICE_MST_SLNO = '" + MstSl + "'";

                    if (dbHelper.CmdExecute(qry) > 0)
                    {
                        if (master.packingList != null)
                        {
                            foreach (CommercialInvoicePackingList detailModel in master.packingList)
                            {
                                //if ((IsMSTExits(MstSL)) && (IsDTLExits(PackingList, MstSL)))
                                //if ((IsMSTExits(MstSl)) && (IsDTLExits(detailModel.PackingList, MstSl)))
                                if  (IsDTLExits(detailModel.PackingList))
                                {

                                    string query = "Update COMM_INVOICE_DTL SET PACKING_LIST_SLNO='" + detailModel.PackingList + "',PACKING_LIST_DATE=(TO_DATE('" + detailModel.PackingListDate + "','dd/mm/yyyy')),PROFORMA_INVOICE_NO='" + detailModel.ProformaInvoiceNo + "', " +
                                        "PROFORMA_INVOICE_DATE=(TO_DATE('" + detailModel.ProformaInvoiceDate + "','dd/mm/yyyy')),PO_NO='" + detailModel.PONo + "',PO_DATE=(TO_DATE('" + detailModel.PODate + "','dd/mm/yyyy')),SAP_SO_NO='" + detailModel.SapSoNo + "'," +
                                        "SAP_SO_DATE=(TO_DATE('" + detailModel.SapSoDate + "','dd/mm/yyyy')),DESTINATION_PORT='" + detailModel.DestinationPort + "',LOADING_PORT=" + detailModel.LoadingPort + ",MODE_OF_SHIPMENT='" + detailModel.ModeOfShipment + "' Where COMM_INVOICE_DTL_SLNO=" + detailModel.CommInvoDTLSlno + "";
                                    if (dbHelper.CmdExecute(query) > 0)
                                    {
                                        isTrue = true;
                                    }
                                   
                                }


                                else
                                {
                                    detailModel.CommInvoDTLSlno = idGenerated.getMAXSL("COMM_INVOICE_DTL", "COMM_INVOICE_DTL_SLNO");
                                    string qry1 = "INSERT INTO COMM_INVOICE_DTL (COMM_INVOICE_DTL_SLNO,COMMM_INVOICE_MST_SLNO,PACKING_LIST_SLNO,PACKING_LIST_DATE,PROFORMA_INVOICE_NO,PROFORMA_INVOICE_DATE,PO_NO,PO_DATE,SAP_SO_NO,SAP_SO_DATE,DESTINATION_PORT,LOADING_PORT,MODE_OF_SHIPMENT)" +

                                    "VALUES(" + detailModel.CommInvoDTLSlno + ", '" + MstSl + "', '" + detailModel.PackingList + "',(TO_DATE('" + detailModel.PackingListDate + "','dd/mm/yyyy')),'" + detailModel.ProformaInvoiceNo + "',(TO_DATE('" + detailModel.ProformaInvoiceDate + "','dd/mm/yyyy')),'" + detailModel.PONo + "',(TO_DATE('" + detailModel.PODate + "','dd/mm/yyyy')),'" + detailModel.SapSoNo + "',(TO_DATE('" + detailModel.SapSoDate + "','dd/mm/yyyy')),'" + detailModel.DestinationPort + "','" + detailModel.LoadingPort + "','" + detailModel.ModeOfShipment + "')";

                                    //dbHelper.CmdExecute(dbConn.SAConnStrReader(), qry1);
                                    if (dbHelper.CmdExecute(qry1) > 0)
                                    {
                                        isTrue = true;
                                    }
                                }
                               
                            }
                        }

                        if (master.productList != null)
                        {
                            foreach (CommercialInvoiceProductDetail detailModel in master.productList)
                            {
                                PDtlSL = GetProdSLNO(detailModel.ProductCode);
                                //if ((IsMSTProExits(MstSL)) && (IsDTLProExits(master.packingList, MstSL)))
                                if (IsDTLProExits(detailModel.ProductCode))
                                {
                                    string query = "Update COMM_INVOICE_PRODUCT_DTL SET COMMM_INVOICE_MST_SLNO=" + MstSl + ",PRODUCT_CODE='" + detailModel.ProductCode + "',PO_QTY_IN_PACK='" + detailModel.OrderQtyInPack + "', " +
                                     "PO_QTY_IN_PCS='" + detailModel.OrderQtyInPcs + "',INVOICE_QTY_IN_PACK='" + detailModel.InvoiceQtyInPack + "',INVOICE_QTY_IN_PCS='" + detailModel.InvoiceQtyInPcs + "',PRICE_PER_PACK='" + detailModel.PricePerPack + "'," +
                                     "CURRENCY='" + detailModel.Currency + "',TOTAL_AMOUNT='" + detailModel.TotalAmount + "',CONVERSION_RATE=" + detailModel.ConversionRate + " where COMM_INVOICE_PROD_SLNO='" + PDtlSL + "'";

                                    if (dbHelper.CmdExecute(query) > 0)
                                    {
                                        isTrue = true;
                                    }
                                }
                                else
                                {
                                    PDtlSL = idGenerated.getMAXSL("COMM_INVOICE_PRODUCT_DTL", "COMM_INVOICE_PROD_SLNO");
                                    string query = " Insert into COMM_INVOICE_PRODUCT_DTL(COMM_INVOICE_PROD_SLNO,COMMM_INVOICE_MST_SLNO,PRODUCT_CODE,PO_QTY_IN_PACK,PO_QTY_IN_PCS,PRICE_PER_PACK,CURRENCY,TOTAL_AMOUNT,CONVERSION_RATE,TOTAL_AMOUNT_IN_BDT)" +
                                    " Values('" + PDtlSL + "'," + MstSl + ",'" + detailModel.ProductCode + "','" + detailModel.OrderQtyInPack + "','" + detailModel.OrderQtyInPcs + "','" + detailModel.PricePerPack + "','" + detailModel.Currency + "','" + detailModel.TotalAmount + "','" + detailModel.ConversionRate + "','" + detailModel.TotalAmountBDT + "')";
                                    //dbHelper.CmdExecute(dbConn.SAConnStrReader(), query);
                                    if (dbHelper.CmdExecute(query) > 0)
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
                    master.CommInvoMSTSlno = idGenerated.getMAXSL("COMM_INVOICE_MST", "COMMM_INVOICE_MST_SLNO");
                    MaxID = idGenerated.getMAXID("COMM_INVOICE_MST", "COMM_INVOICE_NO", "fm000");
                    IUMode = "I";
                    //maxID = MaxID.ToString();
                    qry = "INSERT INTO COMM_INVOICE_MST (COMMM_INVOICE_MST_SLNO,COMM_INVOICE_NO,COMM_INVOICE_DATE,BUYER_CODE, STATUS,NET_INVOICE_AMOUNT,NET_INVOICE_AMOUNT_IN_BDT,ENTERED_BY,ENTERED_DATE,ENTERED_TERMINAL)" +

                    "VALUES(" + master.CommInvoMSTSlno + ", '" + MaxID + "', (TO_DATE('" + master.CommInvoiceDate + "','dd-MM-yyyy')), '" + master.BuyerCode + "','" + master.Status + "', '" + master.TotalAmtPack + "','" + master.TotalAmtBDT + "','" + userID + "',(TO_DATE('" + CurrentDate + "','dd-MM-yyyy')), '" + idGenerated.GetLanIPAddress() + "' )";

                  
                    long abc = dbHelper.CmdExecute(qry);
                    if (abc > 0)
                    {
                        if (master.packingList != null)
                        {
                          
                            foreach (CommercialInvoicePackingList qtyModel in master.packingList)
                            {

                                Int64 DtlSL = idGenerated.getMAXSL("COMM_INVOICE_DTL", "COMM_INVOICE_DTL_SLNO");
                                
                                string qry1 = "INSERT INTO COMM_INVOICE_DTL (COMM_INVOICE_DTL_SLNO,COMMM_INVOICE_MST_SLNO,PACKING_LIST_SLNO,PACKING_LIST_DATE,PROFORMA_INVOICE_NO,PROFORMA_INVOICE_DATE,PO_NO,PO_DATE,SAP_SO_NO,SAP_SO_DATE,DESTINATION_PORT,LOADING_PORT,MODE_OF_SHIPMENT)" +

                        "VALUES(" + DtlSL + ", '" + master.CommInvoMSTSlno + "', '" + qtyModel.PackingList + "',(TO_DATE('" + qtyModel.PackingListDate + "','dd/mm/yyyy')),'" + qtyModel.ProformaInvoiceNo + "',(TO_DATE('" + qtyModel.ProformaInvoiceDate + "','dd/mm/yyyy')),'" + qtyModel.PONo + "',(TO_DATE('" + qtyModel.PODate + "','dd/mm/yyyy')),'" + qtyModel.SapSoNo + "',(TO_DATE('" + qtyModel.SapSoDate + "','dd/mm/yyyy')),'" + qtyModel.DestinationPort + "','" + qtyModel.LoadingPort + "','" + qtyModel.ModeOfShipment + "')";
                               
                                dbHelper.CmdExecute(dbConn.SAConnStrReader(), qry1);

                            }
                        }
                        if (master.productList != null)
                        {
                            foreach (CommercialInvoiceProductDetail detail in master.productList)
                            {
                                PDtlSL = idGenerated.getMAXSL("COMM_INVOICE_PRODUCT_DTL", "COMM_INVOICE_PROD_SLNO");
                                string query = " Insert into COMM_INVOICE_PRODUCT_DTL(COMM_INVOICE_PROD_SLNO,COMMM_INVOICE_MST_SLNO,PRODUCT_CODE,PO_QTY_IN_PACK,PO_QTY_IN_PCS,PRICE_PER_PACK,CURRENCY,TOTAL_AMOUNT,CONVERSION_RATE,TOTAL_AMOUNT_IN_BDT)" +
                                " Values('" + PDtlSL + "','" + master.CommInvoMSTSlno + "','" + detail.ProductCode + "','" + detail.OrderQtyInPack + "','" + detail.OrderQtyInPcs + "','" + detail.PricePerPack + "','" + detail.Currency + "','" + detail.TotalAmount + "','" + detail.ConversionRate + "','" + detail.TotalAmountBDT + "')";
                                dbHelper.CmdExecute(dbConn.SAConnStrReader(), query);
                                

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

        private bool IsDTLProExits(string ProductCode)
        {
            bool isTrue = false;
            string Qry = "SELECT COMM_INVOICE_PROD_SLNO FROM COMM_INVOICE_PRODUCT_DTL WHERE PRODUCT_CODE = '" + ProductCode + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }


        private bool IsDTLExits(string PackingList)
        {
            bool isTrue = false;
            string Qry = "SELECT COMM_INVOICE_DTL_SLNO FROM COMM_INVOICE_DTL WHERE PACKING_LIST_SLNO = '" + PackingList + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(),Qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }

        private bool IsMSTExits(Int64? MstSL)
        {
            bool isTrue = false;
            string Qry = "SELECT * FROM COMM_INVOICE_MST WHERE COMMM_INVOICE_MST_SLNO = " + MstSL + "";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(),Qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }

        //private int CheckRelative(string p1, long p2)
        //{
        //    throw new NotImplementedException();
        //}




        public Int64 GetProdSLNO(string ProductCode)
        {
            Int64 SL = 0;
            string Qry = "Select COMM_INVOICE_PROD_SLNO from COMM_INVOICE_PRODUCT_DTL Where PRODUCT_CODE='" + ProductCode + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(),Qry);
            if (dt.Rows.Count > 0)
            {
                SL = Convert.ToInt64(dt.Rows[0][0].ToString());
            }

            return SL;
        }

        public Int64 GetMstSLNO(string CommInvoiceNo)
        {
            Int64 SL = 0;
            string Qry = "Select COMMM_INVOICE_MST_SLNO from COMM_INVOICE_MST Where COMM_INVOICE_NO='" + CommInvoiceNo + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(),Qry);
            if (dt.Rows.Count > 0)
            {
                SL = Convert.ToInt64(dt.Rows[0][0].ToString());
            }

            return SL;
        }

        public Int64 GetInvSLNO(string BuyerCode)
        {
            Int64 InvSLNO = 0;
            string Qry = "Select COMMM_INVOICE_MST_SLNO from COMM_INVOICE_MST Where COMM_INVOICE_NO='" + BuyerCode + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            if (dt.Rows.Count > 0)
            {
                InvSLNO = Convert.ToInt64(dt.Rows[0][0].ToString());
            }

            return InvSLNO;
        }


        private bool IsMSTExitsByBuyerCode(string CommInvoiceNo)
        {
            bool isTrue = false;
            string Qry = "SELECT COMMM_INVOICE_MST_SLNO FROM COMM_INVOICE_MST WHERE COMM_INVOICE_NO = '" + CommInvoiceNo + "'";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(),Qry);
            if (dt.Rows.Count > 0)
            {
                isTrue = true;
            }

            return isTrue;
        }




        //public List<CommercialInvoiceProductDetail> GetProductList(string PackNo)
        //{

        //    string Str = PackNo.Replace("[", "").Replace("]", "").Replace("\"", "");
            
            
            
        //    //String[] SubStr = Str.Split(',');
        //    //string FullStr = "";
        //    //for (int i = 0; i < SubStr.Length; i++)
        //    //{
        //    //    FullStr=FullStr+SubStr[i].ToString();
        //    //}

        //    string Qry = "Select * from VW_PACKING_LIST_PRODUCT where PACKING_LIST_SLNO in(" + Str + ")";
        //    DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
        //    List<CommercialInvoiceProductDetail> item;

        //    item = (from DataRow row in dt.Rows
        //            select new CommercialInvoiceProductDetail
        //            {
        //                PackingListNo = Convert.ToInt32(row["PACKING_LIST_SLNO"].ToString()),
        //                //PackingListDate = Convert.ToDateTime(row["PACKING_LIST_DATE"]).ToString(("dd/MM/yyyy")),
        //                //ProductCode = row["PRODUCT_CODE"].ToString(),
        //                OrderQtyInPack = Convert.ToInt32(row["ORDER_QTY_IN_PACK"].ToString()),
        //                //OrderQtyInPcs = Convert.ToInt32(row["ORDER_QTY_PCS"].ToString()),
        //                ////InvoiceQtyInPack = Convert.ToDecimal(row["INVOICE_QTY_IN_PACK"].ToString()),
        //                ////InvoiceQtyInPcs = Convert.ToDecimal(row["INVOICE_QTY_IN_PCS"].ToString()),
        //                //PackingQtyInPack = Convert.ToInt32(row["PACKING_QTY_IN_PACK"].ToString()),
        //                //PackingQtyInPcs = Convert.ToInt32(row["PACKING_QTY_IN_PCS"].ToString()),

        //                //Status = row["STATUS"].ToString()

        //            }).ToList();
           

        //    return item;
        //}


        public List<CommercialInvoiceProductDetail> GetProductList(string PackNo)
        {
            string Str = PackNo.Replace("[", "").Replace("]", "").Replace("\"", "");
            //string Qry = "Select * from VW_PACKING_LIST_PRODUCT where PACKING_LIST_SLNO in(" + Str + ")";
            string Qry = "SELECT VW_PACKING_LIST_PRODUCT.PRODUCT_CODE, sum(VW_PACKING_LIST_PRODUCT.ORDER_QTY_IN_PACK) AS OrderQtyPak,sum(VW_PACKING_LIST_PRODUCT.ORDER_QTY_PCS) AS OrderQtyPcs,sum(VW_PACKING_LIST_PRODUCT.PACKING_QTY_IN_PACK) AS PackingQtyInPack,sum(VW_PACKING_LIST_PRODUCT.PACKING_QTY_IN_PCS) AS PackingQtyInPcs  FROM VW_PACKING_LIST_PRODUCT where VW_PACKING_LIST_PRODUCT.PACKING_LIST_SLNO in(" + Str + ") GROUP BY VW_PACKING_LIST_PRODUCT.PRODUCT_CODE";
            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialInvoiceProductDetail> getId = new List<CommercialInvoiceProductDetail>();
            while (reader.Read())
            {
                CommercialInvoiceProductDetail modelData = new CommercialInvoiceProductDetail();
                //modelData.PackingListNo = reader.GetInt32(0);
                //modelData.PackingListDate = reader.GetInt64(1);
                
                //modelData.PackingListDate = reader["PACKING_LIST_DATE"].ToString();
                modelData.ProductCode = reader["PRODUCT_CODE"].ToString();
                //modelData.OrderQtyInPack = reader.GetInt32(0);
                modelData.OrderQtyInPcs = reader.GetInt32(2);
                modelData.PackingQtyInPack = reader.GetInt32(3);
                modelData.PackingQtyInPcs = reader.GetInt32(4);
                //modelData.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                //modelData.INVOICE_NO = reader["INVOICE_NO"].ToString();
                ////modelData.COLLECTION_MODE = reader["COLLECTION_MODE"].ToString();
                if (!reader.IsDBNull(1))
                {
                    modelData.OrderQtyInPack = reader.GetInt32(1);
                }
                else
                {
                    modelData.OrderQtyInPack = 0;
                }
                //if (!reader.IsDBNull(12))
                //{
                //    modelData.TDS_AMT = reader.GetDecimal(12);
                //}
                //else
                //{
                //    modelData.TDS_AMT = 0;
                //}
                //if (!reader.IsDBNull(13))
                //{
                //    modelData.MEMO_COST = reader.GetDecimal(13);
                //}
                //else
                //{
                //    modelData.MEMO_COST = 0;
                //}
                getId.Add(modelData);
            }
            con.Close();
            return getId;
        }

        public List<CommercialInvoiceProductDetail> GetProductSearch()
        {
            //string Str = PackNo.Replace("[", "").Replace("]", "").Replace("\"", "");
            //string Qry = "Select * from VW_PACKING_LIST_PRODUCT where PACKING_LIST_SLNO in(" + Str + ")";
            //string Qry = "SELECT VW_PACKING_LIST_PRODUCT.PRODUCT_CODE, sum(VW_PACKING_LIST_PRODUCT.ORDER_QTY_IN_PACK) AS OrderQtyPak,sum(VW_PACKING_LIST_PRODUCT.ORDER_QTY_PCS) AS OrderQtyPcs,sum(VW_PACKING_LIST_PRODUCT.PACKING_QTY_IN_PACK) AS PackingQtyInPack,sum(VW_PACKING_LIST_PRODUCT.PACKING_QTY_IN_PCS) AS PackingQtyInPcs  FROM VW_PACKING_LIST_PRODUCT where VW_PACKING_LIST_PRODUCT.PACKING_LIST_SLNO in(" + Str + ") GROUP BY VW_PACKING_LIST_PRODUCT.PRODUCT_CODE";
            string Qry = "Select COMM_INVOICE_PRODUCT_DTL.* From COMM_INVOICE_PRODUCT_DTL";
            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialInvoiceProductDetail> getId = new List<CommercialInvoiceProductDetail>();
            while (reader.Read())
            {
                CommercialInvoiceProductDetail modelData = new CommercialInvoiceProductDetail();
                //modelData.PackingListNo = reader.GetInt32(0);
                //modelData.PackingListDate = reader.GetInt64(1);

                //modelData.PackingListDate = reader["PACKING_LIST_DATE"].ToString();
                modelData.CommInvoiceProdSlno = Convert.ToInt32(reader["COMM_INVOICE_PROD_SLNO"].ToString());
                //modelData.CommInvoMSTSlno = Convert.ToInt32(reader["COMMM_INVOICE_MST_SLNO"].ToString());
                modelData.ProductCode = reader["PRODUCT_CODE"].ToString();
                //modelData.OrderQtyInPack = reader.GetInt32(0);
                //modelData.OrderQtyInPcs = reader.GetInt32(2);
                modelData.OrderQtyInPcs = Convert.ToInt32(reader["PO_QTY_IN_PCS"].ToString());
                modelData.OrderQtyInPack = Convert.ToInt32(reader["PO_QTY_IN_PACK"].ToString());
                //modelData.PackingQtyInPack = reader.GetInt32(5);
                //modelData.PackingQtyInPcs = reader.GetInt32(6);
                //modelData.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                //modelData.INVOICE_NO = reader["INVOICE_NO"].ToString();
                ////modelData.COLLECTION_MODE = reader["COLLECTION_MODE"].ToString();
                if (!reader.IsDBNull(5))
                {
                    modelData.PackingQtyInPack = reader.GetInt32(5);
                }
                else
                {
                    modelData.PackingQtyInPack = 0;
                }
                if (!reader.IsDBNull(6))
                {
                    modelData.PackingQtyInPcs = reader.GetInt32(6);
                }
                else
                {
                    modelData.PackingQtyInPcs = 0;
                }
                if (!reader.IsDBNull(7))
                {
                    modelData.InvoiceQtyInPack = reader.GetInt32(7);
                }
                else
                {
                    modelData.InvoiceQtyInPack = 0;
                }
                if (!reader.IsDBNull(8))
                {
                    modelData.InvoiceQtyInPcs = reader.GetInt32(8);
                }
                else
                {
                    modelData.InvoiceQtyInPcs = 0;
                }
          
                getId.Add(modelData);
            }
            con.Close();
            return getId;
        }


        public List<CommercialInvoiceSearch> GetSearchData()
        {
            //string Str = PackNo.Replace("[", "").Replace("]", "").Replace("\"", "");
            //string Qry = "Select * from VW_PACKING_LIST_PRODUCT where PACKING_LIST_SLNO in(" + Str + ")";
            //string Qry = "SELECT VW_PACKING_LIST_PRODUCT.PRODUCT_CODE, sum(VW_PACKING_LIST_PRODUCT.ORDER_QTY_IN_PACK) AS OrderQtyPak,sum(VW_PACKING_LIST_PRODUCT.ORDER_QTY_PCS) AS OrderQtyPcs,sum(VW_PACKING_LIST_PRODUCT.PACKING_QTY_IN_PACK) AS PackingQtyInPack,sum(VW_PACKING_LIST_PRODUCT.PACKING_QTY_IN_PCS) AS PackingQtyInPcs  FROM VW_PACKING_LIST_PRODUCT where VW_PACKING_LIST_PRODUCT.PACKING_LIST_SLNO in(" + Str + ") GROUP BY VW_PACKING_LIST_PRODUCT.PRODUCT_CODE";
            //string Qry = "SELECT c.COMMM_INVOICE_MST_SLNO,d.COMM_INVOICE_DTL_SLNO,c.COMM_INVOICE_NO,c.COMM_INVOICE_DATE,C.BUYER_CODE,F.BUYER_NAME,C.NET_INVOICE_AMOUNT," +
            //            " D.PACKING_LIST_SLNO,D.PACKING_LIST_DATE,D.PO_NO,D.PO_DATE,D.PROFORMA_INVOICE_DATE," +
            //           " D.PROFORMA_INVOICE_NO,D.SAP_SO_NO,D.SAP_SO_DATE,D.DESTINATION_PORT,D.LOADING_PORT,D.MODE_OF_SHIPMENT," +
            //           " E.COMM_INVOICE_PROD_SLNO,E.PO_QTY_IN_PACK,E.PO_QTY_IN_PCS,E.INVOICE_QTY_IN_PACK,E.INVOICE_QTY_IN_PCS," +
            //           " E.PRICE_PER_PACK,E.TOTAL_AMOUNT,E.CURRENCY FROM COMM_INVOICE_MST c " +
            //           " LEFT JOIN COMM_INVOICE_DTL d ON c.COMMM_INVOICE_MST_SLNO = d.COMMM_INVOICE_MST_SLNO " +
                       //" LEFT JOIN BUYER_INFO f ON f.BUYER_CODE = C.BUYER_CODE "
            //           " left join COMM_INVOICE_PRODUCT_DTL e on c.COMMM_INVOICE_MST_SLNO = e.COMMM_INVOICE_MST_SLNO";
            string Qry = " Select * From COMM_INVOICE_MST LEFT JOIN BUYER_INFO f ON f.BUYER_CODE = COMM_INVOICE_MST.BUYER_CODE ";

            OracleConnection con = new OracleConnection(dbConn.SAConnStrReader());
            con.Open();
            OracleCommand cmd = new OracleCommand(Qry, con);
            OracleDataReader reader = cmd.ExecuteReader();
            List<CommercialInvoiceSearch> getId = new List<CommercialInvoiceSearch>();
            while (reader.Read())
            {
                CommercialInvoiceSearch modelData = new CommercialInvoiceSearch();
                modelData.BuyerCode = reader["BUYER_CODE"].ToString();
                       modelData.BuyerName = reader["BUYER_NAME"].ToString();
                        modelData.CommInvoMSTSlno = Convert.ToInt64(reader["COMMM_INVOICE_MST_SLNO"].ToString());
                        //modelData.CommInvoDTLSlno = Convert.ToInt64(reader["COMM_INVOICE_DTL_SLNO"].ToString());
                        modelData.CommInvoiceNo = reader["COMM_INVOICE_NO"].ToString();
                        modelData.Status = reader["STATUS"].ToString();
                        modelData.CommInvoiceDate = Convert.ToDateTime(reader["COMM_INVOICE_DATE"]).ToString("dd/MM/yyyy");
                        modelData.NetInvoiceAmount = Convert.ToInt32(reader["NET_INVOICE_AMOUNT"].ToString());
                        //modelData.ProformaInvoiceNo = reader["PROFORMA_INVOICE_NO"].ToString();
                        //modelData.ProformaInvoiceDate = Convert.ToDateTime(reader["PROFORMA_INVOICE_DATE"]).ToString();
                        //modelData.PackingList = reader["PACKING_LIST_SLNO"].ToString();
                        //modelData.PackingListDate = reader["PACKING_LIST_DATE"].ToString();
                        //modelData.PONo = reader["PO_NO"].ToString();
                        //modelData.PODate = reader["PO_DATE"].ToString();
                        //modelData.SapSoNo = reader["SAP_SO_NO"].ToString();
                        //modelData.SapSoDate = reader["SAP_SO_DATE"].ToString();
                        //modelData.DestinationPort = reader["DESTINATION_PORT"].ToString();
                        //modelData.LoadingPort = reader["LOADING_PORT"].ToString();
                        //modelData.ModeOfShipment = reader["MODE_OF_SHIPMENT"].ToString();
                        //modelData.CommInvoiceProdSlno = Convert.ToInt64(reader["COMM_INVOICE_PROD_SLNO"].ToString());
                        ////modelData.OrderQtyInPack = Convert.ToInt32(reader["PO_QTY_IN_PACK"].ToString());
                        //modelData.OrderQtyInPcs = Convert.ToInt32(reader["PO_QTY_IN_PCS"].ToString());
                        
                        ////modelData.InvoiceQtyInPack = Convert.ToInt32(reader["INVOICE_QTY_IN_PACK"].ToString());
                        ////modelData.InvoiceQtyInPcs = Convert.ToInt32(reader["INVOICE_QTY_IN_PCS"].ToString());
                        //modelData.PricePerPack = Convert.ToDecimal(reader["PRICE_PER_PACK"].ToString());
                        //modelData.TotalAmount = Convert.ToDecimal(reader["TOTAL_AMOUNT"].ToString());
                        //modelData.Currency = reader["CURRENCY"].ToString();


                //CommercialInvoiceProductDetail modelData = new CommercialInvoiceProductDetail();
                ////modelData.PackingListNo = reader.GetInt32(0);
                ////modelData.PackingListDate = reader.GetInt64(1);

                ////modelData.PackingListDate = reader["PACKING_LIST_DATE"].ToString();
                //modelData.ProductCode = reader["PRODUCT_CODE"].ToString();
                ////modelData.OrderQtyInPack = reader.GetInt32(0);
                //modelData.OrderQtyInPcs = reader.GetInt32(2);
                //modelData.PackingQtyInPack = reader.GetInt32(3);
                //modelData.PackingQtyInPcs = reader.GetInt32(4);
                //modelData.BRANCH_NAME = reader["BRANCH_NAME"].ToString();
                //modelData.INVOICE_NO = reader["INVOICE_NO"].ToString();
                ////modelData.COLLECTION_MODE = reader["COLLECTION_MODE"].ToString();
                //if (!reader.IsDBNull(19))
                //{
                //    modelData.OrderQtyInPack = reader.GetInt32(19);
                //}
                //else
                //{
                //    modelData.OrderQtyInPack = 0;
                //}

                //if (!reader.IsDBNull(21))
                //{
                //    modelData.InvoiceQtyInPack = reader.GetInt32(21);
                //}
                //else
                //{
                //    modelData.InvoiceQtyInPack = 0;
                //}
                //if (!reader.IsDBNull(22))
                //{
                //    modelData.InvoiceQtyInPcs = reader.GetInt32(22);
                //}
                //else
                //{
                //    modelData.InvoiceQtyInPcs = 0;
                //}
                //if (!reader.IsDBNull(12))
                //{
                //    modelData.TDS_AMT = reader.GetDecimal(12);
                //}
                //else
                //{
                //    modelData.TDS_AMT = 0;
                //}
                //if (!reader.IsDBNull(13))
                //{
                //    modelData.MEMO_COST = reader.GetDecimal(13);
                //}
                //else
                //{
                //    modelData.MEMO_COST = 0;
                //}
                getId.Add(modelData);
            }
            con.Close();
            return getId;
        }
     

    }
}