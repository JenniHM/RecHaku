//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReseptiHaku.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReseptienVaiheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReseptienVaiheet()
        {
            this.ReseptienVaiheidenLista = new HashSet<ReseptienVaiheidenLista>();
        }
    
        public int ReseptiVaiheID { get; set; }
        public string ReseptiVaihe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReseptienVaiheidenLista> ReseptienVaiheidenLista { get; set; }
    }
}
