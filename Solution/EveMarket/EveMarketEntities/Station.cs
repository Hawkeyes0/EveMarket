namespace EveMarketEntities
{
    public class Station : EveEntity
    {
        public float MaxDockableShipVolume { get; set; }

        public string[] Services { get; set; }

        public int StationId { get; set; }

        public int SystemId { get; set; }

        public int TypeId { get; set; }
    }

    public class System : EveEntity
    {
        public int ConstellationId { get; set; }

        public string SecurityClass { get; set; }

        public float SecurityStatus { get; set; }

        public int SystemId { get; set; }
    }
}