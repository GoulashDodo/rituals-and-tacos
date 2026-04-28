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
        private PauseController _pauseController;

        [Inject]
        private void Initialize(PauseService pauseService, PauseController pauseController)
        {
            _pauseService = pauseService;
            _pauseController = pauseController;
        }

        private void OnEnable()
        {
            _pauseService.OnPauseMenuOpenRequested += ShowPauseMenu;
            _pauseService.OnPauseMenuCloseRequested += HidePauseMenu;
            
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
            _pauseController.PauseGame();
            _pauseMenuUI.SetActive(true);
        }

        public void HidePauseMenu()
        {
            _pauseController.ResumeGame();
            _pauseMenuUI.SetActive(false);
        }
    }
}
