using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class ShipmentMstBEL
    {
        public string ShipmentMstSlNo {get; set;}
        public string ShipmentDate {get; set;}
        public string CommInvoiceNo {get; set;}
        public string CommInvoiceDate {get; set;}
        public string CountryCode {get; set;}
        public string CountryName {get; set;}
        public string BuyerCode {get; set;}
        public string BuyerName {get; set;}
        public string BlNo {get; set;}
        public string BlDate {get; set;}
        public string BlScanRef {get; set;}
        public string ExpNo {get; set;}
        public string ExpDate {get; set;}
        public string ExpScanRef {get; set;}
        public string PaymentAdviceScanRef {get; set;}
        public string InsuranceStatus {get; set;}
        public string Remarks {get; set;}

        public virtual ICollection<ShipmentDtlBEL> ShipmentDetailList { get; set; }

        public virtual ICollection<ShipmentDocumentBEL> ShipmentDocumentList { get; set; }

    }

    public class ShipmentDtlBEL
    {
        public string ShipmentDtlSlNo { get; set; }
        public string ShipmentMstSlNo { get; set; }
        public string PackingListSlNo { get; set; }
        public string PackingListDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProformaInvoiceDate { get; set; }
    }

    public class ShipmentDocumentBEL
    {
        public string ShipmentDocSlNo { get; set; }
        public string ShipmentMstSlNo { get; set; }
        public string DocumentName { get; set; }
        public string DocumentRemarks { get; set; }
        public string DocRef { get; set; }
    }

    public class CommercialInvoiceAndBuyerDetailsBEL
    {
        public string ShipmentMstSlNo { get; set; }
        public string ShipmentDate { get; set; }
        public string CommInvoiceFinalMstSlNo { get; set; }
        public string CommInvoiceNo { get; set; }
        public string CommInvoiceDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}