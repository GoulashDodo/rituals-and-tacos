using System;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using TMPro;
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

        private PauseController _pauseController;
        
        public PauseService(PauseController pauseController)
        {
            _pauseController = pauseController;
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
           
                if (_pauseController.IsPaused)
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

                _pauseController.TogglePause();

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
