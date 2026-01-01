using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Rituals
{
    [CreateAssetMenu(menuName = "Game/Rites/Rites Pool")]
    public class RitualPool : ScriptableObject
    {
        
        [InlineEditor]
        [SerializeField] private Ritual[] _rites;

        private Queue<Ritual> _currentRiteQueue = new Queue<Ritual>();

        public Ritual GetRandomRite()
        {
            if (_currentRiteQueue.Count == 0)
            {
                RefreshQueue();
            }

            return _currentRiteQueue.Dequeue();
        }

        private void OnEnable()
        {
            if (_rites == null || _rites.Length == 0)
            {
                Debug.LogWarning("RitePool is empty or not initialized.");
                return;
            }

            RefreshQueue();
        }

        private void RefreshQueue()
        {
            if (_rites == null || _rites.Length == 0) return;

            _currentRiteQueue.Clear();

            Ritual[] shuffledRites = ShuffleArray(_rites);

            foreach (Ritual rite in shuffledRites)
            {
                _currentRiteQueue.Enqueue(rite);
            }
        }

        private Ritual[] ShuffleArray(Ritual[] array)
        {
            if (array == null || array.Length == 0) return new Ritual[0];

            Ritual[] shuffled = (Ritual[])array.Clone();
            for (int i = shuffled.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
            }
            return shuffled;
        }
    }
}
