//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MON_HOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MON_HOC()
        {
            this.CAU_HOI = new HashSet<CAU_HOI>();
            this.LOP_HOC_PHAN = new HashSet<LOP_HOC_PHAN>();
        }
    
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public byte SoTC { get; set; }
        public string CtTinhDiem { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAU_HOI> CAU_HOI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOP_HOC_PHAN> LOP_HOC_PHAN { get; set; }
    }
}