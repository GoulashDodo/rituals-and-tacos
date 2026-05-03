using _game.Scripts.Game._2.Gameplay.Levels;
using _game.Scripts.Game._2.Gameplay.Levels.Settings;
using UnityEngine;

namespace _game.Scripts.Game._0.Root.LevelLoading
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