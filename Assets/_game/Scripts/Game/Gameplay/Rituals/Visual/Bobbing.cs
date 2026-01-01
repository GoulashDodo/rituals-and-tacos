using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Visual
{
    public class Bobbing : MonoBehaviour
    {
        [SerializeField] private float _bobbingAmount = 0.1f; 
        [SerializeField] private float _bobbingSpeed = 2f; 

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            float newY = _startPosition.y + Mathf.Sin(Time.time * _bobbingSpeed) * _bobbingAmount;
            transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
        }
    }
}
