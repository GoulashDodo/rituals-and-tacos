using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._1.MainMenu.UI.ButtonHandlers
{
    public sealed class HandlerRestartButton : MonoBehaviour
    {
        private ILevelLoader _levelLoader;

        [Inject]
        private void Construct(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public void RestartLevel()
        {
            _levelLoader.ReloadCurrentLevel();
        }
    }
}