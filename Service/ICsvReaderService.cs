using System.Collections.Generic;
using SuppliesPriceLister.Entity;
using SuppliesPriceLister.ViewModel;

namespace SuppliesPriceLister.Service
{
    public interface ICsvReaderService
    {
        List<ListingVm> ReadFile(string fileName, bool priceInCents, CurrencyType currency, double audUsdExchangeRate);
    }
}