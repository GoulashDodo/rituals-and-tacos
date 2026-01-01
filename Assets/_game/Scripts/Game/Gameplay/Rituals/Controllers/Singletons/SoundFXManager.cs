using System.Collections;
using System.Collections.Generic;
using _game.Scripts.Common.Architecture;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons
{
    public class SoundFXManager : Singleton<SoundFXManager>
    {
        [SerializeField] private int _poolSize = 30;

        private AudioMixerGroup _mixerGroup;

        private ObjectPool<AudioSource> _audioPool;
        private readonly List<AudioSource> _activeSources = new List<AudioSource>();

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void SetMixerGroup(AudioMixerGroup mixerGroup)
        {
            _mixerGroup = mixerGroup;

            // Если пул уже был создан, можно просто обновить mixer у существующих источников
            if (_audioPool != null)
            {
                foreach (var s in _activeSources)
                    if (s) s.outputAudioMixerGroup = _mixerGroup;

                return;
            }

            CreatePool();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StopAllSounds();
        }

        private void CreatePool()
        {
            if (_audioPool != null) return;

            _audioPool = new ObjectPool<AudioSource>(
                createFunc: CreatePooledAudioSource,
                actionOnGet: source =>
                {
                    source.gameObject.SetActive(true);
                    if (!_activeSources.Contains(source))
                        _activeSources.Add(source);
                },
                actionOnRelease: source =>
                {
                    // ВАЖНО: именно тут должна быть логика "вернуть в пул"
                    source.Stop();
                    source.clip = null;
                    source.loop = false;
                    source.volume = 1f;
                    source.pitch = 1f;
                    source.time = 0f;

                    source.gameObject.SetActive(false);
                    _activeSources.Remove(source);
                },
                actionOnDestroy: source =>
                {
                    if (source != null)
                        Destroy(source.gameObject);
                },
                collectionCheck: true,
                defaultCapacity: _poolSize,
                maxSize: _poolSize
            );
        }

        private AudioSource CreatePooledAudioSource()
        {
            var soundObject = new GameObject("PooledSoundFX");
            soundObject.transform.SetParent(transform, false);

            var audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = _mixerGroup;

            soundObject.SetActive(false);
            return audioSource;
        }

        public void PlaySoundFXClip(AudioClip clip, float volume = 1.0f)
        {
            if (clip == null) return;

            if (_audioPool == null)
            {
                // На случай, если SetMixerGroup забыли вызвать
                CreatePool();
                if (_audioPool == null) return;
            }

            var audioSource = _audioPool.Get();
            audioSource.outputAudioMixerGroup = _mixerGroup; // если миксер поменялся
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();

            // учитываем pitch (если вдруг изменяют)
            float duration = (audioSource.pitch != 0f) ? (clip.length / Mathf.Abs(audioSource.pitch)) : clip.length;
            StartCoroutine(ReturnToPoolWhenFinished(audioSource, duration));
        }

        public void PlayRandomSoundFXClip(AudioClip[] clips, float volume = 1.0f)
        {
            if (clips == null || clips.Length == 0) return;

            var randomClip = clips[Random.Range(0, clips.Length)];
            PlaySoundFXClip(randomClip, volume);
        }

        private IEnumerator ReturnToPoolWhenFinished(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);

            // Защита: источник мог быть уже возвращён StopAllSounds()
            if (source != null && source.gameObject.activeSelf)
            {
                _audioPool.Release(source);
            }
        }

        private void StopAllSounds()
        {
            if (_audioPool == null) return;

            // С конца, потому что Release() будет удалять из _activeSources
            for (int i = _activeSources.Count - 1; i >= 0; i--)
            {
                var src = _activeSources[i];
                if (src != null && src.gameObject.activeSelf)
                    _audioPool.Release(src);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
