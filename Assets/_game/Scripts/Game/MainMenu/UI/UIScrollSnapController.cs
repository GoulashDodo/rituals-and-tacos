using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _game.Scripts.Game.MainMenu.UI
{
    public class UIScrollSnapController : MonoBehaviour, IEndDragHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _snapSpeed = 12f;

        private RectTransform _viewport;
        private RectTransform _content;
        private RectTransform[] _items;

        private Coroutine _snapCoroutine;

        private void Awake()
        {
            _viewport = _scrollRect.viewport != null
                ? _scrollRect.viewport
                : (RectTransform)_scrollRect.transform;

            _content = _scrollRect.content;

            RebuildItems();
        }

        // Если контент динамический — дергай после изменений
        public void RebuildItems()
        {
            _items = new RectTransform[_content.childCount];
            for (int i = 0; i < _content.childCount; i++)
            {
                _items[i] = (RectTransform)_content.GetChild(i);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SnapToNearest();
        }

        public void SnapToNearest()
        {
            if (_items == null || _items.Length == 0)
                return;

            // Важно: погасить инерцию, иначе будет “борьба” со снапом
            _scrollRect.velocity = Vector2.zero;

            int nearestIndex = GetNearestItemIndex();
            Vector2 target = GetTargetContentPositionToCenterItem(_items[nearestIndex]);

            if (_snapCoroutine != null)
                StopCoroutine(_snapCoroutine);

            _snapCoroutine = StartCoroutine(SmoothSnap(target));
        }

        private int GetNearestItemIndex()
        {
            Vector3 viewportCenterWorld = _viewport.TransformPoint(_viewport.rect.center);

            float minDistance = float.MaxValue;
            int nearestIndex = 0;

            for (int i = 0; i < _items.Length; i++)
            {
                Vector3 itemCenterWorld = _items[i].TransformPoint(_items[i].rect.center);
                float distance = Mathf.Abs(viewportCenterWorld.x - itemCenterWorld.x);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }

        private Vector2 GetTargetContentPositionToCenterItem(RectTransform item)
        {
            Vector3 viewportCenterWorld = _viewport.TransformPoint(_viewport.rect.center);
            Vector3 itemCenterWorld = item.TransformPoint(item.rect.center);

            float deltaWorldX = viewportCenterWorld.x - itemCenterWorld.x;

            // переводим смещение в локальные координаты viewport
            float deltaLocalX = _viewport.InverseTransformVector(new Vector3(deltaWorldX, 0f, 0f)).x;

            return _content.anchoredPosition + new Vector2(deltaLocalX, 0f);
        }

        private IEnumerator SmoothSnap(Vector2 target)
        {
            while (Vector2.Distance(_content.anchoredPosition, target) > 0.1f)
            {
                _content.anchoredPosition = Vector2.Lerp(
                    _content.anchoredPosition,
                    target,
                    Time.deltaTime * _snapSpeed
                );

                yield return null;
            }

            _content.anchoredPosition = target;
        }
    }
}
