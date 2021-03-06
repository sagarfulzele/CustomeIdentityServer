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
    
    public partial class mdPrimaryEquipment
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int PrimaryEquipmentId { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<bool> Approval { get; set; }
        public string Description { get; set; }
        public string DrawingNumber { get; set; }
        public Nullable<bool> AddedInProject { get; set; }
        public Nullable<bool> IsOptional { get; set; }
        public string PsiStandardAttribute { get; set; }
        public Nullable<int> BayID { get; set; }
    
        public virtual mdBay mdBay { get; set; }
        public virtual mdCircuitBreaker mdCircuitBreaker { get; set; }
        public virtual mdCurrentTransformer mdCurrentTransformer { get; set; }
        public virtual mdEarthSwitch mdEarthSwitch { get; set; }
        public virtual mdIsolator mdIsolator { get; set; }
        public virtual mdPowerTransformer mdPowerTransformer { get; set; }
        public virtual mdVoltageTransformer mdVoltageTransformer { get; set; }
    }
}
