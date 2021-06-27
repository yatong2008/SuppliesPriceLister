using AutoMapper;
using SuppliesPriceLister.Entity;
using SuppliesPriceLister.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace SuppliesPriceLister.Service
{
    public class JsonReaderService : IJsonReaderService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<JsonReaderService> _logger;
        public JsonReaderService(IMapper mapper, ILogger<JsonReaderService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public List<ListingVm> ReadFile(string fileName, bool priceInCents, CurrencyType currency, double audUsdExchangeRate)
        {
            try
            {
                var fileString = File.ReadAllText(fileName);
                var data = JsonSerializer.Deserialize<PartnersContainer>(fileString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = {new JsonStringEnumConverter()}
                });

                return (from partner in data.Partners
                    from item in partner.Supplies
                    select _mapper.Map<ListingVm>((item, currency == CurrencyType.USD, audUsdExchangeRate, priceInCents))).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ListingVm>();
            }
        }
    }
}
