namespace CapybaraRancher.Interfaces {
    internal interface IUpgradeManager
    {
        public bool[] Upgrades {get; set;}
        public int UpUpgrade();
        public int GetMoney();
    }
}
