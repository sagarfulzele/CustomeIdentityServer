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
    
    public partial class mdSchematicProjectPath
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mdSchematicProjectPath()
        {
            this.mdAllComponentsFromSchemes = new HashSet<mdAllComponentsFromScheme>();
            this.mdAllTerminalBlocksFromSchemes = new HashSet<mdAllTerminalBlocksFromScheme>();
            this.mdFileTimes = new HashSet<mdFileTime>();
        }
    
        public string SchematicProjectPath1 { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int PathID { get; set; }
        public Nullable<int> SubstationID { get; set; }
        public Nullable<System.DateTime> RefreshOnDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdAllComponentsFromScheme> mdAllComponentsFromSchemes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdAllTerminalBlocksFromScheme> mdAllTerminalBlocksFromSchemes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mdFileTime> mdFileTimes { get; set; }
        public virtual mdSubstation mdSubstation { get; set; }
    }
}
