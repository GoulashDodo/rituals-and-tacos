using _game.Scripts.Game._2.Gameplay.Items.Settings;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items.Spawner
{
    public class CassetteSpawnerFx : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _tvSpriteRenderer;

        [SerializeField] private CassetteSpawner _cassetteSpawner;


        private void Awake()
        {
            _cassetteSpawner.OnCurrentTVShowChanged += UpdateView;
        }

        private void UpdateView(CassetteSettings cassette)
        {
            _tvSpriteRenderer.sprite = cassette.ShowSprite;
        

        }

    }
}
