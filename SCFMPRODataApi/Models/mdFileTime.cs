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
    
    public partial class mdFileTime
    {
        public Nullable<int> PathID { get; set; }
        public string DrawingFile { get; set; }
        public string LastSavedTime { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int FileTimeID { get; set; }
    
        public virtual mdSchematicProjectPath mdSchematicProjectPath { get; set; }
    }
}