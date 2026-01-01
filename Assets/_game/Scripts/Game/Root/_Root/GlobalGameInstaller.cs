using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;

using _game.Scripts.Game.MainMenu.UI;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace _game.Scripts.Game.Root._Root
{
    public class GlobalGameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _fadeCanvasPrefab;

        [SerializeField] private AudioMixerGroup _soundFXMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;

        public override void InstallBindings()
        {
            BindControllers();
            BindPrefabs();
            BuildAudio();
            //BuildCursor();
        }

        private void BindControllers()
        {
            Container.Bind<GameInput>().AsSingle().NonLazy();
            Container.Bind<PauseService>().AsSingle().NonLazy();

        }
        

        private void BindPrefabs()
        {
            if (FindFirstObjectByType<FadeCanvas>() == null)
            {
                var fadeCanvasInstance = Container.InstantiatePrefab(_fadeCanvasPrefab);
                DontDestroyOnLoad(fadeCanvasInstance);
            }

            Container.Bind<FadeCanvas>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void BuildAudio()
        {
            SoundFXManager.Instance.SetMixerGroup(_soundFXMixerGroup);
            MusicManager.Instance.SetMixerGroup(_musicMixerGroup);
        }

        private void BuildCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
