using _game.Scripts.Game._2.Gameplay.Items.Settings;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items
{
    public class PlaceableItemFx : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;

        [SerializeField] private AudioClip _placeOnAltarAudio;
        [SerializeField] private AudioClip _dropAudio;

        private PlaceableItem _placeableItem;
        private SpriteRenderer _spriteRenderer;

        private static int _counter;

        private void Awake()
        {
            _placeableItem = GetComponent<PlaceableItem>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _placeableItem.OnItemDropped += ShowParticle;
            _placeableItem.OnItemPlaced += PlayPlaceOnAltarSound;

            _spriteRenderer.sortingOrder = _counter++;

            SetSpriteFromData(_placeableItem.ItemSettings);
        }

        private void OnDisable()
        {
            _placeableItem.OnItemDropped -= ShowParticle;
            _placeableItem.OnItemPlaced -= PlayPlaceOnAltarSound;
        }

        private void SetSpriteFromData(ItemSettings settings)
        {
            if (settings == null) return;

            var sprite = settings.GetRandomSprite();
            if (sprite == null) return;

            _spriteRenderer.sprite = sprite;
        }

        private void ShowParticle()
        {
            //SoundFXManager.Instance.PlaySoundFXClip(_dropAudio);

            if (_particlePrefab == null)
            {
                Debug.LogWarning("Particle prefab is not assigned.");
                return;
            }

            var particle = Instantiate(_particlePrefab);
            particle.transform.position = transform.position;

            var particleRenderer = particle.GetComponent<ParticleSystemRenderer>();
            if (particleRenderer != null)
            {
                particleRenderer.sortingLayerID = _spriteRenderer.sortingLayerID;
                particleRenderer.sortingOrder = _spriteRenderer.sortingOrder;
            }

            var textureSheet = particle.textureSheetAnimation;
            textureSheet.RemoveSprite(0);
            textureSheet.AddSprite(_spriteRenderer.sprite);
            

            var main = particle.main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            particle.Play(true);
        }

        private void PlayPlaceOnAltarSound()
        {
            //SoundFXManager.Instance.PlaySoundFXClip(_placeOnAltarAudio);
        }
    }
}
