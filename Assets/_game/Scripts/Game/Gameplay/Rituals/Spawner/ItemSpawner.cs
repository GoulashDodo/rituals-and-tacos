using System.Collections.Generic;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Items;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Spawner
{
    public class ItemSpawner : MonoBehaviour
    {
        private readonly Dictionary<PlaceableItem, ObjectPool<PlaceableItem>> _pools = new Dictionary<PlaceableItem, ObjectPool<PlaceableItem>>();
        private ObjectPool<PlaceableItem> _currentPool;

        [SerializeField] private int _initialSpawnedGoCount = 10;
        [SerializeField] private PlaceableItem _defaultPrefab;

        private bool _canSpawnObjects = true;
        private DiContainer _container; 

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

            PauseController.Instance?.OnGamePaused?.AddListener(() => _canSpawnObjects = false);
            PauseController.Instance?.OnGameResumed?.AddListener(() => _canSpawnObjects = true);
        }

        private void SwitchToPrefab(PlaceableItem prefab)
        {
            if (prefab == null)
            {
                Debug.LogWarning("Provided prefab is null. Cannot switch.");
                return;
            }

            if (_pools.TryGetValue(prefab, out var existingPool))
            {
                _currentPool = existingPool;
            }
            else
            {
                _currentPool = CreatePoolForPrefab(prefab);
            }
        }

        private ObjectPool<PlaceableItem> CreatePoolForPrefab(PlaceableItem prefab)
        {
            var newPool = new ObjectPool<PlaceableItem>(
                () => _container.InstantiatePrefabForComponent<PlaceableItem>(prefab, this.transform),
                item => item.gameObject.SetActive(true),
                item => item.gameObject.SetActive(false),
                maxSize: _initialSpawnedGoCount
            );

            _pools[prefab] = newPool;
            return newPool;
        }

        protected PlaceableItem SpawnObject(Vector2 position)
        {
            if (!_canSpawnObjects || _currentPool == null)
            {
                Debug.LogWarning("Cannot spawn objects. Either spawning is disabled or no pool is selected.");
                return null;
            }

            var item = _currentPool.Get();
            _container.Inject(item); 
            item.Initialize(_currentPool);
            item.transform.position = position;

            return item;
        }
    }
}


