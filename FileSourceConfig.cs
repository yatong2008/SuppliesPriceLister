using System;
using System.Collections.Generic;
using System.Text;
using SuppliesPriceLister.Entity;

namespace SuppliesPriceLister
{
    public class FileSourceConfig
    {
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public CurrencyType Currency { get; set; }
        public bool PriceInCents { get; set; }
    }
}
