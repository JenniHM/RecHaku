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
    
    public partial class user_food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user_food()
        {
            this.user_component_value = new HashSet<user_component_value>();
            this.user_specdiet = new HashSet<user_specdiet>();
        }
    
        public int FOODID { get; set; }
        public string FOODNAME { get; set; }
        public string FOODTYPE { get; set; }
        public string PROCESS { get; set; }
        public Nullable<int> EDPORT { get; set; }
        public string IGCLASS { get; set; }
        public string IGCLASSP { get; set; }
        public string FUCLASS { get; set; }
        public string FUCLASSP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_component_value> user_component_value { get; set; }
        public virtual user_foodtype_FI user_foodtype_FI { get; set; }
        public virtual user_fuclass_FI user_fuclass_FI { get; set; }
        public virtual user_fuclass_FI user_fuclass_FI1 { get; set; }
        public virtual user_igclass_FI user_igclass_FI { get; set; }
        public virtual user_igclass_FI user_igclass_FI1 { get; set; }
        public virtual user_process_FI user_process_FI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_specdiet> user_specdiet { get; set; }
    }
}
