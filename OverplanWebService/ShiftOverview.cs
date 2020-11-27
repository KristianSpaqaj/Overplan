namespace OverplanWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShiftOverview")]
    public partial class ShiftOverview
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShiftOverview()
        {
            EmployeeOverview1 = new HashSet<EmployeeOverview>();
        }

        [Key]
        public int ShiftID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public virtual EmployeeOverview EmployeeOverview { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeOverview> EmployeeOverview1 { get; set; }
    }
}
