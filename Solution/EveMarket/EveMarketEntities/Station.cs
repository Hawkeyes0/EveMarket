using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EveMarketEntities
{
    public class Station : EveEntity
    {
        [JsonProperty("max_dockable_ship_volume")]
        public float MaxDockableShipVolume { get; set; }

        [NotMapped]
        public string[] Services { get; set; }

        [JsonProperty("station_id")]
        public int StationId { get; set; }

        [JsonProperty("system_id")]
        public int SystemId { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }

    public class System : EveEntity
    {
        [JsonProperty("constellation_id")]
        public int ConstellationId { get; set; }

        [JsonProperty("security_class")]
        public string SecurityClass { get; set; }

        [JsonProperty("security_status")]
        public float SecurityStatus { get; set; }

        [JsonProperty("system_id")]
        public int SystemId { get; set; }

        [NotMapped]
        public int[] Stations { get; set; }
    }

    public class Constellation : EveEntity
    {
        [JsonProperty("constellation_id")]
        public int ConstellationId { get; set; }

        [JsonProperty("region_id")]
        public int RegionId { get; set; }

        [NotMapped]
        public int[] Systems { get; set; }
    }

    public class Region : EveEntity
    {
        [JsonProperty("region_id")]
        public int RegionId { get; set; }

        [NotMapped]
        public int[] Constellations { get; set; }
    }
}