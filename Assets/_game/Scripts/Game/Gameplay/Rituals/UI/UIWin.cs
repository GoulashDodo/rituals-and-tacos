using _game.Scripts.Game.Gameplay.Rituals.Levels;
using UnityEditor.Search;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UIWin : MonoBehaviour
    {
        private Level _level;
        
        
        [Inject]
        public void Initialize(Level level)
        {
            _level = level;
            _level.Won += Show;
        }

        private void OnEnable()
        {
            if (_level != null)
            {
                _level.Won += Show; 
            }
        }
        private void OnDisable()
        {
            _level.Won -= Show;
        }
        
        

        private void Show()
        {
            gameObject.SetActive(true);
        }
        
    }
}
