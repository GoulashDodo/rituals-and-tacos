using System;
using _game.Scripts.Game._2.Gameplay.Items.Settings;

namespace _game.Scripts.Game._2.Gameplay.Rituals.Settings.Structures
{
    [Serializable]
    public struct RitualComponent
    {
    
        public ItemSettings RequiredItem; 
        public int Count;

    }
}
