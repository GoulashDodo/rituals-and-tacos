using System;
using DG.Tweening;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Altar
{
    [Serializable]
    public sealed class MoveController : IMoveable
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _moveTime = 2f;
        [SerializeField] private bool _loop = true;
        [SerializeField] private bool _randomOrder = false;
        [SerializeField] private Ease _ease = Ease.Linear;

        private Transform _targetTransform;
        private int _currentIndex;
        private Tween _moveTween;

        public void Initialize(GameObject target)
        {
            if (target == null)
            {
                Debug.LogError("MoveController: Target is null.");
                return;
            }

            _targetTransform = target.transform;

            if (_waypoints == null || _waypoints.Length == 0)
            {
                Debug.LogError("MoveController: No waypoints set.");
                return;
            }

            StartMovement();
        }

        public void StartMovement()
        {
            if (_targetTransform == null || _waypoints == null || _waypoints.Length == 0)
            {
                return;
            }

            StopMovement();

            _currentIndex = 0;
            MoveToNextPoint();
        }

        public void StopMovement()
        {
            _moveTween?.Kill();
            _moveTween = null;
        }

        private void MoveToNextPoint()
        {
            if (_targetTransform == null || _waypoints == null || _waypoints.Length == 0)
            {
                return;
            }

            var targetPoint = _waypoints[_currentIndex];
            if (targetPoint == null)
            {
                // Пропускаем null-точки, чтобы не словить NRE
                AdvanceIndex();
                if (_currentIndex == -1) return;
                MoveToNextPoint();
                return;
            }

            _moveTween = _targetTransform
                .DOMove(targetPoint.position, _moveTime)
                .SetEase(_ease)
                .OnComplete(OnReachPoint);
        }

        private void OnReachPoint()
        {
            AdvanceIndex();
            if (_currentIndex == -1) return;

            MoveToNextPoint();
        }

        private void AdvanceIndex()
        {
            if (_randomOrder)
            {
                if (_waypoints.Length == 1)
                {
                    _currentIndex = 0;
                    return;
                }

                var nextIndex = UnityEngine.Random.Range(0, _waypoints.Length);
                if (nextIndex == _currentIndex)
                {
                    nextIndex = (nextIndex + 1) % _waypoints.Length;
                }

                _currentIndex = nextIndex;
                return;
            }

            _currentIndex++;

            if (_currentIndex < _waypoints.Length)
            {
                return;
            }

            if (_loop)
            {
                _currentIndex = 0;
                return;
            }

            _currentIndex = -1;
        }
    }
}
