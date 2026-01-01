using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.CinemachineAddons
{
    public class CinemachineShake : MonoBehaviour
    {
    
        private Health _health;

        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private float shakeTimer;

        [Inject]
        private void Intialize(Health health)
        {
            _health = health;

        }

        private void Awake()
        {

            TryGetComponent<CinemachineVirtualCamera>(out CinemachineVirtualCamera cinemachine);
            {
                _cinemachineVirtualCamera = cinemachine;
            }

            _health.OnLivesAmountChanged += livesAmount =>
            {
                if (livesAmount > 0)
                {
                    ShakeCamera();
                }
            };


        }

        private void Update()
        {
            if(shakeTimer > 0)
            {
                shakeTimer -= Time.unscaledDeltaTime;

                if(shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    shakeTimer = 0f;
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }

        private void ShakeCamera(float intensity = 3, float time = 0.3f)
        {

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }

    
    }
}
