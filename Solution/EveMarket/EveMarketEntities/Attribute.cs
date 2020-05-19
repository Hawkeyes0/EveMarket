using Newtonsoft.Json;

namespace EveMarketEntities
{
    public class Attribute : EveEntity
    {
        [JsonProperty("attribute_id")]
        public int AttributeId { get; set; }

        [JsonProperty("default_value")]
        public float DefaultValue { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("high_is_good")]
        public bool HighIsGood { get; set; }

        [JsonProperty("icon_id")]
        public int IconId { get; set; }

        public bool Published { get; set; }

        public bool Stackable { get; set; }

        [JsonProperty("unit_id")]
        public int UnitId { get; set; }

        public float Value { get; set; }
    }
}