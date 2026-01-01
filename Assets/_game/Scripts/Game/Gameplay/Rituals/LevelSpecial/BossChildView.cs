using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Items;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.LevelSpecial
{
    public class BossChildView : MonoBehaviour
    {
        [SerializeField] private BossChild _bossChild;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Space(20)]
        [SerializeField] private Sprite[] _hungerSprites; // 0 - Full, 1 - Hungry, 2 - Starving
        [SerializeField] private Sprite _waitingForFoodSprite;

        [Space(20)]
        [SerializeField] private AudioClip _eatingSound; // Звук поедания

        private bool _isNearFood = false;

        private void OnEnable()
        {
            if (_bossChild == null)
            {
                Debug.LogError("BossChild не назначен в BossChildView!");
                return;
            }

            _bossChild.OnHungerChanged += UpdateHungerSprite;
            _bossChild.OnItemEaten += PlayEatingSound;
        }

        private void OnDisable()
        {
            if (_bossChild != null)
            {
                _bossChild.OnHungerChanged -= UpdateHungerSprite;
                _bossChild.OnItemEaten -= PlayEatingSound;
            }
        }

        private void UpdateHungerSprite(int hungerLevel)
        {
            if (_isNearFood) return;
            _spriteRenderer.sprite = _hungerSprites[hungerLevel];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlaceableItem _))
            {
                _isNearFood = true;
                _spriteRenderer.sprite = _waitingForFoodSprite;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlaceableItem _))
            {
                _isNearFood = false;
                UpdateHungerSprite((int)_bossChild.CurrentHungerLevel);
            }
        }

        private void PlayEatingSound()
        {
            if (_eatingSound != null)
            {
                Debug.Log("Wdaw");
                SoundFXManager.Instance.PlaySoundFXClip(_eatingSound);
            }
        }
    }
}
