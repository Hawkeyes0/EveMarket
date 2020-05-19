using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EveMarketEntities
{
    public class MarketGroup : EveEntity
    {
        [JsonProperty("market_group_id")]
        public int MarketGroupId { get; set; }

        [JsonProperty("parent_group_id")]
        public int ParentGroupId { get; set; }

        [JsonProperty("type")]
        [NotMapped]
        public int[] Types { get; set; }

        [JsonProperty("children")]
        [NotMapped]
        public List<MarketGroup> Children { get; } = new List<MarketGroup>();
    }
}
