using Newtonsoft.Json;

namespace EveMarketEntities
{
    public abstract class EveEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}