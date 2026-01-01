using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Obstacles;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.LevelSpecial
{
    public class FlashlightLevelTrigger : MonoBehaviour
    {
        [SerializeField] private Flashlight _flashlight;
        [SerializeField] private GameObject _darkness;

        private Score _riteManager;

        private void Awake()
        {
            _darkness.gameObject.SetActive(false);
            _flashlight.gameObject.SetActive(false);
        }

        [Inject]
        private void Initialize(Score riteManager)
        {
            _riteManager = riteManager;
        }

        private void OnEnable()
        {
            _riteManager.OnAmountOfCompletedRitualsChanged += HandleRitualsChanged;
        }

        private void OnDisable()
        {
            _riteManager.OnAmountOfCompletedRitualsChanged -= HandleRitualsChanged;
        }

        private void HandleRitualsChanged(int amount)
        {
            if (amount == 5)
            {
                TriggerEvent();
            }
        }

        private void TriggerEvent()
        {
            _darkness.gameObject.SetActive(true);
            _flashlight.gameObject.SetActive(true);

        }
    }
}
