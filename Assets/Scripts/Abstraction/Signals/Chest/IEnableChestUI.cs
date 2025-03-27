namespace CapybaraRancher.Abstraction.Signals.Chest
{
    public class IEnableChestUI
    {
        public bool isEnable;
        public IEnableChestUI(bool value)
        {
            isEnable = value;
        }
    }
}