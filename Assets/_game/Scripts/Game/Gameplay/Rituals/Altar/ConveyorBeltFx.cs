using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Altar
{
    public class ConveyorBeltFx : MonoBehaviour
    {


        private AltarMovementData _altarMovementData;

        [SerializeField] private Transform[] _wayPoints;
        private int _currentWayPointIndex = 0;


        [Inject]
        private void Initialize(AltarMovementData altarMovementData)
        {
            _altarMovementData = altarMovementData;
        }

        private void OnEnable()
        {
            transform.position = _wayPoints[_currentWayPointIndex].position;
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
                    _currentWayPointIndex = 0;
                    transform.position = _wayPoints[_currentWayPointIndex].position;
                }
                else
                {
                    _currentWayPointIndex += 1;
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

    }
}
