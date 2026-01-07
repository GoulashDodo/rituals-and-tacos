using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
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