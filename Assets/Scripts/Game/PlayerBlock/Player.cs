using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class Player : MonoBehaviour, IController
    {
        [Inject] private readonly IInputController _inputController;

        public void PressA()
        {
            Debug.LogError("A");
        }

        public void PressD()
        {
            Debug.LogError("D");
        }

        public void PressW()
        {
            Debug.LogError("W");
        }

        public void PressS()
        {
            Debug.LogError("S");
        }

        public void PressSpace()
        {
            Debug.LogError("Space");
        }

        public void PressE()
        {
            Debug.LogError("E");
        }
    }
}