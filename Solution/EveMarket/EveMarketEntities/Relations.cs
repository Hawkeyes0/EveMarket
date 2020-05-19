using Newtonsoft.Json;

namespace EveMarketEntities
{
    public class TypeAttribute
    {
        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        [JsonProperty("attribute_id")]
        public int AttributeId { get; set; }

        public float Value { get; set; }
    }

    public class TypeEffect
    {
        public int TypeId { get; set; }

        [JsonProperty("effect_id")]
        public int EffectId { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }
    }
}