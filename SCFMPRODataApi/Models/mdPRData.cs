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
    
    public partial class mdPRData
    {
        public string PanelType { get; set; }
        public Nullable<int> PathID { get; set; }
        public string ExtractMaterialFromLoc { get; set; }
        public string PanelLoc { get; set; }
        public bool UseOnlyExtractConsumables { get; set; }
        public Nullable<int> PanelParentID { get; set; }
        public int PanelID { get; set; }
        public bool IncludeInReport { get; set; }
        public string Family { get; set; }
        public bool HasErr { get; set; }
        public Nullable<int> MprPanelID { get; set; }
        public string ParentType { get; set; }
        public string PR_NO { get; set; }
        public string GTN_NO { get; set; }
        public string SchemeRefreshedOnDateTime { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int PrDataId { get; set; }
        public Nullable<int> SubstationID { get; set; }
    }
}
