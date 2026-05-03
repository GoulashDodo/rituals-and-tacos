using _game.Scripts.Game._2.Gameplay._root.Settings;
using UnityEngine;

namespace _game.Scripts.Game._0.Root.Settings
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game/Settings/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField] public GameplaySettings GameplaySettings{ get; private set; }
    }
}