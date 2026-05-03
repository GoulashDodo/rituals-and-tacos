using _game.Scripts.Game._2.Gameplay.Altar.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Levels.Settings
{
    [CreateAssetMenu(menuName = "Game/Levels/Default Level Data")]

    public class LevelSettings : ScriptableObject
    {

        [field: SerializeField] public string TypeId { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public bool UnlockedByDefault { get; private set; } = false;
        
        
        [field: Space(10)]
        [field: SerializeField] public string Title {get; private set;}

        
       
        [field:Header("Rites")]
        [field: SerializeField] public int TargetScoreCount { get; private set; } =  10;
        
        [field:InlineEditor]
        [field: SerializeField] public RitualPoolSettings LevelRitualPoolSettings { get; private set; }
        
        
        
        [field: Header("Movement")]
        [field: SerializeField] public AltarMovementSettings MovementSettings{ get; private set;}


        

        [field: Header("Lives")]
        [field: SerializeField] public int AmountOfLivesOnLevel { get; private set; } = 5;
        [field: SerializeField] public int LivesLostOnMiss  { get; private set; } = 1;
        


    }
}
