using System;
using _game.Scripts.Game._2.Gameplay.Levels.Conditions.Interfaces;
using _game.Scripts.Game._2.Gameplay.Levels.Service;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.Levels.Conditions.Conditions
{
    public class LivesLostCondition : ILoseCondition, IInitializable, IDisposable
    {
        private readonly Health _health;

        public event Action OnConditionMet;

        public LivesLostCondition(Health health)
        {
            _health = health;
            Initialize();   
        }

        public void Initialize()
        {
            _health.OnAllLivesLostInvoke += OnAllLivesLost;
        }

        public void Dispose()
        {
            _health.OnAllLivesLostInvoke -= OnAllLivesLost;
        }

        private void OnAllLivesLost()
        {
            OnConditionMet?.Invoke();
        }
    }
}