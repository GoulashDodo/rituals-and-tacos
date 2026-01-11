using _game.Scripts.Game.Gameplay.Rituals.Items;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Spawner
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private PlaceableItem _defaultPrefab;

        private DiContainer _container;
        private PlaceableItem _currentPrefab;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        protected virtual void Awake()
        {
            if (_defaultPrefab != null)
            {
                SwitchToPrefab(_defaultPrefab);
            }
        }

        public void SwitchToPrefab(PlaceableItem prefab)
        {
            if (prefab == null)
            {
                Debug.LogWarning("Provided prefab is null. Cannot switch.");
                return;
            }

            _currentPrefab = prefab;
        }

        public PlaceableItem SpawnObject(Vector2 position)
        {
            if (_currentPrefab == null)
            {
                Debug.LogWarning("No prefab selected. Cannot spawn.");
                return null;
            }

            // Zenject сам сделает inject, если он есть в компонентах префаба
            var item = _container.InstantiatePrefabForComponent<PlaceableItem>(_currentPrefab, transform);
            item.transform.position = position;

            // Если хочешь — можно прокинуть сюда контекст/спавнер (см. ниже)
            item.Initialize();

            return item;
        }
    }
}