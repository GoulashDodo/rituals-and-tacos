using _game.Scripts.Game.Gameplay.Rituals.Conditions;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Conditions;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Gameplay.Rituals.Levels.Save;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals._Root
{
    public sealed class LevelSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindConditions();
            BindLevel();
        }

        private void BindConditions()
        {
            Container.Bind<IWinCondition>()
                .WithId(ConditionIds.Win)
                .To<TargetScoreReachedCondition>()
                .AsSingle();

            Container.Bind<ILoseCondition>()
                .WithId(ConditionIds.Lose)
                .To<LivesLostCondition>()
                .AsSingle();
        }

        private void BindLevel()
        {
            Container.BindInterfacesAndSelfTo<Level>().AsSingle();

        }
    }
}