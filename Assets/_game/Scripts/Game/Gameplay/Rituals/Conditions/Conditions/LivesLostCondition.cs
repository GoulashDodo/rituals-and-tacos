using System;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Conditions.Conditions
{
    public class LivesLostCondition : ILoseCondition
    {

        public event Action OnConditionMet;

        [Inject]
        private void Initialize(Health health)
        {
            health.OnAllLivesLostInvoke += () => OnConditionMet?.Invoke();
        }

    }
}