namespace CapybaraRancher.Interfaces
{
    internal interface IPlayer
    {
        public float Energy { get; set; }
        public float Health { get; set; }
        public float Hunger { get; set; }
        public float EnergyMaxValue { get; }
        public float HealthMaxValue { get; }
        public float HungerMaxValue { get; }
    }
}