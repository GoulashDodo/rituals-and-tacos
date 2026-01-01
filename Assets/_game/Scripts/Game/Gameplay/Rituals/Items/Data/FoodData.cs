using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Items.Data
{
    [CreateAssetMenu(menuName = "Game/Item/Food")]
    public class FoodData : ItemData
    {

        [Space][Header("Food")]
        [SerializeField] private int _hungerRestorationAmount = 1;
        public int HungerRestorationAmount => _hungerRestorationAmount;
    }
}
