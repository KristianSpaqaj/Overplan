namespace OverplanWebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OverplanContext : DbContext
    {
        public OverplanContext()
            : base("name=OverplanContext2")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<EmployeeOverview> EmployeeOverviews { get; set; }
        public virtual DbSet<LoginOverview> LoginOverviews { get; set; }
        public virtual DbSet<ShiftOverview> ShiftOverviews { get; set; }
        public virtual DbSet<Roster> Rosters { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeOverview>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeOverview>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeOverview>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<LoginOverview>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<LoginOverview>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Roster>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<database_firewall_rules>()
                .Property(e => e.start_ip_address)
                .IsUnicode(false);

            modelBuilder.Entity<database_firewall_rules>()
                .Property(e => e.end_ip_address)
                .IsUnicode(false);
        }
    }
}
