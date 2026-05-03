using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Visual
{
    public class Rotator : MonoBehaviour
    {
        private enum RotationDirection
        {
            Clockwise,
            Counterclockwise
        }

        [SerializeField]
        private RotationDirection _rotationDirection = RotationDirection.Clockwise;

        [SerializeField]
        private float _rotationSpeed = 100f; 


        private void Update()
        {
            var rotationMultiplier = (_rotationDirection == RotationDirection.Clockwise) ? -1f : 1f;
            transform.Rotate(Vector3.forward, rotationMultiplier * _rotationSpeed * Time.deltaTime);
        }
    }
}
