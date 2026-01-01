using System;
using System.Collections.Generic;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers
{
    public class RitualService
    {
        public bool CurrentRitualIsFinished => _currentRitualIsFinished;

        private readonly RitualPool _ritualPool;
        private Ritual _currentRitual;
        private bool _currentRitualIsFinished = false;

        private readonly List<ItemData> _itemsInRite = new List<ItemData>();

        public event Action<Ritual> OnCurrentRitualChanged;
        public event Action<ItemData, Ritual> OnAllItemsOfOneTypeAdded;
        public event Action<ItemData, Ritual> OnItemReturnedToAltar;
        public event Action OnAllItemsAdded;
        public event Action<Ritual> OnRitualCompletedSuccessfully;
        public event Action<Ritual> OnRitualCompletedUnsuccessfully;

        // true  => правильная кассета добавлена в ритуал
        // false => кассета не добавлена / снята (или кассеты нет в ритуале — событие не дергаем)
        public event Action<bool> HasRightCassetteAdded;

        public RitualService(RitualPool ritualPool)
        {
            _ritualPool = ritualPool;
        }

        public void EndCurrentRitual()
        {
            if (CheckIfAllItemsAdded())
                OnRitualCompletedSuccessfully?.Invoke(_currentRitual);
            else
                OnRitualCompletedUnsuccessfully?.Invoke(_currentRitual);

            SetRandomRite();
        }

        public void SetRandomRite()
        {
            Ritual randomRitual = _ritualPool.GetRandomRite();
            SetRite(randomRitual);
        }

        private void SetRite(Ritual ritual)
        {
            _currentRitual = ritual;
            _currentRitualIsFinished = false;
            RefreshItemsInRite();

            // В начале нового ритуала кассета (если нужна) точно ещё не добавлена
            if (_currentRitual.CassetteForRite != null)
                HasRightCassetteAdded?.Invoke(false);

            OnCurrentRitualChanged?.Invoke(_currentRitual);
        }

        private void RefreshItemsInRite()
        {
            _itemsInRite.Clear();

            foreach (RitualComponent component in _currentRitual.Components)
            {
                for (int i = 0; i < component.Count; i++)
                    _itemsInRite.Add(component.RequiredItem);
            }

            var cassette = _currentRitual.CassetteForRite;
            if (cassette != null)
            {
                Debug.Log("Adding cassette");
                _itemsInRite.Add(cassette);
            }
        }

        public void AddItemToRite(ItemData item)
        {
            if (!_itemsInRite.Contains(item))
                return;

            // Событие дергаем только для кассеты, и только если кассета реально нужна в этом ритуале
            if (_currentRitual.CassetteForRite != null && item == _currentRitual.CassetteForRite)
                HasRightCassetteAdded?.Invoke(true);

            _itemsInRite.Remove(item);

            if (!_itemsInRite.Contains(item))
                OnAllItemsOfOneTypeAdded?.Invoke(item, _currentRitual);

            if (CheckIfAllItemsAdded())
            {
                _currentRitualIsFinished = true;
                OnAllItemsAdded?.Invoke();
            }
        }

        public void ReturnItemToAltar(ItemData item)
        {
            if (item == null) return;

            foreach (RitualComponent component in _currentRitual.Components)
            {
                if (component.RequiredItem == item)
                {
                    _itemsInRite.Add(item);
                    OnItemReturnedToAltar?.Invoke(item, _currentRitual);
                    return;
                }
            }

            if (item == _currentRitual.CassetteForRite)
            {
                _itemsInRite.Add(item);
                OnItemReturnedToAltar?.Invoke(item, _currentRitual);

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
