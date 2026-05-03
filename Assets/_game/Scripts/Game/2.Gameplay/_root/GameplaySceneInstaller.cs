using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._2.Gameplay.Levels;
using _game.Scripts.Game._2.Gameplay.Levels.Service;
using _game.Scripts.Game._2.Gameplay.Levels.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Service;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay._root
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
                return _fallbackLevelSettings;
            }

            return null;
        }

        private void BindSettings(LevelSettings levelSettings)
        {
            Container.BindInstance(levelSettings).AsSingle();
            Container.BindInstance(levelSettings.MovementSettings).AsSingle();
        }

        private void BindControllers(LevelSettings levelSettings)
        {
            Container.Bind<RitualService>()
                .AsSingle()
                .WithArguments(levelSettings.LevelRitualPoolSettings);
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
