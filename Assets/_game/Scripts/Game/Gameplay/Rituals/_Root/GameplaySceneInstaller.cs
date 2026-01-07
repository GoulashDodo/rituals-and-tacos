using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals._Root
{
    public sealed class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private SelectedLevelRuntime _selectedLevelRuntime;

        [SerializeField] private LevelSettings _fallbackLevelSettings; // опционально, для теста/сцены в редакторе

        public override void InstallBindings()
        {
            var levelSettings = ResolveLevelSettings();

            BindSettings(levelSettings);
            BindControllers(levelSettings);
            BindStats(levelSettings);
        }

        private LevelSettings ResolveLevelSettings()
        {
            if (_selectedLevelRuntime != null && _selectedLevelRuntime.CurrentLevelSettings != null)
            {
                return _selectedLevelRuntime.CurrentLevelSettings;
            }

            if (_fallbackLevelSettings != null)
            {
                Debug.LogWarning("SelectedLevelRuntime is empty. Using fallback LevelSettings.");
                return _fallbackLevelSettings;
            }

            Debug.LogError("GameplaySceneInstaller: LevelSettings not provided. " +
                           "Make sure LevelLoader set SelectedLevelRuntime before loading Gameplay.");
            return null;
        }

        private void BindSettings(LevelSettings levelSettings)
        {
            Container.BindInstance(levelSettings).AsSingle();
            Container.BindInstance(levelSettings.MovementData).AsSingle();
        }

        private void BindControllers(LevelSettings levelSettings)
        {
            Container.Bind<RitualService>()
                .AsSingle()
                .WithArguments(levelSettings.LevelRitualPool);
        }

        private void BindStats(LevelSettings levelSettings)
        {
            Container.Bind<Score>()
                .AsSingle()
                .WithArguments(levelSettings.TargetScoreCount);

            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(levelSettings.AmountOfLivesOnLevel, levelSettings.LivesLostOnMiss);
        }
    }
}
