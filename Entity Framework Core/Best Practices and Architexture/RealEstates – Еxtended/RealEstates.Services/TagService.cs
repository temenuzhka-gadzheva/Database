using RealEstates.Data;
using RealEstates.Models;
using System.Linq;

namespace RealEstates.Services
{
    public class TagService : ITagService
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
                    var tag = context.Tags.FirstOrDefault(x => x.Name == "скъп-имот");
                    property.Tags.Add(tag);
                }
                if (property.Price < averagePriceForDistrict)
                {
                    var tag = context.Tags.FirstOrDefault(x => x.Name == "евтин-имот");
                    property.Tags.Add(tag);
                }
            }
            context.SaveChanges();
        }
    }
}
