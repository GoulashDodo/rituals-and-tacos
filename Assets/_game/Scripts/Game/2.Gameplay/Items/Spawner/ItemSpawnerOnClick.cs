using _game.Scripts.Game._0.Root.Input.MouseClickable.Interfaces;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items.Spawner
{
    public sealed class ItemSpawnerOnClick : ItemSpawner, ILeftButtonPressable
    {
        public void OnLeftButtonPressed(Vector3 worldPoint)
        {
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