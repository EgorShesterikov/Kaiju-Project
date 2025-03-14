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
            if (Input.GetKeyDown(KeyCode.A)) _currentController.PressA(true);
            if (Input.GetKeyUp(KeyCode.A)) _currentController.PressA(false);

            if (Input.GetKeyDown(KeyCode.D)) _currentController.PressD(true);
            if (Input.GetKeyUp(KeyCode.D)) _currentController.PressD(false);

            if (Input.GetKeyDown(KeyCode.W)) _currentController.PressW(true);
            if (Input.GetKeyUp(KeyCode.W)) _currentController.PressW(false);

            if (Input.GetKeyDown(KeyCode.S)) _currentController.PressS(true);
            if (Input.GetKeyUp(KeyCode.S)) _currentController.PressS(false);

            if (Input.GetKeyDown(KeyCode.E)) _currentController.PressE();
            if (Input.GetKeyDown(KeyCode.Space)) _currentController.PressSpace();
        }
    }
}