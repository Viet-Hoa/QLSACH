using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLsach.Models
{
    public class NHAPModel
    {
        public PHIEUNHAP pHIEUNHAP { get; set; }
        public List<CTPN> cTPNs { get; set; }
    }
}