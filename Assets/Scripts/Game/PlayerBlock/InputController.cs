using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class InputController : IInputController, ITickable
    {
        private IController _currentController;

        public void SetObjectControl(IController controller)
        {
            _currentController = controller;
            _currentController.StartControl();
        }

        public void Tick()
        {
            if (_currentController == null ) return;

            _currentController.PressInstantHorizontal(Input.GetAxis("InstantHorizontal"));
            _currentController.PressInstantVertical(Input.GetAxis("InstantVertical"));

            if (Input.GetKeyDown(KeyCode.E)) _currentController.PressE();

            if (Input.GetKeyDown(KeyCode.Space)) _currentController.PressSpace(true);
            if (Input.GetKeyUp(KeyCode.Space)) _currentController.PressSpace(false);
        }
    }
}