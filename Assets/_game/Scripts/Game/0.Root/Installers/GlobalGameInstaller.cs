using _game.Scripts.Game._0.Root.Input.MouseClickable;
using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using _game.Scripts.Game._2.Gameplay.Levels.Service;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace _game.Scripts.Game._0.Root.Installers
{
    public sealed class GlobalGameInstaller : MonoInstaller
    {
        [SerializeField] private SelectedLevelRuntime _selectedLevelRuntime;

        
        
        [SerializeField] private AudioMixerGroup _soundFxMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;

        public override void InstallBindings()
        {
            BindInput();
            
            Container.Bind<PauseController>().AsSingle().NonLazy();

            
            Container.BindInstance(_selectedLevelRuntime).AsSingle();

            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();
            
        }

        private void BindInput()
        {
            Container.Bind<GameInput>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MouseClickHandler>().AsSingle().NonLazy();
        }

        

    
    }
}