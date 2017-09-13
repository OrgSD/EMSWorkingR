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
    public class DestinationPortDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
       
        IDGenerated idGenerated = new IDGenerated();

        public List<DestinationPortBEL> GetCountryList()
        {
            string Qry = "SELECT COUNTRY_CODE,COUNTRY_NAME from COUNTRY_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<DestinationPortBEL> item;

            item = (from DataRow row in dt.Rows
                    select new DestinationPortBEL
                    {
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        //SHORT_NAME = row["SHORT_NAME"].ToString(),
                        //Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }


        public List<DestinationPortBEL> GetTransportModeList()
        {
            string Qry = "SELECT TRANSPORT_MODE_CODE,TRANSPORT_MODE_NAME from TRANSPORT_MODE_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<DestinationPortBEL> item;

            item = (from DataRow row in dt.Rows
                    select new DestinationPortBEL
                    {
                        TransportModeCode = row["TRANSPORT_MODE_CODE"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        //SHORT_NAME = row["SHORT_NAME"].ToString(),
                        //Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public List<DestinationPortBEL> GetDestinationPortList()
        {
            string Qry = "SELECT d.DESTINATION_PORT_CODE,d.DESTINATION_PORT_NAME,d.COUNTRY_CODE,d.TRANSPORT_MODE_CODE,C.COUNTRY_NAME,T.TRANSPORT_MODE_NAME " +
                        " FROM DESTINATION_PORT_INFO d " +
                        " LEFT JOIN COUNTRY_INFO c ON d.COUNTRY_CODE = c.COUNTRY_CODE " +
                        " left join TRANSPORT_MODE_INFO t on D.TRANSPORT_MODE_CODE = T.TRANSPORT_MODE_CODE";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<DestinationPortBEL> item;

            item = (from DataRow row in dt.Rows
                    select new DestinationPortBEL
                    {
                        TransportModeCode = row["TRANSPORT_MODE_CODE"].ToString(),
                        TransportModeName = row["TRANSPORT_MODE_NAME"].ToString(),
                        CountryCode = row["COUNTRY_CODE"].ToString(),
                        CountryName = row["COUNTRY_NAME"].ToString(),
                        PortCode = row["DESTINATION_PORT_CODE"].ToString(),
                        PortName = row["DESTINATION_PORT_NAME"].ToString(),
                        
                        //SHORT_NAME = row["SHORT_NAME"].ToString(),
                        //Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public bool SaveUpdate(DestinationPortBEL master)
        {
            try
            {
                string Qry = "";
                if (master.PortCode == null || master.PortCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("DESTINATION_PORT_INFO", "DESTINATION_PORT_CODE", "fm000");
                    IUMode = "I";
                    Qry = "INSERT INTO DESTINATION_PORT_INFO (DESTINATION_PORT_CODE,DESTINATION_PORT_NAME,COUNTRY_CODE,TRANSPORT_MODE_CODE) VALUES('" + MaxID + "','" + master.PortName + "','" + master.CountryCode + "','" + master.TransportModeCode + "')";
                }
                else
                {//U for Insert
                    MaxID = master.PortCode;
                    IUMode = "U";
                    Qry = "UPDATE DESTINATION_PORT_INFO SET DESTINATION_PORT_NAME = '" + master.PortName + "', COUNTRY_CODE = '" + master.CountryCode + "' , TRANSPORT_MODE_CODE = '" + master.TransportModeCode + "' WHERE DESTINATION_PORT_CODE = '" + master.PortCode + "'";
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