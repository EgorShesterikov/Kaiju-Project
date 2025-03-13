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
            if (Input.GetKeyDown(KeyCode.A)) _currentController.PressA();
            if (Input.GetKeyDown(KeyCode.D)) _currentController.PressD();
            if (Input.GetKeyDown(KeyCode.W)) _currentController.PressW();
            if (Input.GetKeyDown(KeyCode.S)) _currentController.PressS();
            if (Input.GetKeyDown(KeyCode.E)) _currentController.PressE();
            if (Input.GetKeyDown(KeyCode.Space)) _currentController.PressSpace();
        }
    }
}