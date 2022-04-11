using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstates.Services
{

    public class DistrictsService : BaseService, IDistrictsService
    {
        private readonly ApplicationDbContext context;
        public DistrictsService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IEnumerable<DistrictInfoDto> GetMostExpensiveDistricts(int count)
        {
            var districts = context.Districts
                .ProjectTo<DistrictInfoDto>(this.Mapper.ConfigurationProvider)
                /*.Select(x => new DistrictInfoDto
                {
                    Name = x.Name,
                    PropertiesCount = x.Properties.Count(),
                    AveragePricePerSquareMeter = x.Properties.
                  Where(p => p.Price.HasValue).
                  Average(p => p.Price / (decimal)p.Size) ?? 0
                })*/
                  .OrderByDescending(x => x.AveragePricePerSquareMeter)
                  .Take(count)
                  .ToList();

            return districts;
        }
    }
}
