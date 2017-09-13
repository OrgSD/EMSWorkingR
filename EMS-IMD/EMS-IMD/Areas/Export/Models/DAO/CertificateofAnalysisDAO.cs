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
    public class CertificateofAnalysisDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<COADetailsBEL> GetCOAList()
        {
            string Qry = "SELECT PACKING_LIST_SLNO,BATCH_NO, DOC_REF, REMARKS, COA_SLNO FROM COA_DETAILS";

                    DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
                    List<COADetailsBEL> item;

                    item = (from DataRow row in dt.Rows
                            select new COADetailsBEL
                            {
                                PackingListSlNo = row["PACKING_LIST_SLNO"].ToString(),
                                BatchNo = row["BATCH_NO"].ToString(),
                                Remarks = row["REMARKS"].ToString(),
                                DocRef = row["DOC_REF"].ToString().Substring(1, row["DOC_REF"].ToString().Length - 1),
                                COASlNo = row["COA_SLNO"].ToString(),
                            }).ToList();
                    return item;
        }
        

        public List<COADetailsBEL> GetPendingBatchNo(string PackingListSlNo)
        {
            string Qry = "SELECT PACKING_LIST_SLNO,BATCH_NO FROM VW_PACKING_LIST_PENDING_BATCH " +
                        "WHERE PACKING_LIST_SLNO = " + PackingListSlNo + "";

            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<COADetailsBEL> item;

            item = (from DataRow row in dt.Rows
                    select new COADetailsBEL
                    {
                        PackingListSlNo = row["PACKING_LIST_SLNO"].ToString(),
                        BatchNo = row["BATCH_NO"].ToString()
                    }).ToList();
            return item;
        }

        public bool SaveUpdate(COADetailsBEL master)
        {
            try
            {
                string Qry = "";

                if (String.IsNullOrEmpty(master.COASlNo))
                {
                    MaxID = idGenerated.getMAXSL("COA_DETAILS", "COA_SLNO").ToString();
                    IUMode = "I";
                    Qry = "Insert into COA_DETAILS(COA_SLNO, PACKING_LIST_SLNO, BATCH_NO, DOC_REF, REMARKS ) " +
                          "Values(" + MaxID + ", " + master.PackingListSlNo + ", '" + master.BatchNo + "', '" + master.DocRef + "','" + master.Remarks + "')";
                }
                else
                {//U for Insert
                    MaxID = master.COASlNo;
                    IUMode = "U";
                    Qry = "Update COA_DETAILS set DOC_REF='" + master.DocRef + "', REMARKS='" + master.Remarks + "' Where BUYER_CODE='" + MaxID + "'";
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