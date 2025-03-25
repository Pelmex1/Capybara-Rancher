namespace CapybaraRancher.Interfaces
{
    internal interface ICrystallController
    {
        public float DelayBeforeCrystalSpawn { get; set; }
        public float DelayBeforeStarving { get; set; }
        public int StartCrystall { get; set; }
    }
}