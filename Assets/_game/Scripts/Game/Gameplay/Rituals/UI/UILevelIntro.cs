using System.Collections;
using TMPro;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UILevelIntro : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _fadeDuration = 0.5f;

        public IEnumerator Show(string message, float duration)
        {
            _text.text = message;

            yield return new WaitForSeconds(duration);
            yield return Fade(1f, 0f);
        }

        private IEnumerator Fade(float from, float to)
        {
            float time = 0f;

            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(from, to, time / _fadeDuration);
                yield return null;
            }

            _canvasGroup.alpha = to;
        }
    }
}