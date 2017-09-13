using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class BuyerInfoBEL
    {
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string EmailID { get; set; }
        public string FaxNo { get; set; }
        public int NotificationDay { get; set; }
        public string TermsCondition { get; set; }
        public string TermsOfPayment { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
    }
}