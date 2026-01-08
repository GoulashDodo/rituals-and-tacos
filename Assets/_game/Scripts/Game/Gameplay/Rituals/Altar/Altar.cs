using System.Collections.Generic;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Items;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Altar
{
    public class Altar : MonoBehaviour, IPlaceableSurface
    {
        [SerializeField] private Transform[] _wayPoints;

        private int _currentWayPointIndex;

        private RitualService _ritualService;
        private AltarMovementData _altarMovementData;
        private Level _level;

        private bool _isMoving;

        [Inject]
        private void Construct(
            RitualService ritualService,
            AltarMovementData altarMovementData,
            Level level)
        {
            _ritualService = ritualService;
            _altarMovementData = altarMovementData;
            _level = level;
            
            _level.Started += OnLevelStarted;
            _level.Won += OnLevelFinished;
            _level.Lost += OnLevelFinished;


        }

        private void OnEnable()
        {
            _currentWayPointIndex = 0;
            SetPositionToCurrentWayPoint();
            
            if (_level != null)
            {
                _level.Started += OnLevelStarted;
                _level.Won += OnLevelFinished;
                _level.Lost += OnLevelFinished;
            }

            _ritualService.OnRitualCompletedSuccessfully += OnRitualCompletedSuccessfully;
            _ritualService.OnAllItemsAdded += OnAllItemsAdded;
        }

        private void OnDisable()
        {
            if (_level != null)
            {
                _level.Started -= OnLevelStarted;
                _level.Won -= OnLevelFinished;
                _level.Lost -= OnLevelFinished;
            }

            if (_ritualService != null)
            {
                _ritualService.OnRitualCompletedSuccessfully -= OnRitualCompletedSuccessfully;
                _ritualService.OnAllItemsAdded -= OnAllItemsAdded;
            }
        }

        private void Update()
        {
            if (!_isMoving)
            {
                return;
            }

            Move();
        }

        public void PlaceItem(GameObject itemGo, ItemData itemData)
        {
            itemGo.transform.SetParent(transform, worldPositionStays: true);
            _ritualService.AddItemToRite(itemData);
        }

        private void OnLevelStarted()
        {
            _isMoving = true;
        }

        private void OnLevelFinished()
        {
            _isMoving = false;
        }

        private void OnRitualCompletedSuccessfully(Ritual rite)
        {
            _altarMovementData.Accelerate();
        }

        private void OnAllItemsAdded()
        {
            _altarMovementData.SetMaxSpeed();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _wayPoints[_currentWayPointIndex].position,
                _altarMovementData.MoveSpeed * Time.deltaTime);

            if (!HasReachedWayPoint())
            {
                return;
            }

            if (_currentWayPointIndex >= _wayPoints.Length - 1)
            {
                ResetAltar();
                return;
            }

            _currentWayPointIndex++;
        }

        private bool HasReachedWayPoint()
        {
            return Vector3.Distance(transform.position, _wayPoints[_currentWayPointIndex].position) <= 0.001f;
        }

        private void SetPositionToCurrentWayPoint()
        {
            if (_wayPoints == null || _wayPoints.Length == 0)
            {
                Debug.LogError("Altar: WayPoints array is empty.");
                return;
            }

            transform.position = _wayPoints[_currentWayPointIndex].position;
        }

        private void ResetAltar()
        {
            var children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            foreach (var child in children)
            {
                if (child.TryGetComponent(out PlaceableItem item))
                {
                    child.SetParent(null);
                    item.ReturnToPool();
                }
                else
                {
                    Destroy(child.gameObject);
                }
            }

            _ritualService.EndCurrentRitual();

            _currentWayPointIndex = 0;
            SetPositionToCurrentWayPoint();
        }
    }
}
