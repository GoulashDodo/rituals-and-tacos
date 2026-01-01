using System;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;

namespace _game.Scripts.Game.Gameplay.Rituals.Rituals
{
    [Serializable]
    public struct RitualComponent
    {
    
        public ItemData RequiredItem;
        public int Count;

    }
}
