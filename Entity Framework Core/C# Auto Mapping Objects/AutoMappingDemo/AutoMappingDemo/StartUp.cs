using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMappingDemo.MapperProfiles;
using AutoMappingDemo.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMappingDemo
{
    public class StartUp
    {
        static void Main()
        {
            // mapping with auto mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SongInfoDtoProfile());

                cfg.CreateMap<Song, SongNameDto>();
            });

            var mapper = config.CreateMapper();
            // for  the example with auto mapping make a copy of mappings
            var db = new MusicHubContext();

            // first example  with auto mapper
            /*  var song = db.Songs
                 .Where(x => x.Id == 4)
                 .FirstOrDefault();*/

            // mapping without auto mapper
            /*   var songDto = new SongInfoDto
               {
                   Name = song.Name,
                   Duration = song.Duration,
                   CreatedOn = song.CreatedOn,
                   Price = song.Price,
                   WriterName = song.Writer.Name
                   // IsDeleted = x.IsDeleted
               }; */

            // with auto mapper
            // first example
            // var songDto2 = mapper.Map<SongInfoDto>(song);
            // Console.WriteLine(JsonConvert.SerializeObject(songDto2, Formatting.Indented));

            //second example
            /* var songDto3 = mapper.Map<SongNameDto>(song);
             Console.WriteLine(JsonConvert.SerializeObject(songDto3, Formatting.Indented));*/


            /*  var songs = GetSongs("s");
              foreach (var song2 in songs)
              {
                  Console.WriteLine($" This song -> {song2.Name} was written by -> {song2.WriterName}");
              }
              // json object 
              // untidy
              Console.WriteLine(JsonConvert.SerializeObject(songs));
              // in good order
              Console.WriteLine(JsonConvert.SerializeObject(songs, Formatting.Indented));*/
             var songs = db.Songs.Where(x => x.Name.Contains("a"))
                      // select and project to are equal
                      .ProjectTo<SongInfoDto>(config)
                      /* .Select(x => new SongInfoDto
                       {
                           Name = x.Name,
                           Duration = x.Duration,
                           CreatedOn = x.CreatedOn,
                           Price = x.Price,
                           WriterName = x.Writer.Name,
                           Performers = string.Join(", ", x.SongsPerformers.Select(p => p.Performer.FirstName))
                           // IsDeleted = x.IsDeleted
                       })*/
                      .ToList();

            // Console.WriteLine(JsonConvert.SerializeObject(songs, Formatting.Indented));

            // from SongInfoDto to create Song

            var songDto = songs.FirstOrDefault();
            Song dbSong = mapper.Map<Song>(songDto);
        }


        //service method
        public static IEnumerable<SongInfoDto> GetSongs(string input)
        {
            var db = new MusicHubContext();
            // mapping
            var songs = db.Songs
                .Where(x => x.Name.Contains(input))
                .Select(x => new SongInfoDto
                {
                    Name = x.Name,
                    Duration = x.Duration,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                    WriterName = x.Writer.Name,
                    // IsDeleted = x.IsDeleted


                })
                .ToList();

            return songs;
        }

        public static SongInfoDto GetSongById(int id)
        {
            var db = new MusicHubContext();

            Song song = db.Songs
                .Where(x => x.Id == id)
                .FirstOrDefault();

            // mapping
            var songDto = new SongInfoDto
            {
                Name = song.Name,
                Duration = song.Duration,
                CreatedOn = song.CreatedOn,
                Price = song.Price,
                // IsDeleted = song.IsDeleted

            };

            return songDto;
        }


    }
    //create mapping 
    public class SongNameDto
    {
        public string Name { get; set; }
    }
    public class SongInfoDto
    {
        public string Name { get; set; }
        public string Performers { get; set; }
        public string WriterName { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Price { get; set; }
        // public bool IsDeleted { get; set; }
    }


}
