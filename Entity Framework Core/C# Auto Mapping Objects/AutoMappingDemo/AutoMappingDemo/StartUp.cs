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
            var songs = GetSongs("s");
            foreach (var song in songs)
            {
                Console.WriteLine($" This song -> {song.Name} was written by -> {song.WriterName}");
            }
            // json object 
            // untidy
            Console.WriteLine(JsonConvert.SerializeObject(songs));
            // in good order
            Console.WriteLine(JsonConvert.SerializeObject(songs, Formatting.Indented));
        }


        //service method
        static IEnumerable<SongInfoDto> GetSongs(string input)
        {
            var db = new MusicHubContext();
            // mapping
            var songs = db.Songs
                .Where(x => x.Name.Contains(input))
                .Select(x => new SongInfoDto
                {
                    Name = x.Name,
                    WriterName = x.Writer.Name,
                    CreatedOn = x.CreatedOn,
                    Price = x.Price,
                   // IsDeleted = x.IsDeleted


                })
                .ToList();

            return songs;
        }

        static SongInfoDto GetSongById(int id)
        {
            var db = new MusicHubContext();

            Songs song = db.Songs
                .Where(x => x.Id == id)
                .FirstOrDefault();

            // mapping
            var songDto = new SongInfoDto
            {
                Name = song.Name,
                WriterName = song.Writer.Name,
                CreatedOn = song.CreatedOn,
                Price = song.Price,
               // IsDeleted = song.IsDeleted

            };

            return songDto;
        }
    }

    public class SongInfoDto
    {
        public string Name { get; set; }
        public string WriterName { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Price { get; set; }
       // public bool IsDeleted { get; set; }
    }


}
