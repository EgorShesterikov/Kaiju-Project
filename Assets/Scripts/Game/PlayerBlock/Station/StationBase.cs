using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class StationBase : MonoBehaviour, IController
    {
        [Inject] private readonly IInputController _inputController;

        public virtual void PressA(bool active)
        {
            
        }

        public virtual void PressD(bool active)
        {
            
        }

        public virtual void PressW(bool active)
        {
            
        }

        public virtual void PressS(bool active)
        {
            
        }

        public virtual void PressSpace()
        {
            
        }

        public virtual void PressE()
        {
            
        }
    }
}