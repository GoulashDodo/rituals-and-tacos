using System;
using _game.Scripts.Game._2.Gameplay.Altar;
using _game.Scripts.Game._2.Gameplay.Items.Settings;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items
{
    public class PlaceableItem : DraggableItem
    {
        public ItemSettings ItemSettings { get; private set; }
        
        public bool IsPlaced { get; private set; }
        
        private IPlaceableSurface _nearestSurface;

        public event Action OnItemDropped;
        public event Action OnItemPlaced;

        public void Initialize()
        {
            ResetState();
        }

        public override void Drag()
        {
            if (IsPlaced) return;
            base.Drag();
        }

        public override void Drop()
        {
            if (IsPlaced) return;

            if (_nearestSurface != null)
            {
                OnItemPlaced?.Invoke();
                PlaceOnSurface();
            }
            else
            {
                OnItemDropped?.Invoke();
                DisposeItem();

            }

            base.Drop();
            
        }

        public void DisposeItem()
        {
            ResetState();
            Destroy(gameObject);
        }

        private void ResetState()
        {
            IsPlaced = false;
            _nearestSurface = null;
        }

        private void PlaceOnSurface()
        {
            if (_nearestSurface == null) return;

            IsPlaced = true;
            _nearestSurface.PlaceItem(gameObject, ItemSettings);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlaceableSurface surface))
            {
                _nearestSurface = surface;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlaceableSurface surface) && surface == _nearestSurface)
            {
                _nearestSurface = null;
            }
        }

        private void OnDisable()
        {
            _nearestSurface = null;
        }
    }
}
