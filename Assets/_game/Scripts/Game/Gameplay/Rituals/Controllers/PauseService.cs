using System;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers
{
    public class PauseService : IDisposable
    {
        public Action OnPauseMenuOpenRequested;
        public Action OnPauseMenuCloseRequested;

        private GameInput _gameInput;

        public PauseService()
        {
            Debug.Log("Pause Service initialized");


            _gameInput = new GameInput();
            _gameInput.Enable();

            

            if (_gameInput.UI.Pause != null)
            {
                _gameInput.UI.Pause.performed += HandlePauseInput;
            }
            else
            {
                Debug.LogError("Pause action not found in GameInput!");
            }
        }




        private void HandlePauseInput(InputAction.CallbackContext context)
        {
            if (PauseController.Instance != null)
            {
                if (!PauseController.Instance.IsPaused)
                {
                    _gameInput.Gameplay.Disable();

                    Debug.Log("Pause Menu Open Requested");
                    OnPauseMenuOpenRequested?.Invoke();
                }
                else
                {
                    _gameInput.Gameplay.Enable();

                    Debug.Log("Pause Menu Close Requested");
                    OnPauseMenuCloseRequested?.Invoke();
                }

                PauseController.Instance.TogglePause();
            }
            else
            {
                Debug.LogError("PauseController.Instance is null!");
            }
        }

        public void Dispose()
        {
            if (_gameInput != null)
            {
                _gameInput.UI.Pause.performed -= HandlePauseInput;
                _gameInput.Disable();
            }
        }
        
    }
}
