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
    
    public partial class mdAllComponentsFromScheme
    {
        public Nullable<int> PathID { get; set; }
        public string Catalog { get; set; }
        public string Make { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string TagName { get; set; }
        public string Family { get; set; }
        public string Loc { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public string ComponentGroup { get; set; }
        public string ItemID { get; set; }
        public int ID { get; set; }
        public string Hdl { get; set; }
        public string DwgName { get; set; }
        public string PentaItemID { get; set; }
    
        public virtual mdSchematicProjectPath mdSchematicProjectPath { get; set; }
    }
}