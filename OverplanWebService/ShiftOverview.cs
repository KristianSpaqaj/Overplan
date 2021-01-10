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
        [Key]
        public int ShiftID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public virtual EmployeeOverview EmployeeOverview { get; set; }
    }
}
