using System.Collections;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UIWin : MonoBehaviour
    {
        [SerializeField] private Image _flashImage;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private CanvasGroup _winPanelCanvasGroup;

        [SerializeField, Min(0f)] private float _flashInDuration = 0.08f;

        [SerializeField, Min(0f)] private float _transitionDuration = 2f;

        [SerializeField, Min(0f)] private float _winPanelFadeInDuration = 0.35f;

        [SerializeField, Range(0f, 1f)] private float _slowdownTargetScale = 0.15f;

        private Level _level;
        private Coroutine _sequenceRoutine;
        private bool _shown;

        [Inject]
        public void Construct(Level level)
        {
            _level = level;
        }

        private void Awake()
        {
            if (_flashImage != null)
            {
                _flashImage.raycastTarget = false;
                SetFlashAlpha(0f);
            }

            if (_winPanel != null)
            {
                _winPanel.SetActive(false);
            }

            if (_winPanelCanvasGroup == null && _winPanel != null)
            {
                _winPanelCanvasGroup = _winPanel.GetComponent<CanvasGroup>();
            }

            if (_winPanelCanvasGroup != null)
            {
                _winPanelCanvasGroup.alpha = 0f;
                _winPanelCanvasGroup.interactable = false;
                _winPanelCanvasGroup.blocksRaycasts = false;
            }
        }

        private void OnEnable()
        {
            if (_level != null)
            {
                _level.Won += OnWon;
            }
        }

        private void OnDisable()
        {
            if (_level != null)
            {
                _level.Won -= OnWon;
            }
        }

        private void OnWon()
        {
            if (_shown) return;
            _shown = true;

            if (_sequenceRoutine != null)
            {
                StopCoroutine(_sequenceRoutine);
            }

            _sequenceRoutine = StartCoroutine(WinSequenceRoutine());
        }

        private IEnumerator WinSequenceRoutine()
        {
            if (PauseController.Instance.IsPaused)
            {
                PauseController.Instance.ResumeGame();
            }

            yield return FadeFlashRoutine(0f, 1f, _flashInDuration);

            yield return TransitionRoutine();

            PauseController.Instance.PauseGame();

            if (_winPanelCanvasGroup != null)
            {
                _winPanelCanvasGroup.interactable = true;
                _winPanelCanvasGroup.blocksRaycasts = true;
            }
        }

        private IEnumerator TransitionRoutine()
        {
            if (_transitionDuration <= 0f)
            {
                SetFlashAlpha(0f);
                Time.timeScale = Mathf.Clamp01(_slowdownTargetScale);

                ShowWinPanelInstant();
                yield break;
            }

            float startScale = Time.timeScale;
            float targetScale = Mathf.Clamp01(_slowdownTargetScale);

            ShowWinPanelForFade();

            float t = 0f;
            while (t < _transitionDuration)
            {
                t += Time.unscaledDeltaTime;

                float k = Mathf.Clamp01(t / _transitionDuration);
                float eased = Mathf.SmoothStep(0f, 1f, k);

                SetFlashAlpha(Mathf.Lerp(1f, 0.15f, eased));

                Time.timeScale = Mathf.Lerp(startScale, targetScale, eased);

                float panelK = GetPanelProgress(t);
                float panelEased = Mathf.SmoothStep(0f, 1f, panelK);
                SetWinPanelAlpha(panelEased);

                yield return null;
            }

            SetFlashAlpha(0.15f);
            Time.timeScale = targetScale;

            float finalPanelK = GetPanelProgress(_transitionDuration);
            float finalPanelEased = Mathf.SmoothStep(0f, 1f, finalPanelK);
            SetWinPanelAlpha(finalPanelEased);
        }

        private float GetPanelProgress(float elapsedUnscaled)
        {
            if (_winPanelFadeInDuration <= 0f)
            {
                return 1f;
            }

            float duration = Mathf.Min(_winPanelFadeInDuration, _transitionDuration);

            if (duration <= 0f)
            {
                return 1f;
            }

            return Mathf.Clamp01(elapsedUnscaled / duration);
        }

        private void ShowWinPanelForFade()
        {
            if (_winPanel == null) return;

            if (!_winPanel.activeSelf)
            {
                _winPanel.SetActive(true);
            }

            if (_winPanelCanvasGroup != null)
            {
                _winPanelCanvasGroup.alpha = 0f;
                _winPanelCanvasGroup.interactable = false;
                _winPanelCanvasGroup.blocksRaycasts = false;
            }
        }

        private void ShowWinPanelInstant()
        {
            if (_winPanel == null) return;

            _winPanel.SetActive(true);

            if (_winPanelCanvasGroup != null)
            {
                _winPanelCanvasGroup.alpha = 1f;
                _winPanelCanvasGroup.interactable = true;
                _winPanelCanvasGroup.blocksRaycasts = true;
            }
        }

        private void SetWinPanelAlpha(float a)
        {
            if (_winPanelCanvasGroup == null) return;

            _winPanelCanvasGroup.alpha = Mathf.Clamp01(a);
        }

        private IEnumerator FadeFlashRoutine(float from, float to, float duration)
        {
            if (_flashImage == null) yield break;

            if (duration <= 0f)
            {
                SetFlashAlpha(to);
                yield break;
            }

            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float k = Mathf.Clamp01(t / duration);
                SetFlashAlpha(Mathf.Lerp(from, to, k));
                yield return null;
            }

            SetFlashAlpha(to);
        }

        private void SetFlashAlpha(float a)
        {
            if (_flashImage == null) return;

            a = Mathf.Clamp01(a);
            var c = _flashImage.color;
            c.a = a;
            _flashImage.color = c;
        }
    }
}
