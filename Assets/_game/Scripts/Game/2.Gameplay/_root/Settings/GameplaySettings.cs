using _game.Scripts.Game._2.Gameplay.Items.Settings; using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay._root.Settings
{
    [CreateAssetMenu(fileName = "Gameplay Settings", menuName = "Game/Gameplay/Settings/Gameplay Settings")]
    public class GameplaySettings : ScriptableObject
    {
        [field: SerializeField] public AllItemsSettings AllItemsSettings { get; private set; }

    }
}