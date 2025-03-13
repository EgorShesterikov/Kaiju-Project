using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class StationBase : MonoBehaviour, IController
    {
        [Inject] private readonly IInputController _inputController;

        public virtual void PressA()
        {
            
        }

        public virtual void PressD()
        {
            
        }

        public virtual void PressW()
        {
            
        }

        public virtual void PressS()
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