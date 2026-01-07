using _game.Scripts.Game.Gameplay.Rituals.Levels;
using UnityEngine;

namespace _game.Scripts.Game.Root.LevelLoading
{
    [CreateAssetMenu(fileName = "Game/Runtime/Selected Level", menuName = "Selected Level Runtime", order = 0)]
    public class SelectedLevelRuntime : ScriptableObject
    {
        
        [field: SerializeField] public LevelSettings CurrentLevelSettings { get; private set; }

        public void Set(LevelSettings levelSettings)
        {
            CurrentLevelSettings = levelSettings;
        }


        public void Clear()
        {
            CurrentLevelSettings = null;
        }
        
        
    }
}