using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
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
