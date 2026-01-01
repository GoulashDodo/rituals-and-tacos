using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UISpiritJar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void UpdateSliderUI(float value)
        {
            _slider.value = value;
        }
    }
}
