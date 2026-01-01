using _game.Scripts.Game.Gameplay.Rituals.Altar;
using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels
{
    [CreateAssetMenu(menuName = "Game/Levels/Default Level Data")]

    public class LevelSettings : ScriptableObject
    {

        [field: SerializeField] public string TypeId { get; private set; }
        [field: SerializeField] public bool UnlockedByDefault { get; private set; } = false;
        
        
        [field: Space(10)]
        [field: SerializeField] public string Title {get; private set;}

        
       
        [field:Header("Rites")]
        [field: SerializeField] public int TargetScoreCount { get; private set; } =  10;
        
        [field:InlineEditor]
        [field: SerializeField] public RitualPool LevelRitualPool { get; private set; }
        
        
        
        [field: Header("Movement")]
        [field: SerializeField] public AltarMovementData MovementData{ get; private set;}


        

        [field: Header("Lives")]
        [field: SerializeField] public int AmountOfLivesOnLevel { get; private set; } = 5;
        [field: SerializeField] public int LivesLostOnMiss  { get; private set; } = 1;
        


    }
}
