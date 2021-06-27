using Microsoft.Extensions.Configuration;
using SuppliesPriceLister.Entity;
using SuppliesPriceLister.Service;
using SuppliesPriceLister.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuppliesPriceLister
{
    public class App
    {
        private readonly IConfigurationRoot _config;
        private readonly ICsvReaderService _csvReaderService;
        private readonly IJsonReaderService _jsonReaderService;

        public App(IConfigurationRoot config, ICsvReaderService csvReaderService, IJsonReaderService jsonReaderService)
        {
            _config = config;
            _csvReaderService = csvReaderService;
            _jsonReaderService = jsonReaderService;
        }

        public async Task Run()
        {
            var audUsdExchangeRate = _config.GetSection("audUsdExchangeRate").Get<double>();
            var listings = new List<ListingVm>();

            var sources = _config.GetSection("sources").Get<List<FileSourceConfig>>();

            foreach (var source in sources)
            {
                switch (source.FileType)
                {
                    case FileType.CSV:
                        listings.AddRange(_csvReaderService.ReadFile(source.FileName, source.PriceInCents, source.Currency, audUsdExchangeRate));
                        break;
                    case FileType.JSON:
                        listings.AddRange(_jsonReaderService.ReadFile(source.FileName, source.PriceInCents, source.Currency, audUsdExchangeRate));
                        break;
                }
            }

            foreach (var listing in listings.OrderByDescending(x => x.PriceIdAud))
            {
                Console.WriteLine("{0}, {1}, {2}", listing.Id, listing.Description, listing.PriceIdAud);
            }
        }
    }
}
