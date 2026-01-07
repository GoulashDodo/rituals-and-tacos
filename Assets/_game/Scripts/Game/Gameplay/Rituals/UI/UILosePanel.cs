using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UILosePanel : MonoBehaviour
    {

        private Level _level;

        private ILevelLoader _levelLoader;

        [Inject]
        private void Initialize(Level level, ILevelLoader levelLoader)
        {
            _level = level;
            _levelLoader = levelLoader;

            _level.Lost += OpenLosePanel;
        }

        private void OpenLosePanel()
        {
            gameObject.SetActive(true);
        }


    }
}
