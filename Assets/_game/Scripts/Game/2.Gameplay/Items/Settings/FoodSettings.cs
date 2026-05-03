using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items.Settings
{
    [CreateAssetMenu(menuName = "Game/Gameplay/Items/Food" )]
    public class FoodSettings : ItemSettings
    {

        [Space][Header("Food")]
        [SerializeField] private int _hungerRestorationAmount = 1;
        public int HungerRestorationAmount => _hungerRestorationAmount;
    }
}
