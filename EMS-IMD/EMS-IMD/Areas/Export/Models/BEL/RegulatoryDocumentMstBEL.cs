using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class RegulatoryDocumentMstBEL
    {
        public string RegDocMstSlNo { get; set; }
        public string ShipmentMstSlNo { get; set; }
        public string ShipmentDate { get; set; }
        public string CommInvoiceNo { get; set; }
        public string CommInvoiceDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }

        public virtual ICollection<RegulatoryDocumentDtlBEL> RegDocList { get; set; }
    }

    public class RegulatoryDocumentDtlBEL
    {
        public string RegDocDtlSlNo { get; set; }
        public string RegDocMstSlNo { get; set; }
        public string DocumentName { get; set; }
        public string Remarks { get; set; }
        public string DocRef { get; set; }
    }
}
