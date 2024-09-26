using AutoMapper;

namespace Catalog.Application.Mappers
{
    public static class ProductMapper
    {
        //load mapper 
        private static Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            //configure mappers 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapField = propertyInfo => propertyInfo.IsPublic || propertyInfo.IsAssembly;
                cfg.AddProfile<ProductMappingProfile>();

            });
            //check config is valid 
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        });
        public static IMapper Mapper => lazy.Value;
    }
}
