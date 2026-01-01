using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UIPauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuUI;

        private PauseService _pauseService;

        [Inject]
        private void Initialize(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        private void OnEnable()
        {
            if (PauseController.Instance != null)
            {
                _pauseService.OnPauseMenuOpenRequested += ShowPauseMenu;
                _pauseService.OnPauseMenuCloseRequested += HidePauseMenu;
            }
            else
            {
                Debug.LogWarning("PauseController instance is missing");
            }
        }

        private void Awake()
        {
            HidePauseMenu();
        }

        private void OnDisable()
        {
            _pauseService.OnPauseMenuOpenRequested -= ShowPauseMenu;
            _pauseService.OnPauseMenuCloseRequested -= HidePauseMenu;
        }

        private void ShowPauseMenu()
        {
            _pauseMenuUI.SetActive(true);
        }

        private void HidePauseMenu()
        {
            _pauseMenuUI.SetActive(false);
        }
    }
}
