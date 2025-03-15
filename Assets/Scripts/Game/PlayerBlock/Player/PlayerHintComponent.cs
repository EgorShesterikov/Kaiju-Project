using UnityEngine;

namespace Kaiju
{
    public class PlayerHintComponent : MonoBehaviour
    {
        [SerializeField] private GameObject pressE_Hint;

        public void DisplayPressE_Hint(bool value)
        {
            pressE_Hint.SetActive(value);
        }
    }
}