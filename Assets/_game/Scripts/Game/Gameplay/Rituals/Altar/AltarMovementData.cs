using System;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Altar
{
    [CreateAssetMenu(menuName = "Game/Altar/Movement Data")]
    public class AltarMovementData : ScriptableObject
    {

        public float MoveSpeed => _moveSpeed;



        [SerializeField] private float _initialSpeed;
        [SerializeField] private float _maxSpeed;


        [Range(0f, 0.5f)]
        [SerializeField] private float _accelerationMultiplier;


        private float _acceleratedSpeed;

        private float _moveSpeed;


        public Action<float> OnSpeedChanged;

        public void Accelerate()
        {
            if(_acceleratedSpeed + (_initialSpeed * _accelerationMultiplier) < _maxSpeed)
            {
                _acceleratedSpeed += _initialSpeed * _accelerationMultiplier;
            }
            else
            {
                _acceleratedSpeed = _maxSpeed;
            }

            SetAcceleratedSpeed();
        }

        public void SetMaxSpeed()
        {
            _moveSpeed = (_maxSpeed * 3 >=  15) ? _maxSpeed * 3 : 15;
            OnSpeedChanged?.Invoke(_moveSpeed/_initialSpeed);
        }

        public void SetAcceleratedSpeed()
        {
            _moveSpeed = _acceleratedSpeed;

            OnSpeedChanged?.Invoke(_moveSpeed/_initialSpeed);
        }


        public void ResetSpeed()
        {
            _acceleratedSpeed = _initialSpeed;
        }

        private void OnEnable()
        {

            ResetSpeed();
            _moveSpeed = _initialSpeed;
        }

        private void OnValidate()
        {
            if(_maxSpeed <= _initialSpeed)
            {
                _maxSpeed = _initialSpeed;
            }
        }
    }
}
