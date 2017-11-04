using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLsach.Models
{
    public class XUATModel
    {
        public PHIEUXUAT pHIEUXUAT { get; set; }
        public List<CTPX> cTPXes { get; set; }
        public XUATModel()
        {
            cTPXes = new List<CTPX>();
        }
    }
}