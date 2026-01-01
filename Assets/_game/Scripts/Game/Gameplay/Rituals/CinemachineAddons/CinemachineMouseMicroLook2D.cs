using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _game.Scripts.Game.Gameplay.Rituals.CinemachineAddons
{
    public sealed class CinemachineMouseMicroLook2D : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField, Range(0.01f, 2f)] private float _maxOffset = 0.35f; // в юнитах мира
        [SerializeField, Range(1f, 30f)] private float _smooth = 12f;

        private CinemachineFramingTransposer _framing;
        private Vector3 _currentOffset;

        private void Awake()
        {
            _framing = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (_framing == null)
            {
                Debug.LogError("CinemachineMouseMicroLook2D: На vcam нужен Body = Framing Transposer.");
            }
        }

        private void LateUpdate()
        {
            if (_framing == null) return;

            Vector2 mouse = Mouse.current.position.ReadValue();
            Vector2 screen = new Vector2(Screen.width, Screen.height);

            // Нормализуем в диапазон [-1..1], центр экрана = (0,0)
            Vector2 centered = (mouse / screen) * 2f - Vector2.one;

            // Чуть снизим чувствительность у краёв (мягкая кривая)
            centered = Vector2.ClampMagnitude(centered, 1f);
            centered = new Vector2(centered.x * Mathf.Abs(centered.x), centered.y * Mathf.Abs(centered.y));

            Vector3 targetOffset = new Vector3(centered.x, centered.y, 0f) * _maxOffset;

            _currentOffset = Vector3.Lerp(_currentOffset, targetOffset, 1f - Mathf.Exp(-_smooth * Time.deltaTime));
            _framing.m_TrackedObjectOffset = _currentOffset;
        }
    }
}