using AutoMapper;
using RealEstates.Services.Profiler;

namespace RealEstates.Services
{
    public abstract class BaseService
    {
        public BaseService()
        {
            InitializeAutoMapper();
        }
        protected IMapper Mapper { get; private set; }
        private void InitializeAutoMapper()
        {
            // mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RealEstateProfile>();
            });

            this.Mapper = config.CreateMapper();
        }
    }
}
