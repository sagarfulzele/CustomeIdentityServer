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
    
    public partial class mdArrangementAcLoopRow
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mdArrangementAcLoopRow()
        {
            this.mdPanels = new HashSet<mdPanel>();
        }
    
        public Nullable<int> SubstationID { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int RowID { get; set; }
        public string ArrangementType { get; set; }
        public string Phases { get; set; }
        public Nullable<int> LoopStartingPanelID { get; set; }
        public Nullable<int> LoopEndingPanelID { get; set; }
        public Nullable<int> AcSupplyInPanelId { get; set; }
    
        public virtual mdSubstation mdSubstation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdPanel> mdPanels { get; set; }
    }
}