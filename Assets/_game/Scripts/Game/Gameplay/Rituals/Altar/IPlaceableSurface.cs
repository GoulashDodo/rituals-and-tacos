using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Altar
{
    public interface IPlaceableSurface
    {
        void PlaceItem(GameObject itemGO, ItemData itemData);
    }
}