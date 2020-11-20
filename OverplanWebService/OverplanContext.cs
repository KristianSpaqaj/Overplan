namespace OverplanWebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OverplanContext : DbContext
    {
        public OverplanContext()
            : base("name=OverplanContext")
        {
            //vi skriver det her så vi kan tilslutte os til databasen
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Afdelingsplan> Afdelingsplans { get; set; }
        public virtual DbSet<Medarbejdersplan> Medarbejdersplans { get; set; }
        public virtual DbSet<Vagtplan> Vagtplans { get; set; }
        public virtual DbSet<Virksomhed> Virksomheds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medarbejdersplan>()
                .Property(e => e.Navn)
                .IsUnicode(false);

            modelBuilder.Entity<Medarbejdersplan>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Medarbejdersplan>()
                .HasMany(e => e.Afdelingsplans)
                .WithRequired(e => e.Medarbejdersplan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vagtplan>()
                .HasMany(e => e.Afdelingsplans)
                .WithRequired(e => e.Vagtplan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Virksomhed>()
                .Property(e => e.Navn)
                .IsUnicode(false);

            modelBuilder.Entity<Virksomhed>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Virksomhed>()
                .HasMany(e => e.Afdelingsplans)
                .WithRequired(e => e.Virksomhed)
                .WillCascadeOnDelete(false);
        }
    }
}
