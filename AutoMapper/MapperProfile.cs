using AutoMapper;
using SuppliesPriceLister.Entity;
using SuppliesPriceLister.ViewModel;
using System;

namespace SuppliesPriceLister.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HumphriesItem, ListingVm>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.desc))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.identifier))
                .ForMember(dest => dest.PriceIdAud, opt => opt.MapFrom(src => src.costAUD));

            CreateMap<(Supply, bool, double, bool), ListingVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dest => dest.PriceIdAud, opt => opt.MapFrom(src => src.Item1.PriceInCents))
                .AfterMap((src, dest) =>
                {
                    var (supply, isUsd, audIsdExchangeRate, inCents) = src;
                    dest.PriceIdAud = Math.Round(supply.PriceInCents / (inCents ? 100.0 : 1.0) / (isUsd ? audIsdExchangeRate : 1.0), 2);
                });
        }
    }
}
