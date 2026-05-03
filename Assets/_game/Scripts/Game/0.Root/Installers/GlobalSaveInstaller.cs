using _game.Scripts.Game._0.Root.Save;
using _game.Scripts.Game._2.Gameplay.Levels;
using _game.Scripts.Game._2.Gameplay.Levels.Save;
using _game.Scripts.Game._2.Gameplay.Levels.Save.Progression;
using _game.Scripts.Game._2.Gameplay.Levels.Settings;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._0.Root.Installers
{
    public sealed class GlobalSaveInstaller : MonoInstaller
    {
        [SerializeField] private AllLevelSettings _allLevels;

        public override void InstallBindings()
        {
            Container.Bind<IKeyValueStorage>()
                .To<PlayerPrefsStorage>()
                .AsSingle();

            Container.BindInstance(_allLevels)
                .AsSingle();

            Container.Bind<ILevelSaveService>()
                .To<LevelSaveService>()
                .AsSingle();

            Container.Bind<ILevelProgressionService>()
                .To<LevelProgressionService>()
                .AsSingle();
        }
    }
}