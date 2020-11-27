namespace OverplanWebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OverplanContext : DbContext
    {
        public OverplanContext()
            : base("name=OverplanContext1")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<EmployeeOverview> EmployeeOverview { get; set; }
        public virtual DbSet<ShiftOverview> ShiftOverview { get; set; }
        public virtual DbSet<Roster> Roster { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeOverview>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeOverview>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeOverview>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeOverview>()
                .HasMany(e => e.ShiftOverview)
                .WithRequired(e => e.EmployeeOverview)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeOverview>()
                .HasMany(e => e.ShiftOverview1)
                .WithMany(e => e.EmployeeOverview1)
                .Map(m => m.ToTable("RosterOverview").MapLeftKey("EmployeeID").MapRightKey("ShiftID"));

            modelBuilder.Entity<Roster>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
