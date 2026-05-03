using _game.Scripts.Game._2.Gameplay.Levels;
using _game.Scripts.Game._2.Gameplay.Levels.Conditions;
using _game.Scripts.Game._2.Gameplay.Levels.Conditions.Conditions;
using _game.Scripts.Game._2.Gameplay.Levels.Conditions.Interfaces;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay._root
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