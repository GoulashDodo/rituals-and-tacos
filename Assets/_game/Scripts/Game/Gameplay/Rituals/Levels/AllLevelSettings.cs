using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels
{
    [CreateAssetMenu(fileName = "Level List", menuName = "Game/Level List")]
    public class AllLevelSettings : ScriptableObject
    {
        
        [field: InlineEditor]
        [field: SerializeField] public LevelSettings[] Levels { get; private set; }

    }
}
