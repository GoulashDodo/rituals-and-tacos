using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Altar.Settings
{
    [CreateAssetMenu(menuName = "Game/Altar/Movement Data")]
    public class AltarMovementSettings : ScriptableObject
    {
        
        [field: SerializeField] public float InitialSpeed {get; private set;}
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float AccelerationAmount { get; private set; }
        
    }
}
