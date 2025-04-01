namespace CapybaraRancher.Abstraction.Signals.UI
{

    public class IOnMoneyChanged
    {
        public float Money;
        public IOnMoneyChanged(int value){
            Money = value;
        }
    }
}