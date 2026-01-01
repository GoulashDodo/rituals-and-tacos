using System;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers
{
    public class Health
    {

        #region FIELDS
        public int CurrentLivesAmount => _currentLivesAmount;

        private int _maxLivesAmount;

        private int _currentLivesAmount;

        private int _livesLostOnMiss;

        #endregion

        #region EVENTS

        public event Action<int> OnLivesAmountChanged;

        public event Action OnAllLivesLostInvoke;


        #endregion


        [Inject]
        private void Initialize(RitualService ritualService)
        {
            ritualService.OnRitualCompletedUnsuccessfully += rite => LoseLives(_livesLostOnMiss);
        }


        public Health(int initialValue, int livesLostOnMiss)
        {
            _maxLivesAmount = initialValue;

            _livesLostOnMiss = livesLostOnMiss;

            _currentLivesAmount = _maxLivesAmount;
        }

        public void AddLives(int livesAmount)
        {
            if(livesAmount <= 0)
            {
                throw new InvalidOperationException("Amount of lives is less than or equals zero");
            }

            if(_currentLivesAmount + livesAmount >= _maxLivesAmount)
            {
                _currentLivesAmount = _maxLivesAmount;
            }
            else
            {
                _currentLivesAmount += livesAmount;
            }


            OnLivesAmountChanged?.Invoke(_currentLivesAmount);


        }

        public void LoseLives(int livesAmount)
        {
            if (livesAmount <= 0)
            {
                throw new InvalidOperationException("Amount of lives is less than or equals zero");
            }

            if (_currentLivesAmount - livesAmount <= 0)
            {

                _currentLivesAmount = 0;

                OnAllLivesLostInvoke.Invoke();

            }
            else
            {
                _currentLivesAmount -= livesAmount;
            }

            OnLivesAmountChanged?.Invoke(_currentLivesAmount);

        }

    }
}
