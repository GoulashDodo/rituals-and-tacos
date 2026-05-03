using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._0.Root.LevelLoading.Interfaces;
using _game.Scripts.Game._2.Gameplay.Levels;
using _game.Scripts.Game._2.Gameplay.Levels.Save;
using _game.Scripts.Game._2.Gameplay.Levels.Settings;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._1.MainMenu.UI.SelectLevel
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