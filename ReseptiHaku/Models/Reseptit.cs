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
    
    public partial class Reseptit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reseptit()
        {
            this.MenuLista = new HashSet<MenuLista>();
            this.ReseptienVaiheidenLista = new HashSet<ReseptienVaiheidenLista>();
        }
    
        public int ReseptiID { get; set; }
        public string ReseptinNimi { get; set; }
        public int ReseptiVaiheetListaID { get; set; }
        public int ReseptiAinesosaListaID { get; set; }
        public Nullable<int> AnnosKoko { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLista> MenuLista { get; set; }
        public virtual ReseptienAinesosaLista ReseptienAinesosaLista { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReseptienVaiheidenLista> ReseptienVaiheidenLista { get; set; }
    }
}
