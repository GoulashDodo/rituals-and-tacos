using _game.Scripts.Common.Architecture;
using UnityEngine;
using UnityEngine.Events;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons
{
    public class PauseController : Singleton<PauseController>
    {

        public bool IsPaused => _isPaused;

        public UnityEvent OnGamePaused;
        public UnityEvent OnGameResumed;

        private bool _isPaused = false;

        protected override void Awake()
        {
            base.Awake();

            OnGamePaused ??= new UnityEvent();
            OnGameResumed ??= new UnityEvent();
        }


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
            OnGamePaused?.Invoke();
        }

        public void ResumeGame()
        {
            if (!_isPaused) return;

            _isPaused = false;
            Time.timeScale = 1f;
            OnGameResumed?.Invoke();
        }
    }
}
