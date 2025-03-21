using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;

        [Inject] private readonly IInputController _inputController;
        [Inject] private readonly IHintController _hintController;

        private void Start()
        {
            _inputController.SetObjectControl(player);
        }
    }
}