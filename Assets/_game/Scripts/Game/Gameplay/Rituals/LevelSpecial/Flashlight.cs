using UnityEngine;
using UnityEngine.InputSystem;

namespace _game.Scripts.Game.Gameplay.Rituals.LevelSpecial
{
    public class Flashlight : MonoBehaviour
    {

        private void Update()
        {
            Vector3 newPosition = GetMouseWorldPosition();
            transform.position = newPosition;
        }


        private Vector3 GetMouseWorldPosition()
        {
            if (Camera.main == null)
            {
                Debug.LogError("Main camera not found!");
                return Vector3.zero;
            }

            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            return worldPosition;
        }


    }
}
