using Microsoft.EntityFrameworkCore;

namespace AutoMappingDemo.Model
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

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<SongsPerformer> SongsPerformers { get; set; }
        public virtual DbSet<Writer> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=MusicHub");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasIndex(e => e.ProducerId);
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.HasIndex(e => e.AlbumId);

                entity.HasIndex(e => e.WriterId);
            });

            modelBuilder.Entity<SongsPerformer>(entity =>
            {
                entity.HasKey(e => new { e.PerformerId, e.SongId });

                entity.HasIndex(e => e.SongId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
