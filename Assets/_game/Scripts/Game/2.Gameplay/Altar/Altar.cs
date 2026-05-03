using System.Collections.Generic;
using _game.Scripts.Game._2.Gameplay.Altar.Movement.Controller;
using _game.Scripts.Game._2.Gameplay.Altar.Movement.Trajectory;
using _game.Scripts.Game._2.Gameplay.Altar.Settings;
using _game.Scripts.Game._2.Gameplay.Items;
using _game.Scripts.Game._2.Gameplay.Items.Settings;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Altar
{
    public class Altar : MonoBehaviour, IPlaceableSurface
    {
        private bool _isMoving;

        private MovementWayPointController _controller;
        
        private void Construct(Waypoints waypoints, AltarMovementSettings altarMovementSettings)
        {
            _controller = new MovementWayPointController(transform, waypoints, altarMovementSettings);


        }



        private void Update()
        {
            if (_isMoving)
            {
                _controller.Move();
            }

        }

        public void PlaceItem(GameObject itemGo, ItemSettings itemSettings)
        {
            itemGo.transform.SetParent(transform, worldPositionStays: true);
        }
        
        private void CleanChildren()
        {
            var children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            foreach (var child in children)
            {
                if (child.TryGetComponent(out PlaceableItem item))
                {
                    child.SetParent(null);
                    item.DisposeItem();
                }
                else
                {
                    Destroy(child.gameObject);
                }
            }
            
        }
    }
}
