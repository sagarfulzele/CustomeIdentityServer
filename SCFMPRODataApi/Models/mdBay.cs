//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PsiMprODataApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mdBay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mdBay()
        {
            this.mdBPanels = new HashSet<mdBPanel>();
            this.mdPrimaryEquipments = new HashSet<mdPrimaryEquipment>();
        }
        [System.ComponentModel.DataAnnotations.Key]
        public int BayID { get; set; }
        public Nullable<int> VoltageLevelID { get; set; }
        public Nullable<int> BayType_id { get; set; }
        public string BayNo { get; set; }
        public string BayName { get; set; }
        public string Bus1 { get; set; }
        public string Bus2 { get; set; }
        public string Bus3 { get; set; }
        public string Position1 { get; set; }
        public string Position2 { get; set; }
        public string Position3 { get; set; }
        public string Bus4 { get; set; }
        public string Position4 { get; set; }
    
        public virtual mdVoltageLevel mdVoltageLevel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdBPanel> mdBPanels { get; set; }
        public virtual msSubstationStructure msSubstationStructure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdPrimaryEquipment> mdPrimaryEquipments { get; set; }
    }
}
