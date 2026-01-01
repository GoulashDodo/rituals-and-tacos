using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Items
{
    public class DraggableItem : MonoBehaviour, IDraggable
    {
        [SerializeField] private LayerMask _draggableLayerMask;

        private UnityEngine.Camera _mainCamera;
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
            _mainCamera = UnityEngine.Camera.main;
            if (_mainCamera == null)
            {
                Debug.LogError("Main camera not found!");
            }
        }

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                return;
            }

            _gameInput.Gameplay.MousePressed.performed += MousePressed;
            _gameInput.Gameplay.MousePressed.canceled += MouseReleased;
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.MousePressed.performed -= MousePressed;
            _gameInput.Gameplay.MousePressed.canceled -= MouseReleased;
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

        private void MousePressed(InputAction.CallbackContext context)
        {
            if (TryGetNearestCollider(out var nearestCollider) && nearestCollider.gameObject == gameObject)
            {
                StartDragging();
            }
        }

        private void MouseReleased(InputAction.CallbackContext context)
        {
            if (TryGetNearestCollider(out var nearestCollider) && nearestCollider.gameObject == gameObject)
            {
                Drop();
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            if (_mainCamera == null) return Vector3.zero;

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = transform.position.z;
            return worldPosition;
        }

        private bool TryGetNearestCollider(out Collider2D nearestCollider)
        {
            Vector2 worldPosition = GetMouseWorldPosition();
            Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition, _draggableLayerMask);

            nearestCollider = null;
            float nearestDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                float distance = Vector2.Distance(worldPosition, collider.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestCollider = collider;
                }
            }

            return nearestCollider != null;
        }
    }
}
