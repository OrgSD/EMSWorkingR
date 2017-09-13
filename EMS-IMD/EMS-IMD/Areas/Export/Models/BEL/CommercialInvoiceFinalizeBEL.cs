using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class CommercialInvoiceFinalizeBEL
    {
        public Int64 CommInvFinalMstSl { get; set; }
        public string CommInvoiceNo { get; set; }
        public string CommInvoiceDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string UserId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string ExpNo { get; set; }
        public string ExpDate { get; set; }
        public string LcNo { get; set; }
        public string LcDate { get; set; }
        public string HsCode { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string ModeOfShipment { get; set; }
        public string ModeOfShipmentName { get; set; }
        public string ConsigneeCode { get; set; }
        public string NotifyParty { get; set; }
        public string DestinationPort { get; set; }
        public string DestinationPortName { get; set; }
        public string Loadingport { get; set; }
        public string LoadingportName { get; set; }
        public string IncoTermCode { get; set; }
        public string NetInvAmount { get; set; }
        public string Status { get; set; }

        public virtual ICollection<CommercialFinalInvoiceProductDetail> productList { get; set; }

    }


    public class CommercialFinalInvoiceProductDetail
    {
        public Int64 CommInvoiceFinalProdSlno { get; set; }

        public Int64 CommInvFinalMstSl { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string CommInvoiceNo { get; set; }
        public string CommInvoiceDate { get; set; }
        public string PackSize { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackingList { get; set; }
        public string PackingListDate { get; set; }
        public string InvoiceQtyInPack { get; set; }
        public string InvoiceQtyInPcs { get; set; }
        public string PackingQtyInPack { get; set; }
        public string PackingQtyInPcs { get; set; }
        public decimal PricePerPack { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountBDT { get; set; }
        public decimal ConversionRate { get; set; }

        public string OrderQtyInPack { get; set; }
        public string OrderQtyInPcs { get; set; }

    }
}