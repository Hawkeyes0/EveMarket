namespace EveMarketEntities
{
    public class Effect
    {
        public int EffectId { get; set; }

        public bool DisallowAutoRepeat { get; set; }

        public int DischargeAttributeId { get; set; }

        public string DisplayName { get; set; }

        public int DurationAttributeId { get; set; }

        public int EffectCategory { get; set; }

        public bool ElectronicChance { get; set; }

        public int FalloffAttributeId { get; set; }

        public int IconId { get; set; }

        public bool IsAssistance { get; set; }

        public bool IsOffensive { get; set; }

        public bool IsWarpSafe { get; set; }

        public Modifier[] Modifiers { get; set; }

        public int PostExpression { get; set; }

        public int PreExpression { get; set; }

        public bool Published { get; set; }

        public int RangeAttributeId { get; set; }

        public bool RangeChance { get; set; }

        public int TrackingSpeedAttributeId { get; set; }
    }

    public class Modifier
    {
        public string Domain { get; set; }

        public int EffectId { get; set; }

        public string Func { get; set; }

        public int ModifiedAttributeId { get; set; }

        public int ModifyingAttributeId { get; set; }

        public int Operator { get; set; }
    }
}