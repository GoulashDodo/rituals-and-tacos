using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Items.Data
{
    [CreateAssetMenu(menuName = "Game/Item/Cassette" )]
    public class CassetteData : ItemData
    {
        [field: SerializeField] public Sprite ShowSprite { get; private set; } 
        [field: SerializeField] public Sprite Emoji { get; private set; }
        
    }
}
