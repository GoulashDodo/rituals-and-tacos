using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
{
    public class HandlerNextLevelButton : MonoBehaviour
    {
        
        [Inject] private ILevelLoader _levelLoader;


        public void LoadNextLevel()
        {
            _levelLoader.LoadNextLevel();
        }
        
        
    }
}