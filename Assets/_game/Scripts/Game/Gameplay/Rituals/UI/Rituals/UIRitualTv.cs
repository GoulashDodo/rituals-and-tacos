using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI.Rituals
{
    public class UIRitualTv : MonoBehaviour
    {


        [SerializeField] private Animator _animator;
        
        [SerializeField] private Image _cassetteIcon;
        [SerializeField] private UIIndicator _uiIndicator;
        
        
        
        private RitualService _service;

        private bool _isHidden = true;
        
        
        [Inject]
        public void Initialize(RitualService service)
        {
            _service = service;
            _service.OnCurrentRitualChanged += OnRitualChange;
            _service.HasRightCassetteAdded += ChangeIndicatorColor;
        }

        private void OnEnable()
        {
            if (_service != null)
            {
                _service.OnCurrentRitualChanged += OnRitualChange;
                _service.HasRightCassetteAdded += ChangeIndicatorColor;
            }
        }
        
        private void OnDisable()
        {
            _service.OnCurrentRitualChanged -= OnRitualChange;
            _service.HasRightCassetteAdded -= ChangeIndicatorColor;

        }


        private void ChangeIndicatorColor(bool rightCassette)
        {
            _uiIndicator.SetState(rightCassette);
        }
        
  

        private void OnRitualChange(Ritual ritual)
        {
            var cassette = ritual.CassetteForRite;
            
            
            if (!cassette)
            {
                Hide();
                
            }
            else
            {
                Show();
                _cassetteIcon.sprite = cassette.Emoji;
                
            }

        }

        private void Hide()
        {
            if (_isHidden) return;
            
            _isHidden = true;
            _animator.SetTrigger("Hide");
        }


        private void Show()
        {
            if (!_isHidden) return;
            _isHidden = false;
            _animator.SetTrigger("Show");
        }
        
        
    }
}