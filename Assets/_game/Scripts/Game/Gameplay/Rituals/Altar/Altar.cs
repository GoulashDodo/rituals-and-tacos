using System.Collections.Generic;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Items;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Altar
{
    public class Altar : MonoBehaviour, IPlaceableSurface
    {


        [SerializeField] private Transform[] _wayPoints;
        private int _currentWayPointIndex = 0;


        private RitualService _ritualService;

        private AltarMovementData _altarMovementData;


        [Inject]
        private void Initialize(RitualService ritualService, AltarMovementData altarMovementData)
        {
            _ritualService = ritualService;
            _altarMovementData = altarMovementData;


            _ritualService.OnRitualCompletedSuccessfully += rite => _altarMovementData.Accelerate();
            _ritualService.OnAllItemsAdded += _altarMovementData.SetMaxSpeed;
        }    

        public void PlaceItem(GameObject itemGO, ItemData itemData)
        {

            itemGO.transform.SetParent(gameObject.transform);

            _ritualService.AddItemToRite(itemData);
        }


        private void OnEnable()
        {
            if(_ritualService != null)
            {
                _ritualService.OnRitualCompletedSuccessfully += rite => _altarMovementData.Accelerate();
                _ritualService.OnAllItemsAdded += _altarMovementData.SetMaxSpeed;
            }

            transform.position = _wayPoints[_currentWayPointIndex].position;

        }

        private void OnDisable()
        {
            _ritualService.OnRitualCompletedSuccessfully -= (rite => _altarMovementData.Accelerate());
            _ritualService.OnAllItemsAdded -= (_altarMovementData.SetMaxSpeed);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _wayPoints[_currentWayPointIndex].position,
                _altarMovementData.MoveSpeed * Time.deltaTime
            );

            if (HasReachedWayPoint())
            {
                if (_currentWayPointIndex == _wayPoints.Length - 1)
                {
                    ResetAltar(); 
                }
                else
                {
                    _currentWayPointIndex++;
                }
            }
        }


        private bool HasReachedWayPoint()
        {
            if (Vector3.Distance(transform.position, _wayPoints[_currentWayPointIndex].position) <= 0.001f)
            {
                return true;
            }

            return false;
        }

        private void ResetAltar()
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            foreach (Transform child in children)
            {
                if (child.TryGetComponent(out PlaceableItem item))
                {
                    item.transform.SetParent(null);
                    item.ReturnToPool(); 
                }
                else
                {
                    Destroy(child.gameObject); 
                }
            }

            _ritualService.EndCurrentRitual();

            _currentWayPointIndex = 0;
            transform.position = _wayPoints[_currentWayPointIndex].position;
        }

    }
}