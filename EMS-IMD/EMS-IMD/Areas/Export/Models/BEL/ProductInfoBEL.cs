using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class ProductInfoBEL
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public string Strength { get; set; }
        public string GenericCode { get; set; }
        public string GenericName { get; set; }
        public string TherapeuticClassCode { get; set; }
        public string TherapeuticClassName { get; set; }
        public string Brand { get; set; }
        public string QtyPerPack { get; set; }
        public string RegistrationNo { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
        public string value { get; set; }
        public string text { get; set; }

        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string PricePerPack { get; set; }
    }
}