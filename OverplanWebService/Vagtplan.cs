namespace OverplanWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vagtplan")]
    public partial class Vagtplan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vagtplan()
        {
            Afdelingsplans = new HashSet<Afdelingsplan>();
        }

        [Key]
        public int VagtID { get; set; }

        public DateTime DatoFra { get; set; }

        public DateTime DatoTil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Afdelingsplan> Afdelingsplans { get; set; }
    }
}
