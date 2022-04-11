using RealEstates.Data;
using RealEstates.Models;
using System;
using System.Linq;

namespace RealEstates.Services
{
    public class TagService : BaseService, ITagService
    {
        private readonly ApplicationDbContext context;
        private readonly IPropertiesService propertiesService;
        public TagService(ApplicationDbContext context, IPropertiesService propertiesService)
        {
            this.context = context;
            this.propertiesService = propertiesService;
        }

        public void Add(string name, int? importance = null)
        {
            var tag = new Tag
            {
                Name = name,
                Importance = importance
            };

            this.context.Tags.Add(tag);
            this.context.SaveChanges();
        }

        public void BulkTagToPropertiesRelatoin()
        {
            // fetch all properties
            // set tags
            // save changes
            var allProperties = context.Properties
                .ToList();

            foreach (var property in allProperties)
            {
                var averagePriceForDistrict = this.propertiesService
                    .AveragePricePerSquareMeter(property.DistrictId);

                if (property.Price > averagePriceForDistrict)
                {
                    var tag = GetTag("скъп-имот");
                    property.Tags.Add(tag);
                }
                else if (property.Price < averagePriceForDistrict)
                {
                    var tag = GetTag("евтин-имот");
                    property.Tags.Add(tag);
                }

                // ново-строителство
                // старо-строителство

                var currentDate = DateTime.Now.AddYears(-15);

                if (property.Year.HasValue && property.Year <= currentDate.Year)
                {
                    var tag = GetTag("старо-строителство");
                    property.Tags.Add(tag);
                }
                else if (property.Year.HasValue && property.Year > currentDate.Year)
                {
                    var tag = GetTag("ново-строителство");
                    property.Tags.Add(tag);
                }

                // малък-имот
                // голям-имот

                var averagePropertySize = this.propertiesService.
                    AverageSize(property.DistrictId);

                if (property.Size >= averagePropertySize)
                {
                    var tag = GetTag("голям-имот");
                    property.Tags.Add(tag);
                }

                else if (property.Size < averagePropertySize)
                {
                    var tag = GetTag("малък-имот");
                    property.Tags.Add(tag);
                }

                // хубава-гледка
                // първи-етаж

                if (property.Floor.HasValue && property.Floor.Value == 1)
                {
                    var tag = GetTag("първи-етаж");
                    property.Tags.Add(tag);
                }
                else if (property.Floor.HasValue && property.Floor.Value > 6)
                {
                    var tag = GetTag("хубава-гледка");
                    property.Tags.Add(tag);
                }
            }
            context.SaveChanges();
        }

        private Tag GetTag(string tagName)
            // return tag
            => context.Tags.FirstOrDefault(x => x.Name == tagName);

    }
}
