namespace CapybaraRancher.Abstraction.Signals
{
    public class IAddMoney
    {
        public float MoneyToAdd;
        public IAddMoney(float value)
        {
            MoneyToAdd = value;
        }
    }
}