using System;

namespace EveMarketEntities
{
    public class Order
    {
        public int Duration { get; set; }

        public bool IsBuyOrder { get; set; }

        public DateTime Issued { get; set; }

        public int LocationId { get; set; }

        public int MinVolume { get; set; }

        public int OrderId { get; set; }

        public double Price { get; set; }

        public string Range { get; set; }

        public int SystemId { get; set; }

        public int TypeId { get; set; }

        public int VolumeRemain { get; set; }

        public int VolumeTotal { get; set; }
    }
}