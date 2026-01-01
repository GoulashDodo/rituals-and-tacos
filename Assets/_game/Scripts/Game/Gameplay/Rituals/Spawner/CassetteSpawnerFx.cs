using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Spawner
{
    public class CassetteSpawnerFx : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _tvSpriteRenderer;

        [SerializeField] private CassetteSpawner _cassetteSpawner;


        private void Awake()
        {
            _cassetteSpawner.OnCurrentTVShowChanged += UpdateView;
        }

        private void UpdateView(CassetteData cassette)
        {
            _tvSpriteRenderer.sprite = cassette.ShowSprite;
        

        }

    }
}
