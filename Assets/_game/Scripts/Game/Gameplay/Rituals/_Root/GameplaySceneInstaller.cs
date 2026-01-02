using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals._Root
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        
        [InlineEditor]
        [SerializeField] private LevelSettings _levelSettings;
   

        public override void InstallBindings()
        {
            BindSettings();
            BindControllers();
            BindStats();


        }

        private void BindSettings()
        {
            Container.BindInstance(_levelSettings).AsSingle();
            Container.BindInstance(_levelSettings.MovementData).AsSingle();

        }


        private void BindControllers()
        {
            Container.Bind<RitualService>()
                .AsSingle()
                .WithArguments(_levelSettings.LevelRitualPool);
        
        }

        private void BindStats()
        {
            Container.Bind<Score>()
                .AsSingle()
                .WithArguments(_levelSettings.TargetScoreCount);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(_levelSettings.AmountOfLivesOnLevel, _levelSettings.LivesLostOnMiss);
        }

    }
}
