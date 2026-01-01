using System;
using _game.Scripts.Game.Gameplay.Rituals.Altar;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace _game.Scripts.Game.Gameplay.Rituals.Items
{
    public class PlaceableItem : DraggableItem
    {
        #region FIELDS

        public ItemData ItemData => _itemData;
        public bool IsPlaced => _isPlaced;

        [SerializeField] private ItemData _itemData;

        private bool _isPlaced;
        private IPlaceableSurface _nearestSurface;
        private ObjectPool<PlaceableItem> _pool;

        #endregion

        #region EVENTS

        public event Action OnItemDropped;
        public event Action OnItemPlaced;

        #endregion



        public void Initialize(ObjectPool<PlaceableItem> pool)
        {
            _pool = pool;
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
                ReturnToPool();
            }

            base.Drop();
        }

        public void ReturnToPool()
        {
            _isPlaced = false;

            if (_pool != null)
            {
                _pool.Release(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void PlaceOnSurface()
        {
            if (_nearestSurface == null)
            {
                return;
            }

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
    }
}
