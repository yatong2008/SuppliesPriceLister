using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister.Entity
{
    public class HumphriesItem
    {
        public Guid identifier { get; set; }
        public string desc { get; set; }
        public Uom unit { get; set; }
        public double costAUD { get; set; }

    }
}
