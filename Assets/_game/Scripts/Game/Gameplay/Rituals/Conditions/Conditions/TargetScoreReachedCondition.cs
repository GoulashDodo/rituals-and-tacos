using System;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Conditions.Conditions
{
    public class TargetScoreReachedCondition : IWinCondition
    {
        public event Action OnConditionMet;

        [Inject]
        public void Initialize(Score score)
        {
            score.OnTargetScoreReached += OnConditionMet1;
        }

        private void OnConditionMet1()
        {
            Debug.Log("ConditionMet1");
            OnConditionMet?.Invoke();
        }
        
    }
}