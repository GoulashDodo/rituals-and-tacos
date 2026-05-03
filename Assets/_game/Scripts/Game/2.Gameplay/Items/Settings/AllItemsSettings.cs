using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items.Settings
{
    [CreateAssetMenu(fileName = "All Item Settings", menuName = "Game/Gameplay/Items/All Item Settings")]
    public class AllItemsSettings : ScriptableObject
    {
        [field: SerializeField] public ItemSettings[] AllItemSettings { get; private set; }   
    }
}