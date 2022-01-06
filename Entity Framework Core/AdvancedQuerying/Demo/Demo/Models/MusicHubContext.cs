using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Models
{
    public partial class MusicHubContext : DbContext
    {
        public MusicHubContext()
        {
        }

        public MusicHubContext(DbContextOptions<MusicHubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Performers> Performers { get; set; }
        public virtual DbSet<Producers> Producers { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<SongsPerformers> SongsPerformers { get; set; }
        public virtual DbSet<Writers> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=MusicHub");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Albums>(entity =>
            {
                entity.HasIndex(e => e.ProducerId);
            });

            modelBuilder.Entity<Songs>(entity =>
            {
                entity.HasIndex(e => e.AlbumId);

                entity.HasIndex(e => e.WriterId);
            });

            modelBuilder.Entity<SongsPerformers>(entity =>
            {
                entity.HasKey(e => new { e.PerformerId, e.SongId });

                entity.HasIndex(e => e.SongId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
