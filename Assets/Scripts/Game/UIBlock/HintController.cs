using System;

namespace Kaiju
{
    public class HintController : IHintController
    {
        public event Action<HintControlData> OnSetTargetHintControl = delegate { };

        public void SetTargetHintControl(HintControlData hintControlData)
        {
            OnSetTargetHintControl.Invoke(hintControlData);
        }
    }
}