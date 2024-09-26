using AutoMapper;

namespace Basket.Application.Mappers
{
    public static class BasketMapper
    {
        public static Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration((cfg) =>
            {
                cfg.ShouldMapField = propertyInfo => propertyInfo.IsPublic || propertyInfo.IsAssembly;
                cfg.AddProfile<BasketMappingProfile>();
            });
            return config.CreateMapper();
        });
        public static IMapper Mapper => lazy.Value;
    }
}
