using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Items
{
    public class PlaceableItemFx : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private ParticleSystem _particlePrefab;

        [SerializeField] private AudioClip _placeOnAltarAudio;
        [SerializeField] private AudioClip _dropAudio;

        private PlaceableItem _altarItem;
        private SpriteRenderer _spriteRenderer;
        private ParticleSystem _particle;

        private static int _counter;
        
        private void Awake()
        {
            _altarItem = GetComponent<PlaceableItem>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _particle = Instantiate(_particlePrefab);
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _particle.gameObject.SetActive(false);
        }

        private void Start()
        {
            SetSpriteFromData();
        }

        private void OnEnable()
        {
            _altarItem.OnItemDropped += ShowParticle;
            _altarItem.OnItemPlaced += PlayPlaceOnAltarSound;
            
            _spriteRenderer.sortingOrder = _counter++;

        }

        private void OnDisable()
        {
            _altarItem.OnItemDropped -= ShowParticle;
            _altarItem.OnItemPlaced -= PlayPlaceOnAltarSound;
        }

        private void SetSpriteFromData()
        {
            if (_itemData == null) return;

            var sprite = _itemData.GetRandomSprite();
            if (sprite == null) return;

            _spriteRenderer.sprite = sprite;

            var textureSheet = _particle.textureSheetAnimation;
            if (textureSheet.enabled)
            {
                textureSheet.RemoveSprite(0);
                textureSheet.AddSprite(sprite);
            }
        }

        private void ShowParticle()
        {
            SoundFXManager.Instance.PlaySoundFXClip(_dropAudio);
            _particle.transform.position = transform.position;
            _particle.gameObject.SetActive(true);
            _particle.Play();
        }

        private void PlayPlaceOnAltarSound()
        {
            SoundFXManager.Instance.PlaySoundFXClip(_placeOnAltarAudio);
        }
    }
}
