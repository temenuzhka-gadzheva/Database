namespace MusicHub
{
    using System;
    using System.Linq;
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
       
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            /*  var albums = context.Albums
                  .Where(x => x.ProducerId == producerId)
                  .Select(x => new
                  {
                      Name = x.Name,
                      ReleaseDate = x.ReleaseDate,
                      ProducerName = x.Producer.Name,
                      AlbumSongs = new
                      {
                          SongName = x.Songs.OrderByDescending(x => x.Name),
                          Price = x.Price,
                          WriterName = x.Songs
                      }
                  }) ;

              return albums.ToString();*/
            throw new NotImplementedException();

        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            
            throw new NotImplementedException();
        }
    }
}
