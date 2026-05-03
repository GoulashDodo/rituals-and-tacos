using _game.Scripts.Game._2.Gameplay.Items.Settings;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Altar
{
    public interface IPlaceableSurface
    {
        void PlaceItem(GameObject itemGo, ItemSettings itemSettings);
    }
}