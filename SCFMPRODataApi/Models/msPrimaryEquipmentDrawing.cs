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
    
    public partial class msPrimaryEquipmentDrawing
    {
        public string PrimaryDrawings { get; set; }
        public Nullable<int> PrimaryEquimentsID { get; set; }
        public bool EquipmementIncluded { get; set; }
        public int ID { get; set; }
    
        public virtual msPrimaryEquipment msPrimaryEquipment { get; set; }
    }
}
