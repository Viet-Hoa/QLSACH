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
    
    public partial class SACH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SACH()
        {
            this.CTPNs = new HashSet<CTPN>();
            this.CTPXes = new HashSet<CTPX>();
            this.SACHBANDCs = new HashSet<SACHBANDC>();
            this.TONKHOes = new HashSet<TONKHO>();
        }
    
        public int MASACH { get; set; }
        public string TENSACH { get; set; }
        public int MANXB { get; set; }
        public int SL { get; set; }
        public int DGM { get; set; }
        public int DGB { get; set; }
        public string TACGIA { get; set; }
        public string THELOAI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPN> CTPNs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPX> CTPXes { get; set; }
        public virtual NXB NXB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SACHBANDC> SACHBANDCs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TONKHO> TONKHOes { get; set; }
    }
}
