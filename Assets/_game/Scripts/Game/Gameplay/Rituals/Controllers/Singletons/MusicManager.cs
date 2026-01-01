using System.Collections;
using _game.Scripts.Common.Architecture;
using UnityEngine;
using UnityEngine.Audio;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons
{
    public class MusicManager : Singleton<MusicManager>
    {
        private AudioSource _musicSource;
        private AudioMixerGroup _mixerGroup;
        private const float FadeDuration = 1.5f;

        protected override void Awake()
        {
            base.Awake();

            if (_musicSource) return;
            
            
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
        }

        public void SetMixerGroup(AudioMixerGroup mixerGroup)
        {
            _mixerGroup = mixerGroup;
            if (_musicSource != null)
            {
                _musicSource.outputAudioMixerGroup = _mixerGroup;
            }
        }

        public void PlayMusic(AudioClip clip, float volume = 1.0f)
        {
            if (_musicSource.clip == clip) return; 

            StartCoroutine(FadeOutAndChangeMusic(clip, volume));
        }

        private IEnumerator FadeOutAndChangeMusic(AudioClip newClip, float targetVolume)
        {
            float startVolume = _musicSource.volume;
            for (float t = 0; t < FadeDuration; t += Time.deltaTime)
            {
                _musicSource.volume = Mathf.Lerp(startVolume, 0, t / FadeDuration);
                yield return null;
            }

            _musicSource.Stop();
            _musicSource.clip = newClip;
            _musicSource.Play();

            for (float t = 0; t < FadeDuration; t += Time.deltaTime)
            {
                _musicSource.volume = Mathf.Lerp(0, targetVolume, t / FadeDuration);
                yield return null;
            }

            _musicSource.volume = targetVolume;
        }
        
      
    }
}