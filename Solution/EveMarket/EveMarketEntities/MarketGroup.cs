using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EveMarketEntities
{
    public class MarketGroup : EveEntity
    {
        [JsonProperty("market_group_id")]
        public int MarketGroupId { get; set; }

        [JsonProperty("parent_group_id")]
        public int ParentGroupId { get; set; }

        public int[] Types { get; set; }

        public List<MarketGroup> Children { get; } = new List<MarketGroup>();
    }
}
