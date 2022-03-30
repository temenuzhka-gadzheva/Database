namespace RealEstates.Services
{
   public interface ITagService
    {
        void Add(string name, int? importance = null);

        // for every property to create tag 
        void BulkTagToPropertiesRelatoin();
    }
}
