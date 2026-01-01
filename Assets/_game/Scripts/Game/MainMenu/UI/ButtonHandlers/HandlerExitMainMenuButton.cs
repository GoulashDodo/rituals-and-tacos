using _game.Scripts.Game.Root._Root;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
{
    public class HandlerExitMainMenuButton : MonoBehaviour
    {

        [Inject] private LevelLoader _levelLoader;

        public void LoadMainMenu()
        {
            _levelLoader.LoadMainMenu();
        }
    }
}
