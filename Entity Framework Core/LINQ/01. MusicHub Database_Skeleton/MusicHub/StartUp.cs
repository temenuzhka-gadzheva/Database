namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
            // Console.WriteLine(ExportAlbumsInfo(context, 9));
            Console.WriteLine(ExportSongsAboveDuration(context, 4));

        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Albums
                .ToList()
                .Where(x => x.ProducerId == producerId)
              .Select(album => new
              {
                  AlbumName = album.Name,
                  ReleaseDate = album.ReleaseDate,
                  ProducerName = album.Producer.Name,
                  Songs = album.Songs
                  .Select(song => new
                  {
                      SongName = song.Name,
                      Price = song.Price,
                      Writer = song.Writer.Name
                  })
                  .OrderByDescending(song => song.SongName)
                  .ThenBy(song => song.Writer)
                  .ToList(),
              AlbumPrice = album.Price
              })
              .OrderByDescending(album => album.AlbumPrice)
              .ToList();

            var sb = new StringBuilder();
          
            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");


                var counter = 1;
                foreach (var item in album.Songs)
                {
                    sb.AppendLine($"---#{counter}");
                    sb.AppendLine($"---SongName: {item.SongName}");
                    sb.AppendLine($"---Price: {item.Price:F2}");
                    sb.AppendLine($"---Writer: {item.Writer}");
                    counter++;
                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            }

            return sb.ToString().Trim();

        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .ToList()
                .Where(x => x.Duration.TotalSeconds > duration)
                .Select(song => new
                {
                    SongName = song.Name,
                    Writer = song.Writer.Name,
                    PerformerFullName = song.SongPerformers
                   .Select(performer => performer.Performer.FirstName + " " + performer.Performer.LastName)
                   .FirstOrDefault(),
                   AlbumProducer = song.Album.Producer.Name,
                   Duration = song.Duration
                })
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.Writer)
                .ThenBy(x => x.PerformerFullName)
                .ToList();

            var sb = new StringBuilder();
            var counter = 1;

            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{counter}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.Writer}");
                sb.AppendLine($"---Performer: {song.PerformerFullName}");
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration:c}");

                counter++;
            }

            return sb.ToString().Trim();
         
        }
    }
}
