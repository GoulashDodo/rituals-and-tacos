using System;
using _game.Scripts.Game._2.Gameplay.Items.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Service;
using _game.Scripts.Game._2.Gameplay.Rituals.Settings;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.Items.Spawner
{
    public class CassetteSpawner : MonoBehaviour
    {

        private RitualService _ritualService; 

        private int _currentTVShowIndex;

        [SerializeField] private CassetteSettings[] _cassettes;
        private CassetteSettings _currentCassette;

        public event Action<CassetteSettings> OnCurrentTVShowChanged;

        [Inject]
        private void Initialize(RitualService ritualService)
        {
            _ritualService = ritualService;
        }
        
        
        private void OnEnable()
        {
            if (_ritualService != null)
            {
                _ritualService.OnCurrentRitualChanged += HandleRitualChanged;
            }
        }

        private void OnDisable()
        {
            _ritualService.OnCurrentRitualChanged -= HandleRitualChanged;
        }

        private void HandleRitualChanged(RitualSettings ritualSettings)
        {
            if(_currentCassette != null)
            {
                _ritualService.AddItemToRite(_currentCassette);
            }
        }

        public void IncreaseCurrentTVShowIndex(int amount)
        {
            if(amount <= 0) 
            {
                throw new InvalidOperationException("Amount cannot be less than zero"); 
            }

            if(_currentTVShowIndex + amount >= _cassettes.Length)
            {
                _currentTVShowIndex = 0;
            }
            else
            {
                _currentTVShowIndex += amount;
            }

            ChangeCurrentTVShow();
        }

        public void DecreaseCurrentTVShowIndex(int amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Amount cannot be less than zero");
            }

            if(_currentTVShowIndex - amount < 0)
            {
                _currentTVShowIndex = _cassettes.Length - 1;
            }
            else
            {
                _currentTVShowIndex -= amount;
            }

            ChangeCurrentTVShow();
        }

        private void Start()
        {
            ChangeCurrentTVShow();
        }

        private void ChangeCurrentTVShow()
        {
            if(!_ritualService.CurrentRitualIsFinished) _ritualService.ReturnItemToAltar(_currentCassette);

            _currentCassette = _cassettes[_currentTVShowIndex];

            _ritualService.AddItemToRite(_currentCassette);

            if(_currentCassette == null)
            {
                throw new Exception("The cassettes are not set correctly!");
            }

            OnCurrentTVShowChanged?.Invoke(_currentCassette);
        }


    }
}
