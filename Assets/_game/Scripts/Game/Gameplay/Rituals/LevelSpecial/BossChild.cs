using System;
using _game.Scripts.Game.Gameplay.Rituals.Altar;
using _game.Scripts.Game.Gameplay.Rituals.Items;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.LevelSpecial
{
    public class BossChild : MonoBehaviour, IPlaceableSurface
    {
        public enum HungerLevel
        {
            Full,     // 0
            Hungry,   // 1
            Starving  // 2
        }

        public HungerLevel CurrentHungerLevel => _currentHungerLevel;

        [SerializeField] private float _timeToGetHungry = 5f;

        private float _timer;
        private HungerLevel _currentHungerLevel = HungerLevel.Full;

        private Level _level;
        
        public event Action<int> OnHungerChanged;
        public event Action OnFullStarvation;
        public event Action OnItemEaten;


        [Inject]
        public void Construct(Level level)
        {
            _level = level;
        }
        
        private void Start()
        {
            OnHungerChanged?.Invoke((int)_currentHungerLevel); 
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeToGetHungry)
            {
                _timer = 0f;
                IncreaseHunger();
            }
        }

        public void PlaceItem(GameObject itemGo, ItemData itemData)
        {
            if (itemData is FoodData foodData)
            {
                DecreaseHunger(foodData.HungerRestorationAmount);
                ResetTimer();
            }

            if (itemGo.TryGetComponent(out PlaceableItem item))
            {
                item.transform.SetParent(null);
                item.ReturnToPool();

                OnItemEaten?.Invoke();
            }

        }

        private void IncreaseHunger()
        {
            if (_currentHungerLevel < HungerLevel.Starving)
            {
                _currentHungerLevel++;
                OnHungerChanged?.Invoke((int)_currentHungerLevel); 
                Debug.Log($"Hunger level increased to: {_currentHungerLevel}");
            }
            else
            {
                Debug.Log("Hunger level reached full starvation!");
                OnFullStarvation?.Invoke();
                _level.EndLevel(LevelResult.Lose, "Feed the baby");
            }
        }

        private void DecreaseHunger(int amount)
        {
            if (_currentHungerLevel > HungerLevel.Full)
            {
                _currentHungerLevel -= amount;
                if (_currentHungerLevel < HungerLevel.Full)
                {
                    _currentHungerLevel = HungerLevel.Full;
                }

                OnHungerChanged?.Invoke((int)_currentHungerLevel);
                Debug.Log($"Hunger level decreased to: {_currentHungerLevel}");
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}
