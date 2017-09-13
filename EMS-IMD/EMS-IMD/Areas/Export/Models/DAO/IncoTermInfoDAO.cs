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
    public class IncoTermInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<IncoTermInfoBEL> GetIncoTermInfoList()
        {
            string Qry = "SELECT INCOTERM_CODE,INCOTERM_NAME,STATUS from INCOTERM_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<IncoTermInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new IncoTermInfoBEL
                    {
                        IncoTermCode = row["INCOTERM_CODE"].ToString(),
                        IncoTermName = row["INCOTERM_NAME"].ToString(),
                        //SHORT_NAME = row["SHORT_NAME"].ToString(),
                        Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public bool SaveUpdate(IncoTermInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.IncoTermCode == null || master.IncoTermCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("INCOTERM_INFO", "INCOTERM_CODE", "fm000");
                    IUMode = "I";
                    Qry = "INSERT INTO INCOTERM_INFO (INCOTERM_CODE,INCOTERM_NAME,STATUS) VALUES('" + MaxID + "', '" + master.IncoTermName + "' , '" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.IncoTermCode;
                    IUMode = "U";
                    Qry = "UPDATE INCOTERM_INFO SET INCOTERM_NAME = '" + master.IncoTermName + "', STATUS = '" + master.Status + "' WHERE INCOTERM_CODE = '" + master.IncoTermCode + "'";
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