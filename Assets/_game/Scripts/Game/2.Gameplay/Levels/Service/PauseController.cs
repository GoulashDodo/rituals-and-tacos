using System;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Levels.Service
{
    public class PauseController 
    {

        public bool IsPaused => _isPaused;

        public event Action GamePaused;
        public event Action GameResumed;

        private bool _isPaused = false;




        public void TogglePause()
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void PauseGame()
        {
            if (_isPaused) return;

            _isPaused = true;
            Time.timeScale = 0f;
            GamePaused?.Invoke();
        }

        public void ResumeGame()
        {
            if (!_isPaused) return;

            _isPaused = false;
            Time.timeScale = 1f;
            GameResumed?.Invoke();
        }
    }
}
