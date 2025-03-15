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
        }

        public void Tick()
        {
            _currentController.PressInstantHorizontal(Input.GetAxis("InstantHorizontal"));
            _currentController.PressInstantVertical(Input.GetAxis("InstantVertical"));

            if (Input.GetKeyDown(KeyCode.E)) _currentController.PressE();
            if (Input.GetKeyDown(KeyCode.Space)) _currentController.PressSpace();
        }
    }
}