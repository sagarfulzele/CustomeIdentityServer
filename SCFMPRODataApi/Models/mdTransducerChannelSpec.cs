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
    
    public partial class mdTransducerChannelSpec
    {
        public string CtrPtr { get; set; }
        public string Scale { get; set; }
        public Nullable<int> TransducerID { get; set; }
        public string ChannelNo { get; set; }
        public string ChannelFunction { get; set; }
        public string mARange { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int TransducerChannelSpecID { get; set; }
    
        public virtual mdTransducer mdTransducer { get; set; }
    }
}