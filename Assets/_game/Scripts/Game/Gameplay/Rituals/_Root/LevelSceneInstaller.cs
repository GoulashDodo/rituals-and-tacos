using _game.Scripts.Game.Gameplay.Rituals.Conditions.Conditions;
using _game.Scripts.Game.Gameplay.Rituals.Conditions.Interfaces;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals._Root
{
    public class LevelSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.Bind<IWinCondition>()
                .WithId("WinCondition")
                .To<TargetScoreReachedCondition>()
                .AsTransient();

            Container.Bind<ILoseCondition>()
                .WithId("LoseCondition")
                .To<LivesLostCondition>()
                .AsTransient();

            Container.Bind<Level>().AsSingle().NonLazy();
        }
    }
}