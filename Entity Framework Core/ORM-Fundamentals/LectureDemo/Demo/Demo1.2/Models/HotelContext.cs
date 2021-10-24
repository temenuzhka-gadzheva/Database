using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Demo1._2.Models
{
    public partial class HotelContext : DbContext
    {
        public HotelContext()
        {
        }

        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BedType> BedTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Occupancy> Occupancies { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=Hotel");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<BedType>(entity =>
            {
                entity.HasKey(e => e.BedType1)
                    .HasName("PK__BedTypes__80514389B60B74C2");

                entity.Property(e => e.BedType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BedType");

                entity.Property(e => e.Notes).IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.EmergencyName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EmergencyNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Occupancy>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DateOccupied).HasColumnType("date");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.PhoneCharge).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.RateApplied).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AmountCharged).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.FirstDateOccupied).HasColumnType("date");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastDateOccupied).HasColumnType("date");

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.PaymentTotal).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TaxRate).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TotalDays).HasComputedColumnSql("(datediff(day,[FirstDateOccupied],[LastDateOccupied]))", false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BedType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.RoomNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.RoomStatus).HasMaxLength(50);

                entity.Property(e => e.RoomType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoomStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RoomStatus");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.RoomStatus1).HasColumnName("RoomStatus");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasKey(e => e.RoomType1)
                    .HasName("PK__RoomType__3A76E8C23592ED44");

                entity.Property(e => e.RoomType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RoomType");

                entity.Property(e => e.Notes).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
