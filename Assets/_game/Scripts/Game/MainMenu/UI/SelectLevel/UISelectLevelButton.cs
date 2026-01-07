using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Root.LevelLoading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Game.MainMenu.UI.SelectLevel
{
    public sealed class UISelectLevelButton : MonoBehaviour
    {
        [SerializeField] private Image _levelImage;
        [SerializeField] private TextMeshProUGUI _levelTitle;
        [SerializeField] private Button _button;

        private ILevelLoader _levelLoader;
        private string _levelTypeId;

        private bool _isInitialized;

        public void Initialize(ILevelLoader levelLoader, bool isUnlocked, LevelSettings levelSettings)
        {
            _levelLoader = levelLoader;
            _levelTypeId = levelSettings.TypeId;

            _button.interactable = isUnlocked;
            _levelTitle.text = levelSettings.Title;

            // Если нужно — выставляй спрайт
            // _levelImage.sprite = levelSettings.Sprite;

            if (!_isInitialized)
            {
                _button.onClick.AddListener(OnClicked);
                _isInitialized = true;
            }
        }

        private void OnDestroy()
        {
            if (_isInitialized)
            {
                _button.onClick.RemoveListener(OnClicked);
            }
        }

        private void OnClicked()
        {
            if (string.IsNullOrWhiteSpace(_levelTypeId))
            {
                Debug.LogError("UISelectLevelButton: LevelTypeId is empty.");
                return;
            }

            _levelLoader.LoadLevel(_levelTypeId);
        }
    }
}