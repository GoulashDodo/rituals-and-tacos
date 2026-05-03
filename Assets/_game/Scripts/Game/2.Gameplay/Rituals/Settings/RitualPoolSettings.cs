using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Rituals.Settings
{
    [CreateAssetMenu(menuName = "Game/Gameplay/Rites/Rites Pool")]
    public class RitualPoolSettings : ScriptableObject
    {
        [field: SerializeField] public string TypeId { get; private set;  }
        
        [InlineEditor]
        [SerializeField] private RitualSettings[] _rites;


        public RitualSettings GetRandomRite()
        {
            return _rites[Random.Range(0, _rites.Length)];
        }
        
    }
}
