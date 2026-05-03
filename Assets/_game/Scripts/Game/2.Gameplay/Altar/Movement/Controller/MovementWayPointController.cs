using _game.Scripts.Game._2.Gameplay.Altar.Movement.Trajectory;
using _game.Scripts.Game._2.Gameplay.Altar.Settings;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Altar.Movement.Controller
{
    public class MovementWayPointController : IMovementController
    {
        private readonly Transform _parentTransform;
        private readonly Waypoints _waypoints;
        private readonly AltarMovementSettings _settings;
        
        private int _currentWaypointIndex;
        
        public MovementWayPointController(Transform parentTransform, Waypoints waypoints, AltarMovementSettings settings)
        {
            _parentTransform = parentTransform;
            _waypoints = waypoints;
            _settings = settings;
        }


        public void Move()
        {
            _parentTransform.position = Vector3.MoveTowards(
                _parentTransform.position,
                _waypoints.Waypoint[_currentWaypointIndex].position,
                _settings.InitialSpeed * Time.deltaTime);

            if (!HasReachedWayPoint())
            {
                return;
            }

            if (_currentWaypointIndex >= _waypoints.Waypoint.Length - 1)
            {
                _currentWaypointIndex = 0;
                SetPositionToCurrentWayPoint();
                return;
            }

            _currentWaypointIndex++;
        }
        
        private void SetPositionToCurrentWayPoint()
        {

            _parentTransform.position = _waypoints.Waypoint[_currentWaypointIndex].position;
        }
        
        private bool HasReachedWayPoint()
        {
            return Vector3.Distance(_parentTransform.position, _waypoints.Waypoint[_currentWaypointIndex].position) <= 0.001f;
        }
        
    }
}