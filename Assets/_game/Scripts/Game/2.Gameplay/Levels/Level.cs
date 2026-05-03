using System;
using _game.Scripts.Game._2.Gameplay.Levels.Conditions.Interfaces;
using _game.Scripts.Game._2.Gameplay.Levels.Save.Progression;
using _game.Scripts.Game._2.Gameplay.Levels.Service;
using _game.Scripts.Game._2.Gameplay.Levels.Settings;
using _game.Scripts.Game._2.Gameplay.Levels.Structures;
using _game.Scripts.Game._2.Gameplay.Rituals.Service;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.Levels
{
    

    public  class Level : IInitializable, IDisposable
    {
        
        private readonly LevelSettings _levelSettings;
        private readonly RitualService _ritualService;
        private readonly PauseController _pauseController;
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
            [Inject(Id = "LoseCondition")] ILoseCondition loseCondition, PauseController pauseController)
        {
            _levelSettings = levelSettings;
            _ritualService = ritualService;
            _levelProgressionService = levelProgressionService;
            _winCondition = winCondition;
            _loseCondition = loseCondition;
            _pauseController = pauseController;

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
            _pauseController.ResumeGame();
            _ritualService.SetRandomRite();

            Started?.Invoke();
        }

        private void OnWinConditionMet() => EndLevel(LevelResult.Win);
        private void OnLoseConditionMet() => EndLevel(LevelResult.Lose);

        public void EndLevel(LevelResult result, string message = "")
        {
            

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