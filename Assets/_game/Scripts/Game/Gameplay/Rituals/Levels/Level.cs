using System;
using _game.Scripts.Game.Gameplay.Rituals.Altar;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Levels.Save.Progression;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels
{
    public enum LevelResult
    {
        Win,
        Lose
    }

    public  class Level : IInitializable, IDisposable
    {
        
        private readonly LevelSettings _levelSettings;
        private readonly RitualService _ritualService;
        private readonly ILevelProgressionService _levelProgressionService;
        private readonly IWinCondition _winCondition;
        private readonly ILoseCondition _loseCondition;

        public event Action Started;
        public event Action Won;
        public event Action<string> Lost;

        public Level(
            LevelSettings levelSettings,
            RitualService ritualService,
            ILevelProgressionService levelProgressionService,
            [Inject(Id = "WinCondition")] IWinCondition winCondition,
            [Inject(Id = "LoseCondition")] ILoseCondition loseCondition)
        {
            _levelSettings = levelSettings;
            _ritualService = ritualService;
            _levelProgressionService = levelProgressionService;
            _winCondition = winCondition;
            _loseCondition = loseCondition;
            
            Initialize();
        }

        public void Initialize()
        {
            if (_winCondition != null) _winCondition.OnConditionMet += OnWinConditionMet;
            if (_loseCondition != null) _loseCondition.OnConditionMet += OnLoseConditionMet;
        }

        public void StartLevel()
        {
            //TODO: Change this
            PauseController.Instance.ResumeGame();
            _ritualService.SetRandomRite();

            Started?.Invoke();
        }

        private void OnWinConditionMet() => EndLevel(LevelResult.Win);
        private void OnLoseConditionMet() => EndLevel(LevelResult.Lose);

        public void EndLevel(LevelResult result, string message = "")
        {
            
            //TODO: Change this
            PauseController.Instance.PauseGame();
            _levelSettings.MovementData.ResetSpeed();

            if (result == LevelResult.Win)
            {
                _levelProgressionService.UnlockNextLevel(_levelSettings.TypeId);
                Won?.Invoke();
                return;
            }

            Lost?.Invoke(message);
            
        }

        public void Dispose()
        {
            if (_winCondition != null) _winCondition.OnConditionMet -= OnWinConditionMet;
            if (_loseCondition != null) _loseCondition.OnConditionMet -= OnLoseConditionMet;
        }
    }
}