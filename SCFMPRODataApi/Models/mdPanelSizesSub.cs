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
    
    public partial class mdPanelSizesSub
    {
        public Nullable<int> PanelID { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Depth { get; set; }
        public string Color { get; set; }
        public string Enclosure { get; set; }
        public Nullable<int> EndPlatesQty { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int PanelSizeID { get; set; }
    
        public virtual mdPanelsSub mdPanelsSub { get; set; }
    }
}