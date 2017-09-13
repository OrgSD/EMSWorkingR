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
    public class PriceTypeInfoDAO
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<PriceTypeInfoBEL> GetPriceTypeList()
        {
            string Qry = "SELECT PRICE_TYPE_CODE, PRICE_TYPE_NAME, STATUS FROM PRICE_TYPE_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PriceTypeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PriceTypeInfoBEL
                    {
                        PriceTypeCode = row["PRICE_TYPE_CODE"].ToString(),
                        PriceTypeName = row["PRICE_TYPE_NAME"].ToString(),
                        Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }


        public List<PriceTypeInfoBEL> GetPriceTypeComboList()
        {
            string Qry = "SELECT PRICE_TYPE_CODE, PRICE_TYPE_NAME, STATUS FROM PRICE_TYPE_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<PriceTypeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new PriceTypeInfoBEL
                    {
                        PriceTypeCode = row["PRICE_TYPE_CODE"].ToString(),
                        PriceTypeName = row["PRICE_TYPE_NAME"].ToString(),                     
                    }).ToList();
            return item;
        }
    }
}