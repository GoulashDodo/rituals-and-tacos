using _game.Scripts.Game.Root._Root;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {

        private LevelLoader _levelLoader;


        [Inject]
        private void Initialize(LevelLoader levelLoader)
        {

            _levelLoader = levelLoader;
        }
        
        public void Continue()
        {
            _levelLoader.LoadCurrentLevel();
        }
        

    }
}
