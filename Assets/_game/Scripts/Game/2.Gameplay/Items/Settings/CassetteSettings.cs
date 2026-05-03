using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items.Settings
{
    [CreateAssetMenu(menuName = "Game/Gameplay/Items/Cassette" )]
    public class CassetteSettings : ItemSettings
    {
        [field: SerializeField] public Sprite ShowSprite { get; private set; } 
        [field: SerializeField] public Sprite Emoji { get; private set; }
        
    }
}
