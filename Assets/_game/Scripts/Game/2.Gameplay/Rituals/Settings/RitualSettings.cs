using _game.Scripts.Game._2.Gameplay.Items.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Settings.Structures;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Rituals.Settings
{
    [CreateAssetMenu(menuName = "Game/Gameplay/Rites/Default Rite")]
    public class RitualSettings : ScriptableObject
    {

        [field: SerializeField] public string TypeId { get; private set;  }

        [field: SerializeField] public ERiteDifficulty RiteDifficulty { get; private set; } =  ERiteDifficulty.Normal;
        
        
        [field: SerializeField] public string RiteName { get; private set; } = "Test";
        [field: SerializeField] public RitualComponent[] Components { get; private set; }
        [field: SerializeField] public CassetteSettings CassetteForRite { get; private set; } 

        


    }
}
