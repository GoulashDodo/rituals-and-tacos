using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Gameplay.Rituals.Levels.Save;
using _game.Scripts.Game.Gameplay.Rituals.Levels.Save.Progression;
using _game.Scripts.Game.Root.Save;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Root.Installers
{
    public sealed class SaveInstaller : MonoInstaller
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