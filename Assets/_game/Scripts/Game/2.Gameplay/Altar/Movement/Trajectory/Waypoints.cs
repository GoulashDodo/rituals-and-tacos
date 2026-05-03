using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Altar.Movement.Trajectory
{
    public class Waypoints : MonoBehaviour
    {
        [field: SerializeField] public Transform[] Waypoint { get; private set; }
    }
}