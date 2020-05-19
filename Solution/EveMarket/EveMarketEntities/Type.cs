using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EveMarketEntities
{
    public class Type : EveEntity
    {
        /// <summary>容量</summary>
        public float Capacity { get; set; }

        [JsonProperty("dogma_attributes")]
        [NotMapped]
        public List<TypeAttribute> DogmaAttributes { get; set; }

        [JsonProperty("dogma_effects")]
        [NotMapped]
        public List<TypeEffect> DogmaEffects { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("graphic_id")]
        public int GraphicId { get; set; }

        [JsonProperty("icon_id")]
        public int IconId { get; set; }

        [JsonProperty("market_group_id")]
        public int MarketGroupId { get; set; }

        public float Mass { get; set; }

        [JsonProperty("packaged_volume")]
        public float PackagedVolume { get; set; }

        [JsonProperty("portion_size")]
        public int PortionSize { get; set; }

        public bool Published { get; set; }

        public float Radius { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        public float Volume { get; set; }
    }
}