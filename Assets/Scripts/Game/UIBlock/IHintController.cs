using System;

namespace Kaiju
{
    public interface IHintController
    {
        event Action<HintControlData> OnSetTargetHintControl;

        void SetTargetHintControl(HintControlData hintControlData);
    }
}