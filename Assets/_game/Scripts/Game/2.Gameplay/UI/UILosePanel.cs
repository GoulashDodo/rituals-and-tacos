using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using _game.Scripts.Game._2.Gameplay.Levels;
using TMPro;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.UI
{
    public class UILosePanel : MonoBehaviour
    {

        private Level _level;

        private ILevelLoader _levelLoader;

        [SerializeField] private TextMeshProUGUI _text;
        
        [Inject]
        private void Initialize(Level level, ILevelLoader levelLoader)
        {
            _level = level;
            _levelLoader = levelLoader;

            _level.Lost += OpenLosePanel;
        }

        private void OpenLosePanel(string loseReason)
        {
            gameObject.SetActive(true);
            _text.text = loseReason;
        }


    }
}
