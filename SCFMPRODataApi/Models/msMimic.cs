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
    
    public partial class msMimic
    {
        public Nullable<int> BayID { get; set; }
        public string Mimics { get; set; }
        public int ID { get; set; }
    
        public virtual msSubstationStructure msSubstationStructure { get; set; }
    }
}
