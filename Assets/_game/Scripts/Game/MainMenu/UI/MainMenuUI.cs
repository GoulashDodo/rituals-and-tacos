using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI
{
    public class MainMenuUI : MonoBehaviour
    {

        [SerializeField] private GameObject _selectLevelPanel;

        
        private ILevelLoader _levelLoader;
        

        [Inject]
        private void Initialize(ILevelLoader levelLoader)
        {

            _levelLoader = levelLoader;
        }
   
        public void OpenLevelSelectScreen()
        {
            _selectLevelPanel.SetActive(true);
        }
        
        

    }
}
