using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class StationBase : MonoBehaviour, IController
    {
        [Inject] private readonly IInputController _inputController;

        private Player _player;
        private Vector3 _offsetPositionPlayer;

        public virtual void Enter(Player player)
        {
            _inputController.SetObjectControl(this);
            _player = player;
            _offsetPositionPlayer = _player.transform.position - transform.position;
        }

        protected virtual void Exit()
        {
            _inputController.SetObjectControl(_player);
            _player = null;
            _offsetPositionPlayer = Vector3.zero;
        }

        public virtual void PressInstantVertical(float value) { }

        public virtual void PressInstantHorizontal(float value) { }

        public virtual void PressSpace(bool active) { }

        public virtual void PressE()
        {
            Exit();
        }

        private void Update()
        {
            if (_player)
            {
                _player.transform.position = transform.position + _offsetPositionPlayer;
            }
        }
    }
}