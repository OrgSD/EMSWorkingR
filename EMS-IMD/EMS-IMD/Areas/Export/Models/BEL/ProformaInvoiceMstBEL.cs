using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class ProformaInvoiceMstBEL
    {
        public string ProInvMstSlNo {get; set;}
        public string ProformaInvoiceNo {get; set;}
        public string ProformaInvoiceDate {get; set;}
        public string PoNo {get; set;}
        public string PoDate {get; set;}
        public string BuyerCode {get; set;}
        public string BuyerName {get; set;}
        public string BuyerAddress { get; set; }
        public string CompanyCode {get; set;}
        public string CompanyName {get; set;}
        public string ConsigneeCode {get; set;}
        public string ConsigneeName {get; set;}
        public string NotifyPartyCode {get; set;}
        public string NotifyPartyName {get; set;}
        public string OrderTypeCode {get; set;}
        public string OrderTypeName {get; set;}
        public string PriceTypeCode {get; set;}
        public string PriceTypeName {get; set;}
        public string IncoTermCode {get; set;}
        public string IncoTermName {get; set;}
        public string DestinationPortCode {get; set;}
        public string DestinationPortName {get; set;}
        public string LoadingPortCode {get; set;}
        public string LoadingPortName {get; set;}
        public string SAPSoNo {get; set;}
        public string SAPSoDate {get; set;}
        public string TransportModeCode {get; set;}
        public string TransportModeName { get; set; }
        public string TransshipmentCode {get; set;}
        public string PartialShipment {get; set;}
        public string PaymentType {get; set;}
        public string BankCode {get; set;}
        public string BankName {get; set;}
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string TermsOfPayment {get; set;}
        public string NotifyDays {get; set;}
        public string TotalPIAmount {get; set;}
        public string TotalPIAmountBDT {get; set;}
        public string Remarks {get; set;}
        public string ApprovedStatus { get; set; }
        public string ApprovedRemarks { get; set; }
        public virtual ICollection<ProformaInvoiceDtlBEL> ProformaDetailList { get; set; }
    }

    public class ProformaInvoiceDtlBEL
    {
        public string ProInvDtlSlNo { get; set; }
        public string ProInvMstSlNo { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string NoOfPack { get; set; }
        public string NoOfPcs { get; set; }
        public string QtyPerPack { get; set; }
        public string PricePerPack { get; set; }
        public string ConversionRate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string TotalAmount { get; set; }
        public string TotalAmountBDT { get; set; }
        public string PackingQtyInPack { get; set; }
    }

}
 