namespace CapybaraRancher.Interfaces
{
    internal interface ICapybaraItem
    {
        public CapybaraData Data1 { get; set; }
        public CapybaraData Data2 { get; set; }
        public void Transformation();
    }
}