//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClonestoneMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblgame
    {
        public int idgame { get; set; }
        public Nullable<int> fkperson1 { get; set; }
        public Nullable<int> fkperson2 { get; set; }
        public Nullable<byte> win { get; set; }
        public Nullable<System.DateTime> gamestart { get; set; }
        public Nullable<System.DateTime> gameende { get; set; }
    
        public virtual tblperson tblperson { get; set; }
        public virtual tblperson tblperson1 { get; set; }
    }
}
