//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLsach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTPX
    {
        public int MAPX { get; set; }
        public int MASACH { get; set; }
        public int SL { get; set; }
        public string Index { get;set;}
    
        public virtual PHIEUXUAT PHIEUXUAT { get; set; }
        public virtual SACH SACH { get; set; }
    }
}
