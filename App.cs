using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using SuppliesPriceLister.Entity;
using SuppliesPriceLister.ViewModel;

namespace SuppliesPriceLister
{
    public class App
    {
        private readonly IConfigurationRoot _config;
        private readonly ILogger<App> _logger;
        private readonly IMapper _mapper;

        public App(IConfigurationRoot config, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<App>();
            _config = config;
            _mapper = mapper;
        }

        public async Task Run()
        {
            //read humphries.csv
            var Listings = new List<ListingVm>();

            IEnumerable<HumphriesItem> humphriesItems;

            using (var reader = new StreamReader("humphries.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                humphriesItems = csv.GetRecords<HumphriesItem>().ToList();

                Listings.AddRange(humphriesItems.Select(humphriesItem => _mapper.Map<ListingVm>(humphriesItem)));
            }

            //read megacorp.json

            var payload = File.ReadAllText("megacorp.json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            var megacorpData = JsonSerializer.Deserialize<PartnersContainer>(payload, options);

            foreach (var megacorpDataPartner in megacorpData.Partners)
            {
                foreach (var supply in megacorpDataPartner.Supplies)
                {
                    Listings.Add(_mapper.Map<ListingVm>((supply, true, _config.GetSection("audUsdExchangeRate").Get<double>(), true)));
                }

                //Listings.AddRange(megacorpDataPartner.Supplies.Select(supply => new ListingVm()
                //{
                //    Id = supply.Id,
                //    Description = supply.Description,
                //    PriceIdAud = Math.Round(supply.PriceInCents / 100.0 / _config.GetSection("audUsdExchangeRate").Get<double>(), 2)
                //}));
            }

            foreach (var listing in Listings.OrderByDescending(x => x.PriceIdAud))
            {
                Console.WriteLine("{0}, {1}, {2}", listing.Id, listing.Description, listing.PriceIdAud);
            }
        }
    }
}
