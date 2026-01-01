using System;

namespace _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces
{
    public interface IGameCondition
    {

        event Action OnConditionMet;

    }
}
