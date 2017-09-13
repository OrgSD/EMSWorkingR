using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class SalesCollectionBEL
    {
        public string  SalesCollSlNo {get; set;}
        public string  SalesCollDate {get; set;}
        public string  CommInvoiceNo {get; set;}
        public string  CommInvoiceDate {get; set;}
        public string  BuyerCode {get; set;}
        public string  BuyerName {get; set;}
        public string  CollAmount {get; set;}
        public string  BankCharge {get; set;}
        public string  FdbcNo {get; set;}
        public string  FdbcDate {get; set;}
        public string  BankCode {get; set;}
        public string  BankName {get; set;}

        public string RemainingAmt { get; set; }
        public string PrevRecv { get; set; }
        public string PrevBank { get; set; }
    }
}