using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        [Inject] private readonly IInputController _inputController;

        private void Awake()
        {
            _inputController.SetObjectControl(player);
        }
    }
}