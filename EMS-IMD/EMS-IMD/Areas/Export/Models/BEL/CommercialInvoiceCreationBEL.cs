using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class CommercialInvoiceCreationBEL 
    {
     
        public Int64? CommInvoMSTSlno { get; set; }
        public string CommInvoiceNo { get; set; }
        public string CommInvoiceDate { get; set; }
        public string BuyerCode { get; set; }
        //public string PackingList { get; set; }
        public string BuyerName { get; set; }
        public int NetInvoiceAmount { get; set; }
        public int NetInvoiceAmountInBDT { get; set; }
        public string CurrentDate { get; set; }
        public int GetIPAddress { get; set; }
        public string Status { get; set; }
        public decimal TotalAmtPack { get; set; }
        public decimal TotalAmtBDT { get; set; }

        IList<CommercialInvoicePackingList> _packList = new List<CommercialInvoicePackingList>();
        public IList<CommercialInvoicePackingList> packList
        {
            get { return _packList; }
            set { _packList = value; }
        }

        public virtual ICollection<CommercialInvoicePackingList> packingList { get; set; }

        IList<CommercialInvoiceProductDetail> _proList = new List<CommercialInvoiceProductDetail>();
        public IList<CommercialInvoiceProductDetail> proList
        {
            get { return _proList; }
            set { _proList = value; }
        }

        public virtual ICollection<CommercialInvoiceProductDetail> productList { get; set; }



        
    }

    public class CommercialInvoicePackingList 
    {
        public Int64? CommInvoDTLSlno { get; set; }
        public Int64? CommInvoMSTSlno { get; set; }
        //public int PackingListNo { get; set; }
        public string PackingListDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProformaInvoiceDate { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public string SapSoNo { get; set; }
        public string SapSoDate { get; set; }
        public string DestinationPort { get; set; }
        public string LoadingPort { get; set; }
        public string ModeOfShipment { get; set; }
       

        public string PackingList { get; set; }
    }


    public class CommercialInvoiceProductDetail
    {
        public Int64 CommInvoiceProdSlno { get; set; }
        public Int64 CommInvoMSTSlno { get; set; }
        public string ProductCode { get; set; }
        public string PackingList { get; set; }
        public string PackingListDate { get; set; }
        public int InvoiceQtyInPack { get; set; }
        public int InvoiceQtyInPcs { get; set; }
        public int PackingQtyInPack { get; set; }
        public int PackingQtyInPcs { get; set; }
        public decimal PricePerPack { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountBDT { get; set; }
        public decimal ConversionRate { get; set; }

        public int OrderQtyInPack { get; set; }
        public int OrderQtyInPcs { get; set; }
       
    }



    public class CommercialInvoiceSearch
    {
        public Int64? CommInvoMSTSlno { get; set; }
        public string CommInvoiceNo { get; set; }
        public string CommInvoiceDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public int NetInvoiceAmount { get; set; }
        public int NetInvoiceAmountInBDT { get; set; }
        public string CurrentDate { get; set; }
        public int GetIPAddress { get; set; }
        public string Status { get; set; }
        public Int64? CommInvoDTLSlno { get; set; }
        //public Int64? CommInvoMSTSlno { get; set; }
        //public int PackingList { get; set; }
        public string PackingListDate { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProformaInvoiceDate { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public string SapSoNo { get; set; }
        public string SapSoDate { get; set; }
        public string DestinationPort { get; set; }
        public string LoadingPort { get; set; }
        public string ModeOfShipment { get; set; }
        public string PackingList { get; set; }
        public Int64 CommInvoiceProdSlno { get; set; }
        //public Int64 CommInvoMSTSlno { get; set; }
        public string ProductCode { get; set; }
        //public string PackingList { get; set; }
        //public string PackingListDate { get; set; }
        public int InvoiceQtyInPack { get; set; }
        public int InvoiceQtyInPcs { get; set; }
        public int PackingQtyInPack { get; set; }
        public int PackingQtyInPcs { get; set; }
        public decimal PricePerPack { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountBDT { get; set; }
        public decimal ConversionRate { get; set; }

        public int OrderQtyInPack { get; set; }
        public int OrderQtyInPcs { get; set; }


    }




}