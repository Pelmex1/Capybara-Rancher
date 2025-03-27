namespace CapybaraRancher.Abstraction.Signals 
{
    public class ISetSaveName
    {
        public string Name;
        public ISetSaveName(string value)
        {
            Name = value;
        }
    }
}