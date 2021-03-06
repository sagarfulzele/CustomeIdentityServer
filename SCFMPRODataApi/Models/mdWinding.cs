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
    
    public partial class mdWinding
    {
        public string WindingName { get; set; }
        public string Ratio { get; set; }
        public string PrimaryWinding { get; set; }
        public string SecondaryWinding { get; set; }
        public string Class { get; set; }
        public string Burden { get; set; }
        public string WindingFunction { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int WidingID { get; set; }
        public Nullable<int> PrimaryEquipmentId { get; set; }
    
        public virtual mdVoltageTransformer mdVoltageTransformer { get; set; }
    }
}
