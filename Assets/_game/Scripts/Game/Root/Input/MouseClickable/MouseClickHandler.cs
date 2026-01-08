using System;
using _game.Scripts.Game.Root.Input.MouseClickable.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _game.Scripts.Game.Root.Input.MouseClickable
{
    public sealed class MouseClickHandler : IInitializable, IDisposable
    {
        private readonly GameInput _gameInput;

        private Camera _camera;

        public MouseClickHandler(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        public void Initialize()
        {

            _camera = Camera.main;


            
            _gameInput.Gameplay.Enable(); 

            
            _gameInput.Gameplay.MousePressed.performed += HandleLeftMousePressed;
            _gameInput.Gameplay.MousePressed.canceled += HandleLeftMouseReleased;
        }

        public void Dispose()
        {
            _gameInput.Gameplay.MousePressed.performed -= HandleLeftMousePressed;
            _gameInput.Gameplay.MousePressed.canceled -= HandleLeftMouseReleased;
            
            _gameInput.Gameplay.Disable();
        }

        private void HandleLeftMousePressed(InputAction.CallbackContext context)
        {
            
            if (!TryGetMouseWorldPoint(out var worldPoint))
            {
                return;
            }

            var hit = Physics2D.OverlapPoint(worldPoint);
            if (hit == null)
            {
                return;
            }

            if (hit.TryGetComponent<ILeftButtonPressable>(out var clickable))
            {
                clickable.OnLeftButtonPressed(worldPoint);
            }
        }

        private void HandleLeftMouseReleased(InputAction.CallbackContext context)
        {
            
            if (!TryGetMouseWorldPoint(out var worldPoint))
            {
                return;
            }

            var hit = Physics2D.OverlapPoint(worldPoint);
            if (hit == null)
            {
                return;
            }

            if (hit.TryGetComponent<ILeftButtonReleasable>(out var clickable))
            {
                clickable.OnLeftButtonReleased(worldPoint);
            }
        }

        private bool TryGetMouseWorldPoint(out Vector3 worldPoint)
        {
            if (_camera == null)
            {
                _camera = Camera.main;
                if (_camera == null)
                {
                    worldPoint = default;
                    return false;
                }
            }

            var screenPosition = Mouse.current.position.ReadValue();
            worldPoint = _camera.ScreenToWorldPoint(screenPosition);
            worldPoint.z = 0f;
            return true;
        }
    }
}
