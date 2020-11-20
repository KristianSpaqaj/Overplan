namespace OverplanWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Virksomhed")]
    public partial class Virksomhed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Virksomhed()
        {
            Afdelingsplans = new HashSet<Afdelingsplan>();
        }

        [Key]
        public int VirksomhedsID { get; set; }

        [Required]
        [StringLength(50)]
        public string Navn { get; set; }

        [Required]
        [StringLength(50)]
        public string Adresse { get; set; }

        public int Telefon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Afdelingsplan> Afdelingsplans { get; set; }
    }
}
