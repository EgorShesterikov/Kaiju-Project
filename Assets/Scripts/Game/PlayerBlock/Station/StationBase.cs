using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class StationBase : MonoBehaviour, IController
    {
        [Inject] private readonly IInputController _inputController;

        private IController _enterController;

        public void Enter(IController player)
        {
            _inputController.SetObjectControl(this);
            _enterController = player;
        }

        private void Exit()
        {
            _inputController.SetObjectControl(_enterController);
            _enterController = null;
        }

        public virtual void PressInstantVertical(float value) { }

        public virtual void PressInstantHorizontal(float value) { }

        public virtual void PressSpace(bool active) { }

        public virtual void PressE()
        {
            Exit();
        }
    }
}