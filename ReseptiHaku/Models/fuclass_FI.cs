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
    
    public partial class fuclass_FI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fuclass_FI()
        {
            this.food = new HashSet<food>();
            this.food1 = new HashSet<food>();
        }
    
        public string THSCODE { get; set; }
        public string DESCRIPT { get; set; }
        public string LANG { get; set; }
        public string CategoryName { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<food> food { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<food> food1 { get; set; }
    }
}
