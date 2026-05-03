using System;
using _game.Scripts.Game._2.Gameplay.Levels.Conditions.Interfaces;
using _game.Scripts.Game._2.Gameplay.Levels.Service;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.Levels.Conditions.Conditions
{
    public class TargetScoreReachedCondition : IWinCondition, IInitializable, IDisposable
    {
        private readonly Score _score;

        public event Action OnConditionMet;

        public TargetScoreReachedCondition(Score score)
        {
            _score = score;
            Initialize();
        }

        public void Initialize()
        {
            _score.OnTargetScoreReached += OnTargetScoreReached;
        }

        public void Dispose()
        {
            _score.OnTargetScoreReached -= OnTargetScoreReached;
        }

        private void OnTargetScoreReached()
        {
            OnConditionMet?.Invoke();

        }
    }
}