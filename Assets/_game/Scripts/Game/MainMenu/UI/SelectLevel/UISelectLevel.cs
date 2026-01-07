using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Gameplay.Rituals.Levels.Save;
using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.MainMenu.UI.SelectLevel
{
    public sealed class UISelectLevel : MonoBehaviour
    {
        [SerializeField] private UISelectLevelButton _buttonPrefab;

        private AllLevelSettings _levelSettings;
        private ILevelSaveService _levelSaveService;
        private ILevelLoader _levelLoader;

        [Inject]
        public void Construct(AllLevelSettings levelSettings, ILevelSaveService levelSaveService, ILevelLoader levelLoader)
        {
            _levelSettings = levelSettings;
            _levelSaveService = levelSaveService;
            _levelLoader = levelLoader;

            BuildButtons();
        }

        private void BuildButtons()
        {
            foreach (var levelSetting in _levelSettings.Levels)
            {
                var button = Instantiate(_buttonPrefab, transform);

                bool isUnlocked = _levelSaveService.IsUnlocked(levelSetting.TypeId);

                button.Initialize(_levelLoader, isUnlocked, levelSetting);
            }
        }
    }
}