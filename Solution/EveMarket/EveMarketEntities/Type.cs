namespace EveMarketEntities
{
    public class Type : EveEntity
    {
        /// <summary>容量</summary>
        public float Capacity { get; set; }

        public Attribute[] DogmaAttributes { get; set; }

        public Effect[] DogmaEffects { get; set; }

        public int GroupId { get; set; }

        public int GraphicId { get; set; }

        public int IconId { get; set; }

        public int MarketGroupId { get; set; }

        public float Mass { get; set; }

        public float PackagedVolume { get; set; }

        public int PortionSize { get; set; }

        public bool Published { get; set; }

        public float Radius { get; set; }

        public int TypeId { get; set; }

        public float Volume { get; set; }
    }
}