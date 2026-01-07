using System.Collections;
using UnityEngine;

namespace _game.Scripts.Game.MainMenu.UI
{
    public class FadeCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public IEnumerator Fade(float targetAlpha, float duration)
        {
            float startAlpha = _canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
                yield return null;
            }

            _canvasGroup.alpha = targetAlpha;
        }

        public void SetAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }
    }
}
