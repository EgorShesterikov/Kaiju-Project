using UnityEngine;
using Zenject;

namespace Kaiju
{
    public abstract class StationBase : MonoBehaviour, IController
    {
        [Inject] protected readonly IHintController _hintController;

        [Inject] private readonly IInputController _inputController;

        private IController _player;

        public virtual void Enter(IController player)
        {
            _inputController.SetObjectControl(this);
            _player = player;
        }

        protected virtual void Exit()
        {
            _inputController.SetObjectControl(_player);
            _player = null;
        }

        public virtual void PressInstantVertical(float value)
        {
        }

        public virtual void PressInstantHorizontal(float value)
        {
        }

        public virtual void PressSpace(bool active)
        {
        }

        public virtual void PressE()
        {
            Exit();
        }
    }
}