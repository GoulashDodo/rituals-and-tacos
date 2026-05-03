using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._1.MainMenu.UI.ButtonHandlers
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