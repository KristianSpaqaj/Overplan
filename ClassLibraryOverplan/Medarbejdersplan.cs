namespace ClassLibraryOverplan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Medarbejdersplan")]
    public partial class Medarbejdersplan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medarbejdersplan()
        {
            Afdelingsplans = new HashSet<Afdelingsplan>();
        }

        [Key]
        public int MedarbejderID { get; set; }

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
