using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Demo.Data
{
    public partial class MinionsDBContext : DbContext
    {
        public MinionsDBContext()
        {
        }

        public MinionsDBContext(DbContextOptions<MinionsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<EvilnessFactor> EvilnessFactors { get; set; }
        public virtual DbSet<Minion> Minions { get; set; }
        public virtual DbSet<MinionsVillain> MinionsVillains { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Villain> Villains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=MinionsDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EvilnessFactor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Minion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Minions)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK__Minions__TownId__29572725");
            });

            modelBuilder.Entity<MinionsVillain>(entity =>
            {
                entity.HasKey(e => new { e.MinionId, e.VillainId })
                    .HasName("PK_MininonsVillains");

                entity.HasOne(d => d.Minion)
                    .WithMany(p => p.MinionsVillains)
                    .HasForeignKey(d => d.MinionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MinionsVi__Minio__30F848ED");

                entity.HasOne(d => d.Villain)
                    .WithMany(p => p.MinionsVillains)
                    .HasForeignKey(d => d.VillainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MinionsVi__Villa__31EC6D26");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.CountryCode)
                    .HasConstraintName("FK__Towns__CountryCo__267ABA7A");
            });

            modelBuilder.Entity<Villain>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EvilnessFactor)
                    .WithMany(p => p.Villains)
                    .HasForeignKey(d => d.EvilnessFactorId)
                    .HasConstraintName("FK__Villains__Evilne__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
