using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._1.MainMenu.UI.ButtonHandlers
{
    public class HandlerExitMainMenuButton : MonoBehaviour
    {

        [Inject] private ILevelLoader _levelLoader;

        public void LoadMainMenu()
        {
            _levelLoader.LoadMainMenu();
        }
    }
}
