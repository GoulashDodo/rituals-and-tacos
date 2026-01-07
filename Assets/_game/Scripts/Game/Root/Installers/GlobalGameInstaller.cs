using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.MainMenu.UI;
using _game.Scripts.Game.Root.Input.MouseClickable;
using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace _game.Scripts.Game.Root.Installers
{
    public sealed class GlobalGameInstaller : MonoInstaller
    {
        [SerializeField] private FadeCanvas _fadeCanvasPrefab;
        [SerializeField] private SelectedLevelRuntime _selectedLevelRuntime;

        
        
        [SerializeField] private AudioMixerGroup _soundFxMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;

        public override void InstallBindings()
        {
            BindControllers();
            BindFadeCanvas();
            BindAudio();
            
            Container.BindInstance(_selectedLevelRuntime).AsSingle();

            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();
            
        }

        private void BindControllers()
        {
            Container.Bind<GameInput>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MouseClickHandler>().AsSingle().NonLazy();
            Container.Bind<PauseService>().AsSingle().NonLazy();
        }

        private void BindFadeCanvas()
        {
            var fadeCanvas = Container.InstantiatePrefabForComponent<FadeCanvas>(_fadeCanvasPrefab);
            Object.DontDestroyOnLoad(fadeCanvas.gameObject);

            Container.Bind<FadeCanvas>()
                .FromInstance(fadeCanvas)
                .AsSingle();
        }

        private void BindAudio()
        {
            SoundFXManager.Instance.SetMixerGroup(_soundFxMixerGroup);
            MusicManager.Instance.SetMixerGroup(_musicMixerGroup);
        }
    }
}