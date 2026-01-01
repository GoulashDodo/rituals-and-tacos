using _game.Scripts.Game.Gameplay.Rituals.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Spawner
{
    public class ItemSpawnerOnClick : ItemSpawner
    {
        private GameInput _gameInput;

        [Inject]
        private void Initialize(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        private void OnEnable()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.Gameplay.MousePressed.performed += MousePressed;
        }

        private void OnDisable()
        {
            _gameInput.Gameplay.MousePressed.performed -= MousePressed;
            _gameInput.Gameplay.Disable();
        }

        private void SpawnObject()
        {
            DraggableItem newItem = SpawnObject(GetMouseWorldPosition());

            if (newItem != null)
            {
                newItem.StartDragging();
            }
        }

        private void MousePressed(InputAction.CallbackContext context)
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMouseWorldPosition(), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                SpawnObject();
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null)
            {
                Debug.LogError("Main camera not found!");
                return Vector3.zero;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            return worldPosition;
        }
    }
}
