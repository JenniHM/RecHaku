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
    
    public partial class MenuLista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuLista()
        {
            this.MenuVaihde = new HashSet<MenuVaihde>();
            this.SuosikkiMenut = new HashSet<SuosikkiMenut>();
        }
    
        public int MenuID { get; set; }
        public string MenunNimi { get; set; }
        public Nullable<int> LoginID { get; set; }
        public Nullable<bool> Julkinen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuVaihde> MenuVaihde { get; set; }
        public virtual Logins Logins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuosikkiMenut> SuosikkiMenut { get; set; }
        public virtual MenunKategoriat MenunKategoriat { get; set; }

    }
}
