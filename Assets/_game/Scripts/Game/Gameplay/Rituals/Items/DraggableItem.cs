using _game.Scripts.Game.Root.Input.MouseClickable.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Items
{
    public class DraggableItem : MonoBehaviour, IDraggable, ILeftButtonPressable, ILeftButtonReleasable
    {
        [SerializeField] private LayerMask _draggableLayerMask;

        private Camera _mainCamera;
        private GameInput _gameInput;
        private Vector3 _offset;
        private bool _isDragging;

        [Inject]
        private void Construct(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        protected virtual void Awake()
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                Debug.LogError("Main camera not found!");
            }
        }



        protected virtual void Update()
        {
            if (_isDragging)
            {
                Drag();
            }
        }

        public void StartDragging()
        {
            _offset = transform.position - GetMouseWorldPosition();
            _isDragging = true;
        }

        public virtual void Drag()
        {
            transform.position = GetMouseWorldPosition() + _offset;
        }

        public virtual void Drop()
        {
            _isDragging = false;
        }


        private Vector3 GetMouseWorldPosition()
        {
            if (_mainCamera == null) return Vector3.zero;

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = transform.position.z;
            return worldPosition;
        }



        public void OnLeftButtonPressed(Vector3 mousePosition)
        {
            StartDragging();
        }

        public void OnLeftButtonReleased(Vector3 mousePosition)
        {
            Drop();
        }
    }
}
