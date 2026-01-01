using DG.Tweening;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Visual
{
    public sealed class BreathingEffect : MonoBehaviour
    {
        [SerializeField] private float _scaleMultiplier = 1.1f;
        [SerializeField] private float _breathingSpeed = 1.5f;
        [SerializeField] private bool _playOnStart = true;

        private Vector3 _originalScale;
        private Tween _breathingTween;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            if (_playOnStart)
            {
                StartBreathing();
            }
        }

        private void OnDisable()
        {
            StopBreathing();
        }

        public void StartBreathing()
        {
            if (_breathingTween != null && _breathingTween.IsActive())
            {
                return;
            }

            transform.localScale = _originalScale;

            _breathingTween = transform
                .DOScale(_originalScale * _scaleMultiplier, _breathingSpeed)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true); // если нужно работать при Time.timeScale = 0
        }

        public void StopBreathing()
        {
            if (_breathingTween == null)
            {
                return;
            }

            _breathingTween.Kill();
            _breathingTween = null;

            transform.localScale = _originalScale;
        }
    }
}