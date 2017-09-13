using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS_IMD.Areas.Export.Models.BEL
{
    public class DestinationPortBEL
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string TransportModeCode { get; set; }
        public string TransportModeName { get; set; }
        public string PortCode { get; set; }
        public string PortName { get; set; }

    }
}