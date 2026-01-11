using System;
using _game.Scripts.Game.Gameplay.Rituals.Altar;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Items
{
    public class PlaceableItem : DraggableItem
    {
        public ItemData ItemData => _itemData;
        public bool IsPlaced => _isPlaced;

        [SerializeField] private ItemData _itemData;

        private bool _isPlaced;
        private IPlaceableSurface _nearestSurface;

        public event Action OnItemDropped;
        public event Action OnItemPlaced;

        // Оставил метод, чтобы сохранить общий контракт (вдруг где-то вызывается)
        public void Initialize()
        {
            ResetState();
        }

        public override void Drag()
        {
            if (_isPlaced) return;
            base.Drag();
        }

        public override void Drop()
        {
            if (_isPlaced) return;

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
            _isPlaced = false;
            _nearestSurface = null;
        }

        private void PlaceOnSurface()
        {
            if (_nearestSurface == null) return;

            _isPlaced = true;
            _nearestSurface.PlaceItem(gameObject, _itemData);
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
            // защита от "липкого" surface, если объект выключат/уничтожат в триггере
            _nearestSurface = null;
        }
    }
}
