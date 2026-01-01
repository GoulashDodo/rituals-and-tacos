using System.Collections;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.LevelSpecial
{
    public class BeerLevelTrigger : MonoBehaviour
    {

        [SerializeField] private Volume _volume;

        private Score _riteManager;

        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;
        private Bloom _bloom;
        private MotionBlur _motionBlur;

        [Inject]
        private void Initialize(Score riteManager)
        {
            _riteManager = riteManager;
        }

        private void Start()
        {
            if (_volume.profile.TryGet(out _vignette) &&
                _volume.profile.TryGet(out _chromaticAberration) &&
                _volume.profile.TryGet(out _bloom) &&
                _volume.profile.TryGet(out _motionBlur))
            {
                Debug.Log("Все постэффекты найдены.");
            }
            else
            {
                Debug.LogError("Некоторые постэффекты не найдены в VolumeProfile!");
            }
        }

        private void OnEnable()
        {
            _riteManager.OnAmountOfCompletedRitualsChanged += amount =>
            {
                if (amount == 5)
                {
                    ChangeProfile(0.15f, 0.3f, 0.5f, 0.3f);
                }
                if (amount == 10)
                {
                    ChangeProfile(0.2f, 0.5f, 0.8f, 0.5f);
                }
                if (amount == 15)
                {
                    ChangeProfile(0.3f, 0.8f, 1.2f, 0.7f);
                }
            };
        }

        private void ChangeProfile(float targetVignette, float targetChromatic, float targetBloom, float targetMotionBlur)
        {
            StartCoroutine(LerpEffects(targetVignette, targetChromatic, targetBloom, targetMotionBlur));
        }

        private IEnumerator LerpEffects(float targetVignette, float targetChromatic, float targetBloom, float targetMotionBlur)
        {
            float duration = 2.0f;
            float time = 0;

            float startVignette = _vignette.intensity.value;
            float startChromatic = _chromaticAberration.intensity.value;
            float startBloom = _bloom.intensity.value;
            float startMotionBlur = _motionBlur.intensity.value;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                _vignette.intensity.value = Mathf.Lerp(startVignette, targetVignette, t);
                _chromaticAberration.intensity.value = Mathf.Lerp(startChromatic, targetChromatic, t);
                _bloom.intensity.value = Mathf.Lerp(startBloom, targetBloom, t);
                _motionBlur.intensity.value = Mathf.Lerp(startMotionBlur, targetMotionBlur, t);

                yield return null;
            }
        }
    }
}
