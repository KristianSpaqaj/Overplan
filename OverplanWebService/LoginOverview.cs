namespace OverplanWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginOverview")]
    public partial class LoginOverview
    {
        [Key]
        [StringLength(10)]
        public string Username { get; set; }

        [Required]
        [StringLength(8)]
        public string Password { get; set; }

        public bool Leader { get; set; }
    }
}
