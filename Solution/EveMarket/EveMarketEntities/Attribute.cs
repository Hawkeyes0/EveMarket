namespace EveMarketEntities
{
    public class Attribute : EveEntity
    {
        public int AttributeId { get; set; }

        public float DefaultValue { get; set; }

        public string DisplayName { get; set; }

        public bool HighIsGood { get; set; }

        public int IconId { get; set; }

        public bool Published { get; set; }

        public bool Stackable { get; set; }

        public int UnitId { get; set; }
    }
}