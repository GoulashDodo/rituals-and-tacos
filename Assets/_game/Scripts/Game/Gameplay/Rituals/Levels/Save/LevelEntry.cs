using System;
using System.Collections.Generic;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels.Save
{
    
    [Serializable]
    public sealed class LevelsSaveData
    {
        public int Version = 1;

        public List<LevelEntry> Levels = new();
    }
    
    
    [Serializable]
    public class LevelEntry
    {
        public string TypeId;
        
        public bool IsUnlocked;
        
        public LevelEntry(string typeId, bool isUnlocked)
        {
            TypeId = typeId;
            IsUnlocked = isUnlocked;
        }
        
    }
}