using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class PurchaseOrderMstBEL
    {
        public string PoMstSlno { get; set; }
        public string PoNo { get; set; }
        public string PoDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public string IncoTermCode { get; set; }
        public string IncoTermName { get; set; }
        public string OrderTypeCode { get; set; }
        public string OrderTypeName { get; set; }
        public string PriceTypeCode { get; set; }
        public string PriceTypeName { get; set; }
        public string DestinationPortCode { get; set; }
        public string DestinationPortName { get; set; }
        public string LoadingPortCode { get; set; }
        public string LoadingPortName { get; set; }
        public string TransportModeCode { get; set; }
        public string TransportModeName { get; set; }
        public string NotifyDays { get; set; }
        public string TermsCondition { get; set; }
        public string TermsOfPayment { get; set; }
        public string TotalPoAmount { get; set; }
        public string TotalPoAmountBDT { get; set; }
        public string Remarks { get; set; }
        public string PoStatus { get; set; }
        public virtual ICollection<PurchaseOrderDtlBEL> PODetailList { get; set; }
    }

    public class PurchaseOrderDtlBEL
    {
        public string PoDtlSlno { get; set; }
        public string PoMstSlno { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CompanyCode { get; set; }       
        public string CompanyName { get; set; }
        public string NoOfPack { get; set; }
        public string NoOfPcs { get; set; }
        public string PricePerPack { get; set; }
        public string TotalAmount { get; set; }
        public string ConversionRate { get; set; }
        public string TotalAmountBDT { get; set; }

        public string PackSize { get; set; }
        public string QtyPerPack { get; set; }

    }
}