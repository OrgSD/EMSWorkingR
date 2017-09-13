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
    public class TransportModeDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<TransportModeBEL> GetTransportModeList()
        {
            string Qry = "SELECT TRANSPORT_MODE_CODE,TRANSPORT_MODE_NAME from TRANSPORT_MODE_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<TransportModeBEL> item;

            item = (from DataRow row in dt.Rows
                    select new TransportModeBEL
                    {
                        TransportModeCode = row["TRANSPORT_MODE_CODE"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        //SHORT_NAME = row["SHORT_NAME"].ToString(),
                        //Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public bool SaveUpdate(TransportModeBEL master)
        {
            try
            {
                string Qry = "";
                if (master.TransportModeCode == null || master.TransportModeCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("TRANSPORT_MODE_INFO", "TRANSPORT_MODE_CODE", "fm000");
                    IUMode = "I";
                    Qry = "INSERT INTO TRANSPORT_MODE_INFO (TRANSPORT_MODE_CODE,TRANSPORT_MODE_NAME) VALUES('" + MaxID + "', '" + master.TransportModeName + "')";
                }
                else
                {//U for Insert
                    MaxID = master.TransportModeCode;
                    IUMode = "U";
                    Qry = "UPDATE TRANSPORT_MODE_INFO SET TRANSPORT_MODE_NAME = '" + master.TransportModeName + "' WHERE TRANSPORT_MODE_CODE = '" + master.TransportModeCode + "'";
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