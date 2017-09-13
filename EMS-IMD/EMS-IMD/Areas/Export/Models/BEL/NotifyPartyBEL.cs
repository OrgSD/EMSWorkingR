using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class NotifyPartyBEL
    {

        public string NotifyPartyCode { get; set; }
        public string NotifyPartyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string EmailID { get; set; }
        public string FaxNo { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Status { get; set; }
    }
}