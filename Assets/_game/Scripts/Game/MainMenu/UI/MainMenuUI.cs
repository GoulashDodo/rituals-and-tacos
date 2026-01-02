using _game.Scripts.Game.Root._Root;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField] private GameObject _selectLevelPanel;

        
        private LevelLoader _levelLoader;
        

        [Inject]
        private void Initialize(LevelLoader levelLoader)
        {

            _levelLoader = levelLoader;
        }
   
        public void OpenLevelSelectScreen()
        {
            _selectLevelPanel.SetActive(true);
        }
        
        

    }
}
