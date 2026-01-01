using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UIProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _timeToChange = 0.25f;
        
        private Score _score;

        private Tween _tween;
        
        [Inject]
        public void Initialize(Score score)
        {
            _score = score;
            _score.Progress01Changed += OnProgressChanged;

            OnProgressChanged(_score.Progress01);
        }

        private void OnDestroy()
        {
            if (_score != null)
            {
                _score.Progress01Changed -= OnProgressChanged;
            }
        }

        private void OnProgressChanged(float target)
        {
            target = Mathf.Clamp01(target);

            _tween?.Kill();

            if (_timeToChange <= 0f)
            {
                _slider.value = target;
                return;
            }

            _tween = _slider.DOValue(target, _timeToChange)
                .SetEase(Ease.InQuad);
        }
    }
}