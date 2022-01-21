using AutoMapper;
using AutoMappingDemo.Model;
using System.Linq;

namespace AutoMappingDemo.MapperProfiles
{
  public  class SongInfoDtoProfile: Profile
    {
        public SongInfoDtoProfile()
        {
            this.CreateMap<Song, SongInfoDto>()

                 .ForMember(x => x.Performers, options =>
                 {
                     options.MapFrom(x =>
                     string.Join(", ", x.SongsPerformers.Select(p => p.Performer.FirstName)));
                 })
                 //to convert from SongInfoDto to Song and Song to SongInfoDto
                 .ReverseMap();
        }
    }
}
