using System;
using System.Collections.Generic;
using _game.Scripts.Game._2.Gameplay.Items.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Settings.Structures;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Rituals.Service
{
    public class RitualService
    {
        public bool CurrentRitualIsFinished => _currentRitualIsFinished;

        private readonly RitualPoolSettings _ritualPoolSettings;
        private RitualSettings _currentRitualSettings;
        private bool _currentRitualIsFinished = false;

        private readonly List<ItemSettings> _itemsInRite = new();

        public event Action<RitualSettings> OnCurrentRitualChanged;
        public event Action<ItemSettings, RitualSettings> OnAllItemsOfOneTypeAdded;
        public event Action<ItemSettings, RitualSettings> OnItemReturnedToAltar;
        public event Action OnAllItemsAdded;
        public event Action<RitualSettings> OnRitualCompletedSuccessfully;
        public event Action<RitualSettings> OnRitualCompletedUnsuccessfully;

        public event Action<bool> HasRightCassetteAdded;

        public RitualService(RitualPoolSettings ritualPoolSettings)
        {
            _ritualPoolSettings = ritualPoolSettings;
        }

        public void EndCurrentRitual()
        {
            if (CheckIfAllItemsAdded())
                OnRitualCompletedSuccessfully?.Invoke(_currentRitualSettings);
            else
                OnRitualCompletedUnsuccessfully?.Invoke(_currentRitualSettings);

            SetRandomRite();
        }

        public void SetRandomRite()
        {
            RitualSettings randomRitualSettings = _ritualPoolSettings.GetRandomRite();
            SetRite(randomRitualSettings);
        }

        private void SetRite(RitualSettings ritualSettings)
        {
            _currentRitualSettings = ritualSettings;
            _currentRitualIsFinished = false;
            RefreshItemsInRite();

            // В начале нового ритуала кассета (если нужна) точно ещё не добавлена
            if (_currentRitualSettings.CassetteForRite != null)
                HasRightCassetteAdded?.Invoke(false);

            OnCurrentRitualChanged?.Invoke(_currentRitualSettings);
        }

        private void RefreshItemsInRite()
        {
            _itemsInRite.Clear();

            foreach (RitualComponent component in _currentRitualSettings.Components)
            {
                for (int i = 0; i < component.Count; i++)
                    _itemsInRite.Add(component.RequiredItem);
            }

            var cassette = _currentRitualSettings.CassetteForRite;
            if (cassette != null)
            {
                Debug.Log("Adding cassette");
                _itemsInRite.Add(cassette);
            }
        }

        public void AddItemToRite(ItemSettings item)
        {
            if (!_itemsInRite.Contains(item))
                return;

            // Событие дергаем только для кассеты, и только если кассета реально нужна в этом ритуале
            if (_currentRitualSettings.CassetteForRite != null && item == _currentRitualSettings.CassetteForRite)
                HasRightCassetteAdded?.Invoke(true);

            _itemsInRite.Remove(item);

            if (!_itemsInRite.Contains(item))
                OnAllItemsOfOneTypeAdded?.Invoke(item, _currentRitualSettings);

            if (CheckIfAllItemsAdded())
            {
                _currentRitualIsFinished = true;
                OnAllItemsAdded?.Invoke();
            }
        }

        public void ReturnItemToAltar(ItemSettings item)
        {
            if (item == null) return;

            foreach (RitualComponent component in _currentRitualSettings.Components)
            {
                if (component.RequiredItem == item)
                {
                    _itemsInRite.Add(item);
                    OnItemReturnedToAltar?.Invoke(item, _currentRitualSettings);
                    return;
                }
            }

            if (item == _currentRitualSettings.CassetteForRite)
            {
                _itemsInRite.Add(item);
                OnItemReturnedToAltar?.Invoke(item, _currentRitualSettings);

                // Кассету вернули на алтарь => она больше не добавлена в ритуал
                HasRightCassetteAdded?.Invoke(false);
            }
        }

        private bool CheckIfAllItemsAdded()
        {
            return _itemsInRite.Count == 0;
        }
    }
}
