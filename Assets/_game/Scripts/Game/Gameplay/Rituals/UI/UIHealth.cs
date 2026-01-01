using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using TMPro;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthTMP;

        private Health _health;


        [Inject]
        private void Initialize(Health livesAmount)
        {

            _health = livesAmount;
            _health.OnLivesAmountChanged += UpdateHealthUI;

            UpdateHealthUI(_health.CurrentLivesAmount);
        }

        private void UpdateHealthUI(int livesAmount)
        {
            _healthTMP.text = livesAmount.ToString();
        }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnLivesAmountChanged -= UpdateHealthUI;
            }
        }
    }
}
