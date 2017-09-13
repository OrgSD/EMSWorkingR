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
    public class OrderTypeInfoDAO
    {
        DBConnection dbConn = new DBConnection();
        DBHelper dbHelper = new DBHelper();
        IDGenerated idGenerated = new IDGenerated();

        public List<OrderTypeInfoBEL> GetOrderTypeList()
        {
            string Qry = "SELECT ORDER_TYPE_CODE, ORDER_TYPE_NAME, STATUS FROM ORDER_TYPE_INFO";
            DataTable dt = dbHelper.GetDataTable(dbConn.SAConnStrReader(), Qry);
            List<OrderTypeInfoBEL> item;

            item = (from DataRow row in dt.Rows
                    select new OrderTypeInfoBEL
                    {
                        OrderTypeCode = row["ORDER_TYPE_CODE"].ToString(),
                        OrderTypeName = row["ORDER_TYPE_NAME"].ToString(),
                       // Status = row["STATUS"].ToString(),
                    }).ToList();
            return item;
        }

       
    }
}