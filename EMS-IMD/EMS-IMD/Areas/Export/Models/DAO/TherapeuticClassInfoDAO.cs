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
    public class TherapeuticClassInfoDAO : ReturnData
    {

        DBConnection dbConn = new DBConnection();
       
        DBHelper dbHelper = new DBHelper();
      
        IDGenerated idGenerated = new IDGenerated();
       
        public List<TherapeuticClassInfoBEL> GetTherapeuticInfoList()
        {
            string Qry = "SELECT THERAPEUTIC_CLASS_CODE,THERAPEUTIC_CLASS_NAME,STATUS from THERAPEUTIC_CLASS_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<TherapeuticClassInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new TherapeuticClassInfoBEL
                    {
                        TherapeuticClassCode = row["THERAPEUTIC_CLASS_CODE"].ToString(),
                        TherapeuticClassName = row["THERAPEUTIC_CLASS_NAME"].ToString(),
                        //SHORT_NAME = row["SHORT_NAME"].ToString(),
                        Status = row["STATUS"].ToString()

                    }).ToList();
            return item;
        }




        //public ValidationMsg Save(TherapeuticClassInfoBEL model)
        //{
        //    _vmMsg = new ValidationMsg();

        //    try
        //    {
        //        //mxSl = idGenerated.getMAXSL("CURRENCY_CODE", "CURRENCY_INFO");
        //        MaxID = idGenerated.getMAXID("THERAPEUTIC_CLASS_CODE", "THERAPEUTIC_CLASS_INFO", "fm000");
        //        string qry = "INSERT INTO THERAPEUTIC_CLASS_INFO (THERAPEUTIC_CLASS_CODE,THERAPEUTIC_CLASS_NAME,STATUS) VALUES('" + model.TherapeuticClassCode + "', '" + model.TherapeuticClassName + "', '" + model.Status + "')";

        //        if (dbHelper.CmdExecute1(qry) > 0)
        //        {
        //            _vmMsg.Type = Enums.MessageType.Success;
        //            _vmMsg.Msg = "Saved Successfully.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _vmMsg.Type = Enums.MessageType.Error;
        //        _vmMsg.Msg = "Failed to save.";

        //        if (ex.Message.Contains("ORA-00001"))
        //        {
        //            _vmMsg.Type = Enums.MessageType.Error;
        //            _vmMsg.Msg = "Divison Code already Exist.";
        //        }
        //        if (ex.Message.Contains("UK_DIVISION_NAME"))
        //        {
        //            _vmMsg.Type = Enums.MessageType.Error;
        //            _vmMsg.Msg = "Divison Name already Exist.";
        //        }
        //        if (ex.Message.Contains("ORA-01438"))
        //        {
        //            _vmMsg.Type = Enums.MessageType.Error;
        //            _vmMsg.Msg = "Value larger than specified precision allowed.";
        //        }
        //    }
        //    return _vmMsg;
        //}

        //public ValidationMsg Update(TherapeuticClassInfoBEL model)
        //{
        //    _vmMsg = new ValidationMsg();
        //    try
        //    {
        //        string qry = "UPDATE THERAPEUTIC_CLASS_INFO SET THERAPEUTIC_CLASS_NAME = '" + model.THERAPEUTIC_CLASS_NAME + "', STATUS = '" + model.STATUS + "' WHERE THERAPEUTIC_CLASS_CODE = '" + model.THERAPEUTIC_CLASS_CODE + "'";
        //        if (dbHelper.CmdExecute1(qry) > 0)
        //        {
        //            _vmMsg.Type = Enums.MessageType.Update;
        //            _vmMsg.Msg = "Updated Successfully.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _vmMsg.Type = Enums.MessageType.Error;
        //        _vmMsg.Msg = "Failed to Update.";
        //        if (ex.Message.Contains("ORA-00001"))
        //        {
        //            _vmMsg.Type = Enums.MessageType.Error;
        //            _vmMsg.Msg = "Divison Code already Exist.";
        //        }
        //        if (ex.Message.Contains("UK_DIVISION_NAME"))
        //        {
        //            _vmMsg.Type = Enums.MessageType.Error;
        //            _vmMsg.Msg = "Divison Name already Exist.";
        //        }
        //        if (ex.Message.Contains("ORA-01438"))
        //        {
        //            _vmMsg.Type = Enums.MessageType.Error;
        //            _vmMsg.Msg = "Value larger than specified precision allowed.";
        //        }
        //    }
        //    return _vmMsg;
        //}



        public bool SaveUpdate(TherapeuticClassInfoBEL master)
        {
            try
            {
                string Qry = "";
                if (master.TherapeuticClassCode == null || master.TherapeuticClassCode == "")
                {//I for Insert  
                    MaxID = idGenerated.getMAXID("THERAPEUTIC_CLASS_INFO", "THERAPEUTIC_CLASS_CODE", "fm0000");
                    IUMode = "I";
                    Qry = "INSERT INTO THERAPEUTIC_CLASS_INFO (THERAPEUTIC_CLASS_CODE,THERAPEUTIC_CLASS_NAME,STATUS) VALUES('" + MaxID + "', '" + master.TherapeuticClassName + "','" + master.Status + "')";
                }
                else
                {//U for Insert
                    MaxID = master.TherapeuticClassCode;
                    IUMode = "U";
                    Qry = "UPDATE THERAPEUTIC_CLASS_INFO SET THERAPEUTIC_CLASS_NAME = '" + master.TherapeuticClassName + "', STATUS = '" + master.Status + "' WHERE THERAPEUTIC_CLASS_CODE = '" + master.TherapeuticClassCode + "'";
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