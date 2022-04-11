using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class PropertiesService : BaseService, IPropertiesService
    {
        private readonly ApplicationDbContext context;

        public PropertiesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(string district, int price,
            int floor, int maxFloor, int size, int yardSize,
            int year, string propertyType, string buildingType)
        {
            var property = new Property
            {
                Size = size,
                Price = price <= 0 ? null : price,
                Floor = floor <= 0 || floor > 255 ? null : (byte)floor,
                TotalFloors = maxFloor <= 0 || maxFloor > 255 ? null : (byte)maxFloor,
                YardSize = yardSize <= 0 ? null : yardSize,
                Year = year <= 1800 ? null : year,
            };

            var dbDistrict = context.Districts.FirstOrDefault(x => x.Name == district);
            if (dbDistrict == null)
            {
                dbDistrict = new District { Name = district };
            }
            property.District = dbDistrict;

            var dbPropertyType = context.PropertyTypes.FirstOrDefault(x => x.Name == propertyType);
            if (dbPropertyType == null)
            {
                dbPropertyType = new PropertyType { Name = propertyType };
            }
            property.Type = dbPropertyType;

            var dbBuildingType = context.BuildingTypes.FirstOrDefault(x => x.Name == buildingType);
            if (dbBuildingType == null)
            {
                dbBuildingType = new BuildingType { Name = buildingType };
            }
            property.BuildingType = dbBuildingType;

            context.Properties.Add(property);
            context.SaveChanges();
        }

        public decimal AveragePricePerSquareMeter()
        {
            return context.Properties.Where(x => x.Price.HasValue)
                .Average(x => x.Price / (decimal)x.Size) ?? 0;
        }

        public decimal AveragePricePerSquareMeter(int districtId)
        {
            return context.Properties.Where(x => x.Price.HasValue && x.DistrictId == districtId)
                .Average(x => x.Price / (decimal)x.Size) ?? 0;
        }

        public double AverageSize(int districtId)
        {
            // average size per every section
            return context.Properties.Where(x => x.DistrictId == districtId)
                .Average(x => x.Size);
        }

        public IEnumerable<PropertyInfoFullData> GetFullData(int count)
        {
            var properties = context.Properties
                .Where(x => x.Floor.HasValue &&
                            x.Floor.Value > 1 &&
                            x.Floor.Value <= 8 &&
                            x.Year.HasValue && x.Year.Value > 2015)
                .ProjectTo<PropertyInfoFullData>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Price)
                .ThenBy(x => x.Size)
                .ThenBy(x => x.Year)
                .Take(count)
                .ToList();

            return properties;
        }
        public IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize)
        {
            var properties = context.Properties.
                   Where(x => x.Price >= minPrice &&
                         x.Price <= maxPrice &&
                         x.Size >= minSize &&
                         x.Size <= maxSize)
                  .ProjectTo<PropertyInfoDto>
                  (this.Mapper.ConfigurationProvider)
                  .ToList();

            return properties;
            /*
             .Select(x => new PropertyInfoDto 
             {
                 Size = x.Size,
                 Price = x.Price ?? 0,
                 BuildingType = x.BuildingType.Name,
                 DistrictName = x.District.Name,
                 PropertyType = x.Type.Name
             })*/
        }
    }
}
