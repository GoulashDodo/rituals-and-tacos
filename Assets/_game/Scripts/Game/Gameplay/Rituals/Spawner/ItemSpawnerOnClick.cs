using _game.Scripts.Game.Gameplay.Rituals.Items;
using _game.Scripts.Game.Root.Input.MouseClickable.Interfaces;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Spawner
{
    public sealed class ItemSpawnerOnClick : ItemSpawner, ILeftButtonPressable
    {
        public void OnLeftButtonPressed(Vector3 worldPoint)
        {
            Debug.Log("OnLeftButtonPressed");
            var hit = Physics2D.OverlapPoint(worldPoint);
            if (hit == null || hit.gameObject != gameObject)
            {
                return;
            }

            SpawnAndDrag(worldPoint);
        }

        private void SpawnAndDrag(Vector3 worldPoint)
        {
            DraggableItem newItem = SpawnObject(worldPoint);
            if (newItem != null)
            {
                newItem.StartDragging();
            }
        }
    }
}