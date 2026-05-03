using System;

namespace _game.Scripts.Game._2.Gameplay.Levels.Conditions.Interfaces
{
    public interface IGameCondition
    {

        event Action OnConditionMet;

    }
}
