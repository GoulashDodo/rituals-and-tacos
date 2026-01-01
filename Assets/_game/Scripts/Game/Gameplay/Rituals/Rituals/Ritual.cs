using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Rituals
{
    [CreateAssetMenu(menuName = "Game/Rites/Default Rite")]
    public class Ritual : ScriptableObject
    {

        public enum ERiteDifficulty
        {
            Easy = 1,
            Normal = 2,
            Hard = 3,
            Imposssible = 5
        }

        public ERiteDifficulty RiteDifficulty => _riteDifficulty;
        public string RiteName => _riteName;
        public RitualComponent[] Components => _components;

        public CassetteData CassetteForRite => _cassetteForRite; 


        [SerializeField] private ERiteDifficulty _riteDifficulty = ERiteDifficulty.Easy;
        [SerializeField] private string _riteName = "Lorem Ipsum";
        [SerializeField] private RitualComponent[] _components;

        [SerializeField] private CassetteData _cassetteForRite;

    }
}
