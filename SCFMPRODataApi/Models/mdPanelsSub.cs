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
    
    public partial class mdPanelsSub
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mdPanelsSub()
        {
            this.mdPanelFunctionsSubs = new HashSet<mdPanelFunctionsSub>();
            this.mdPanelSizesSubs = new HashSet<mdPanelSizesSub>();
        }
    
        public Nullable<int> SubstationID { get; set; }
        public string PanelType { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int PanelID { get; set; }
        public string PanelNo { get; set; }
        public string DwgNo { get; set; }
        public string WiringScheduleDwgNo { get; set; }
        public string SipTbGroup { get; set; }
        public string CableScheduleDwgNo { get; set; }
        public string ACPhase { get; set; }
        public bool BusBarProtection { get; set; }
        public bool SyncCircuit { get; set; }
        public bool IsSupplyByPSI { get; set; }
        public bool AnnCircuit { get; set; }
        public bool IndCircuit { get; set; }
        public string SIP_Group { get; set; }
        public string TB_Group { get; set; }
        public string PentaID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdPanelFunctionsSub> mdPanelFunctionsSubs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdPanelSizesSub> mdPanelSizesSubs { get; set; }
    }
}
