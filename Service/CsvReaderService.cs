using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Logging;
using SuppliesPriceLister.Entity;
using SuppliesPriceLister.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SuppliesPriceLister.Service
{
    public class CsvReaderService : ICsvReaderService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CsvReaderService> _logger;
        public CsvReaderService(IMapper mapper, ILogger<CsvReaderService> logger)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public List<ListingVm> ReadFile(string fileName, bool priceInCents, CurrencyType currency, double audUsdExchangeRate)
        {
            try
            {
                using var reader = new StreamReader(fileName);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var items = csv.GetRecords<HumphriesItem>().ToList();

                return items.Select(item => _mapper.Map<ListingVm>(item)).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new List<ListingVm>();
            }
        }
    }
}
