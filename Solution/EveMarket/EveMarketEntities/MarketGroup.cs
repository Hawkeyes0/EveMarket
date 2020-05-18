using System;

namespace EveMarketEntities
{
    public class MarketGroup : EveEntity
    {
        public int MarketGroupId { get; set; }

        public int ParentGroupId { get; set; }

        public int[] types { get; set; }
    }
}
