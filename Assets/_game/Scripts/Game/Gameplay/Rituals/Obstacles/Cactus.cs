using _game.Scripts.Game.Gameplay.Rituals.Items;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Obstacles
{
    public class Cactus : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlaceableItem>(out PlaceableItem item))
            {
                if (!item.IsPlaced)
                {
                    item.Drop();
                }
            }
        }

    }
}
