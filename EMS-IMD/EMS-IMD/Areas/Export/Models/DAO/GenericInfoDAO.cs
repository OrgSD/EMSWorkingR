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
    public class GenericInfoDAO : ReturnData
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<GenericInfoBEL> GetGenericInfoList()
        {
            string Qry = "SELECT * from GENERIC_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<GenericInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new GenericInfoBEL
                    {
                        GenericCode = row["GENERIC_CODE"].ToString(),
                        GenericName = row["GENERIC_NAME"].ToString(),
                        //BankAddress = row["ADDRESS"].ToString(),
                        Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }

        public bool SaveUpdate(GenericInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.GenericCode == null || master.GenericCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("GENERIC_INFO", "GENERIC_CODE", "fm0000");
                    IUMode = "I";
                    Qry = "INSERT INTO GENERIC_INFO (GENERIC_CODE,GENERIC_NAME,STATUS) VALUES('" + MaxID + "', '" + master.GenericName + "' , '" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.GenericCode;
                    IUMode = "U";
                    Qry = "UPDATE GENERIC_INFO SET GENERIC_NAME = '" + master.GenericName + "',STATUS = '" + master.Status + "' WHERE GENERIC_CODE = '" + master.GenericCode + "'";
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