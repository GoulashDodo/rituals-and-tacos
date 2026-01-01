using System.Collections;
using _game.Scripts.Game.Gameplay.Rituals.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _game.Scripts.Game.Gameplay.Rituals.Spawner
{
    public class ItemSpawnerOnDrag : ItemSpawner
    {
        [Space(10)]
        [SerializeField] private Transform _targetPosition;

        [Header("Timer")]
        [SerializeField] private float _timeToCharge = 2f;
        [SerializeField] private float _timeToRestore = 1f;

        private float _currentTime;
        private bool _isCharged;
        private bool _isSpawning;
        private bool _isCharging;

        private PlaceableItem _spawnedItem;
        private Vector3 _previousMousePosition;
        private GameInput _gameInput;

        public UnityEvent<float> OnProgressChanged;

        protected override void Awake()
        {
            _gameInput = new GameInput();
            base.Awake();
        }

        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Gameplay.MousePosition.performed += OnMouseMoved;
            _gameInput.Gameplay.MousePressed.started += OnMouseDragStarted;
            _gameInput.Gameplay.MousePressed.canceled += OnMouseDragEnded;
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.MousePosition.performed -= OnMouseMoved;
            _gameInput.Gameplay.MousePressed.started -= OnMouseDragStarted;
            _gameInput.Gameplay.MousePressed.canceled -= OnMouseDragEnded;
            _gameInput.Disable();
        }

        private void Update()
        {
            if (!_isCharged || _spawnedItem == null) return;

            if (_spawnedItem.IsPlaced || !_spawnedItem.gameObject.activeSelf)
            {
                _spawnedItem = SpawnObject(_targetPosition.position);
            }
        }

        private void OnMouseMoved(InputAction.CallbackContext context)
        {
            Vector3 currentMousePosition = GetMouseWorldPosition();
            if (currentMousePosition != _previousMousePosition && !_isSpawning)
            {
                HandleChargeProgress();
                _previousMousePosition = currentMousePosition;
            }
        }

        private void OnMouseDragStarted(InputAction.CallbackContext context)
        {
            Vector2 worldPosition = GetMouseWorldPosition();
            Collider2D collider = Physics2D.OverlapPoint(worldPosition);

            if (collider != null && collider.gameObject == gameObject)
            {
                _isCharging = true;
            }
        }

        private void OnMouseDragEnded(InputAction.CallbackContext context)
        {
            _isCharging = false;
        }

        private void HandleChargeProgress()
        {
            if (_isCharged || !_isCharging) return;

            _currentTime += Time.deltaTime; 
            OnProgressChanged?.Invoke(_currentTime / _timeToCharge);

            if (_currentTime >= _timeToCharge)
            {
                CompleteCharge();
            }
        }

        private void CompleteCharge()
        {
            _isCharged = true;
            _isSpawning = true;


            if(_spawnedItem == null)
            {
                _spawnedItem = SpawnObject(_targetPosition.position);
            }
            StartCoroutine(RestoreCharge());
        }

        private IEnumerator RestoreCharge()
        {
            float elapsedTime = 0f;
            float startValue = _currentTime;

            while (elapsedTime < _timeToRestore)
            {
                _currentTime = Mathf.Lerp(startValue, 0, elapsedTime / _timeToRestore);
                elapsedTime += Time.deltaTime;

                OnProgressChanged?.Invoke(_currentTime / _timeToCharge);
                yield return null;
            }

            _currentTime = 0;
            _isCharged = false;
            _isSpawning = false;
            OnProgressChanged?.Invoke(0);
        }

        private Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null)
            {
                Debug.LogError("Main camera not found!");
                return Vector3.zero;
            }

            Vector2 mouseScreenPosition = _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();
            return Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0));
        }
    }
}
