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
    
    public partial class mdBPanel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int PanelID { get; set; }
        public string DummyCol { get; set; }
        public Nullable<int> BayID { get; set; }
    
        public virtual mdBay mdBay { get; set; }
        public virtual mdPanel mdPanel { get; set; }
    }
}
