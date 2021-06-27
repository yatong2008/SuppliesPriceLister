using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister.Entity
{
    public class Supply
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Uom Uom { get; set; }
        public int PriceInCents { get; set; }
        public Guid ProviderId { get; set; }
        public MaterialType? MaterialType { get; set; }
    }

    public enum MaterialType
    {
        Steel,
        Lumber,
        Concrete
    }

    public enum Uom
    {
        lm,
        m2,
        ea
    }
}
