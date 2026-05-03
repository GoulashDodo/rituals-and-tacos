using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._1.MainMenu.UI
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
