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
    
    public partial class tbledit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbledit()
        {
            this.tbleditFiles = new HashSet<tbleditFile>();
        }
    
        public int idedit { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public Nullable<int> fkperson { get; set; }
        public Nullable<System.DateTime> editdate { get; set; }
    
        public virtual tblperson tblperson { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbleditFile> tbleditFiles { get; set; }
    }
}
