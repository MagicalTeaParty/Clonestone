//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClonestoneMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vEditor
    {
        public int idedit { get; set; }
        public int ideditfile { get; set; }
        public int idperson { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public Nullable<System.DateTime> editdate { get; set; }
        public byte[] editfile { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}