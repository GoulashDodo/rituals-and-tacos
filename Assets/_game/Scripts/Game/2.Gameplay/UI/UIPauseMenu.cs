using _game.Scripts.Game._2.Gameplay.Levels.Service;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.UI
{
    public class UIPauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenuUI;

        private PauseController _pauseService;
        private PauseController _pauseController;

        [Inject]
        private void Initialize(PauseController pauseService, PauseController pauseController)
        {
            _pauseService = pauseService;
            _pauseController = pauseController;
        }

        private void OnEnable()
        {
            
            
        }

        private void Awake()
        {
            HidePauseMenu();
        }

        private void OnDisable()
        {
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
