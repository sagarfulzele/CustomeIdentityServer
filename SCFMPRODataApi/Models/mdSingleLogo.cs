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
    
    public partial class mdSingleLogo
    {
        public int LogoID { get; set; }
        public Nullable<int> LogoDetailID { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int SingleLogoID { get; set; }
    
        public virtual mdLogoDetail mdLogoDetail { get; set; }
        public virtual mdProjectLogo mdProjectLogo { get; set; }
    }
}
