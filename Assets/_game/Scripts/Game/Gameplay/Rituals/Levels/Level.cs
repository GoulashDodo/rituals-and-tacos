using System;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Root._Root;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels
{
    public class Level : IDisposable
    {
        private LevelSettings _levelSettings;
        private LevelLoader _levelLoader;

        private IWinCondition _winCondition;
        private ILoseCondition _loseCondition;
        private RitualService _ritualService;

        #region EVENTS

        public event Action OnLevelStarted;
        public event Action OnLevelWin;
        public event Action OnLevelLose;

        #endregion

        [Inject]
        private void Initialize(
            LevelSettings levelSettings,
            LevelLoader levelLoader,
            RitualService ritualService,
            [Inject(Id = "WinCondition")] IWinCondition winCondition,
            [Inject(Id = "LoseCondition")] ILoseCondition loseCondition)
        {
            _levelSettings = levelSettings;
            _levelLoader = levelLoader;
            _winCondition = winCondition;
            _loseCondition = loseCondition;
            _ritualService = ritualService;
            
            if (_winCondition != null)
            {
                _winCondition.OnConditionMet += Win;
            }

            if (_loseCondition != null)
            {
                _loseCondition.OnConditionMet += Lose;
            }
        }

        public void StartLevel()
        {
            PauseController.Instance.ResumeGame();
            _ritualService.SetRandomRite();
            
            OnLevelStarted?.Invoke();
            Debug.Log("Level started.");
        }

        private void Win()
        {
            PauseController.Instance.TogglePause();
            _levelSettings.MovementData.ResetSpeed();
            
            OnLevelWin?.Invoke();
            Debug.Log("Level won!");
        }

        private void Lose()
        {
            PauseController.Instance.TogglePause();
            _levelSettings.MovementData.ResetSpeed();

            OnLevelLose?.Invoke();
            Debug.Log("Level lost.");
        }

       
        public void Dispose()
        {
            if (_winCondition != null)
            {
                _winCondition.OnConditionMet -= Win;
            }
            
            if (_loseCondition != null)
            {
                _loseCondition.OnConditionMet -= Lose;
            }

        }
    }
}
