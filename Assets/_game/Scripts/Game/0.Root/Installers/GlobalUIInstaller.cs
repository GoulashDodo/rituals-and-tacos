using _game.Scripts.Game._1.MainMenu.UI;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._0.Root.Installers
{
    public class GlobalUIInstaller : MonoInstaller
    {
        [SerializeField] private FadeCanvas _fadeCanvasPrefab;


        public override void InstallBindings()
        {
            BindFadeCanvas();
        }
        
        private void BindFadeCanvas()
        {
            var fadeCanvas = Container.InstantiatePrefabForComponent<FadeCanvas>(_fadeCanvasPrefab);
            DontDestroyOnLoad(fadeCanvas.gameObject);

            Container.Bind<FadeCanvas>()
                .FromInstance(fadeCanvas)
                .AsSingle();
        }
        
    }
}