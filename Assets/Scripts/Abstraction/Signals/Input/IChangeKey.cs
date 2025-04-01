using UnityEngine;

namespace CapybaraRancher.Abstraction.Signals.Input
{
    public class IChangeKey
    {
        public KeyCode KeyCode;
        public IChangeKey(KeyCode value)
        {
            KeyCode = value;
        }
    }
}
