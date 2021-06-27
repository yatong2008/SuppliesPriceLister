using System.Collections.Generic;

namespace SuppliesPriceLister.Entity
{
    public class Partner
    {
        public string Name { get; set; }
        public string PartnerType { get; set; }
        public string PartnerAddress { get; set; }
        public List<Supply> Supplies { get; set; }
    }

    public class PartnersContainer
    {
        public List<Partner> Partners { get; set; }
    }
}
