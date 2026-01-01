using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _game.Scripts
{
    public class AudioMixerController : MonoBehaviour
    {
        [Header("Audio Mixers")]
        [SerializeField] private AudioMixer _audioMixer;

        [Header("UI Elements")]
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        private const float MinVolume = -80f;
        private const float MaxVolume = 0f;

        private void Start()
        {
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

            _masterVolumeSlider.value = GetNormalizedVolume("MasterVolume");
            _musicVolumeSlider.value = GetNormalizedVolume("MusicVolume");
            _sfxVolumeSlider.value = GetNormalizedVolume("SFXVolume");
        }

        private void OnMasterVolumeChanged(float value) => SetVolume("MasterVolume", value);
        private void OnMusicVolumeChanged(float value) => SetVolume("MusicVolume", value);
        private void OnSFXVolumeChanged(float value) => SetVolume("SFXVolume", value);

        private void SetVolume(string parameterName, float normalizedValue)
        {
            float volume = Mathf.Log10(Mathf.Max(normalizedValue, 0.0001f)) * 20; 
            _audioMixer.SetFloat(parameterName, volume);
        }

        private float GetNormalizedVolume(string parameterName)
        {
            if (_audioMixer.GetFloat(parameterName, out float volume))
            {
                return Mathf.Pow(10, volume / 20f); 
            }
            return 1f; 
        }
    }
}
