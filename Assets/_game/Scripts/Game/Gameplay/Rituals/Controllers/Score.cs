using System;
using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using Zenject;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers
{
    public class Score
    {
        public int CurrentScore { get; private set; }
        public int CompletedRitualsCount { get; private set; }

        public float Progress01 => GetProgress01();

        private readonly int _targetScore;
        private bool _isTargetReached;

        #region Events

        public event Action<int> OnScoreChanged;
        public event Action OnTargetScoreReached;
        public event Action<int> OnAmountOfCompletedRitualsChanged;
        public event Action<float> Progress01Changed;

        #endregion

        public Score(int targetScore)
        {
            if (targetScore <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(targetScore));
            }

            _targetScore = targetScore;
        }

        [Inject]
        private void Initialize(RitualService ritualService)
        {
            ritualService.OnRitualCompletedSuccessfully += OnRitualCompleted;
        }

        private void OnRitualCompleted(Ritual ritual)
        {
            AddScoreBasedOnRitual(ritual);
            IncrementCompletedRites();
        }

        private bool HasReachedTarget()
        {
            return CompletedRitualsCount >= _targetScore;
        }

        public void Reset()
        {
            CurrentScore = 0;
            CompletedRitualsCount = 0;
            _isTargetReached = false;

            OnScoreChanged?.Invoke(CurrentScore);
            OnAmountOfCompletedRitualsChanged?.Invoke(CompletedRitualsCount);
            Progress01Changed?.Invoke(0f);
        }

        #region Internal Logic

        private void AddScoreBasedOnRitual(Ritual ritual)
        {
            AddScore((int)ritual.RiteDifficulty);
        }

        private void IncrementCompletedRites()
        {
            CompletedRitualsCount++;

            OnAmountOfCompletedRitualsChanged?.Invoke(CompletedRitualsCount);
            Progress01Changed?.Invoke(GetProgress01());

            TryNotifyTargetReached();
        }

        private void AddScore(int amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Amount must be greater than zero");
            }

            CurrentScore += amount;
            OnScoreChanged?.Invoke(CurrentScore);

            // Если цель = "набрать очки", а не "сделать ритуалы",
            // тогда можно проверять здесь. Сейчас оставляю универсально.
            TryNotifyTargetReached();
        }

        private void TryNotifyTargetReached()
        {
            if (_isTargetReached)
            {
                return;
            }

            if (!HasReachedTarget())
            {
                return;
            }

            _isTargetReached = true;
            OnTargetScoreReached?.Invoke();
        }

        private float GetProgress01()
        {
            return Mathf.Clamp01((float)CompletedRitualsCount / _targetScore);
        }

        #endregion
    }
}
