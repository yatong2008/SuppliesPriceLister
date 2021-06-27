using AutoMapper;

namespace SuppliesPriceLister.AutoMapper
{
    /// <summary>
    /// Static Global AutoMapper Config file
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AddProfile(new MapperProfile());
            });
        }
    }
}
