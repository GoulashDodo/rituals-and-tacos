using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Game.Gameplay.Rituals.UI.Rituals
{
    public class UIIndicator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Graphic _target; // Image, TMP, etc. (любая Graphic)

        [Header("Colors")]
        [SerializeField] private Color _onColor = Color.green;
        [SerializeField] private Color _offColor = Color.gray;

        [Header("Transitions")]
        [SerializeField, Min(0f)] private float _colorLerpDuration = 0.2f;

        [Header("Off Blink (Red <-> Black)")]
        [SerializeField] private bool _blinkWhenOff = true;
        [SerializeField] private Color _blinkColorA = Color.red;
        [SerializeField] private Color _blinkColorB = Color.black;
        [SerializeField, Min(0.01f)] private float _blinkHalfPeriod = 0.35f; // время до переключения цвета

        [Header("Startup State")]
        [SerializeField] private bool _startOn = false;

        private Coroutine _transitionRoutine;
        private Coroutine _blinkRoutine;
        private bool _isOn;

        private void Reset()
        {
            _target = GetComponent<Graphic>();
        }

        private void Awake()
        {
            if (_target == null)
                _target = GetComponent<Graphic>();

            _isOn = _startOn;
            ApplyImmediate(_isOn);
        }

        private void OnEnable()
        {
            // на всякий случай восстановим нужные корутины
            SetState(_isOn, instant: true);
        }

        private void OnDisable()
        {
            StopAllRoutines();
        }

        /// <summary>
        /// Включить/выключить индикатор.
        /// </summary>
        public void SetState(bool isOn, bool instant = false)
        {
            _isOn = isOn;

            StopAllRoutines();

            if (_target == null)
                return;

            if (instant || _colorLerpDuration <= 0f)
            {
                ApplyImmediate(isOn);
            }
            else
            {
                var targetColor = isOn ? _onColor : _offColor;
                _transitionRoutine = StartCoroutine(LerpToColor(_target.color, targetColor, _colorLerpDuration));
            }

            // Мигание только когда выключена
            if (!isOn && _blinkWhenOff)
            {
                // мигание должно стартовать после (возможного) перехода в offColor
                _blinkRoutine = StartCoroutine(BlinkLoop());
            }
        }

        /// <summary>
        /// Удобный метод для переключателя.
        /// </summary>
        public void Toggle(bool instant = false) => SetState(!_isOn, instant);

        private void ApplyImmediate(bool isOn)
        {
            if (_target == null) return;

            _target.color = isOn ? _onColor : _offColor;
        }

        private IEnumerator LerpToColor(Color from, Color to, float duration)
        {
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float k = Mathf.Clamp01(t / duration);
                _target.color = Color.Lerp(from, to, k);
                yield return null;
            }

            _target.color = to;
        }

        private IEnumerator BlinkLoop()
        {
            // начинаем с красного (можно поменять местами, если надо)
            Color current = _blinkColorA;

            while (!_isOn) // пока выключена
            {
                // плавно уходим в current
                if (_colorLerpDuration > 0f)
                    yield return LerpToColor(_target.color, current, _colorLerpDuration);
                else
                    _target.color = current;

                // держим половину периода
                float wait = 0f;
                while (wait < _blinkHalfPeriod && !_isOn)
                {
                    wait += Time.unscaledDeltaTime;
                    yield return null;
                }

                // переключаем цель
                current = (current == _blinkColorA) ? _blinkColorB : _blinkColorA;
            }
        }

        private void StopAllRoutines()
        {
            if (_transitionRoutine != null)
            {
                StopCoroutine(_transitionRoutine);
                _transitionRoutine = null;
            }

            if (_blinkRoutine != null)
            {
                StopCoroutine(_blinkRoutine);
                _blinkRoutine = null;
            }
        }
    }
}
