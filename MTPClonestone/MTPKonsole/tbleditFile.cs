//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTPKonsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbleditFile
    {
        public int ideditfile { get; set; }
        public int fkedit { get; set; }
        public System.Guid uidfile { get; set; }
        public byte[] editfile { get; set; }
        public string mimetype { get; set; }
    
        public virtual tbledit tbledit { get; set; }
    }
}