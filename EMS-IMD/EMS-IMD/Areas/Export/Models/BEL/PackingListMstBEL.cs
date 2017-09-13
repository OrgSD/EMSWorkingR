using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class PackingListMstBEL
    {
        public string PackingListSlNo { get; set; }
        public string PackingDate { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string NoOfCtn { get; set; }
        public string NoOfPack { get; set; }
        public string NetWeight { get; set; }
        public string NetWeightUnit { get; set; }
        public string GrossWeight { get; set; }
        public string GrossWeightUnit { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public virtual ICollection<PackingListProdDetailsBEL> PackingProdDetailsList { get; set; }
        public virtual ICollection<PackingListProformaMstBEL> PackingProformaMSTList { get; set; }
        public virtual ICollection<PackingListProformaDtlBEL> PackingProformaDTLList { get; set; }
    }

    public class PackingListProdDetailsBEL
    {
        public string PackingListProdSlNo { get; set; }
        public string PackingListSlNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string MCSlNo { get; set; }
        public string NoOfMC { get; set; }
        public string NoOfScPerMc { get; set; }
        public string PackPerSc { get; set; }
        public string PackingQtyInPack { get; set; }
        public string BatchNo { get; set; }
        public string ManufacturingDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }      
        public string NetWeight { get; set; }
        public string NetWeightUnit { get; set; }
        public string GrossWeight { get; set; }
        public string GrossWeightUnit { get; set; }
    }

    public class PackingListProformaMstBEL
    {
        public string PackingListPISlNo { get; set; }
        public string PackingListSlNo { get; set; }
        public string ProformaInvoiceNo { get; set; }
        public string ProformaInvoiceDate { get; set; }
        public string SAPSONo { get; set; }
        public string SAPSODate { get; set; }
        public string PoNo { get; set; }
        public string PoDate { get; set; }
    }

    public class PackingListProformaDtlBEL
    {
        public string PackingListPIDtlSlNo { get; set; }
        public string PackingListPISlNo { get; set; }
        public string PackingListSlNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PIQtyInPack { get; set; }
        public string PIQtyInPcs { get; set; }
        public string PackingQtyInPack { get; set; }
        public string PackingQtyInPcs { get; set; }
        public string Qty { get; set; }
    }
}


