using System.Collections.Generic;
using System.Linq;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Gameplay.Rituals.Levels.Save;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Common.Development
{
    public sealed class LevelUnlocker : MonoBehaviour
    {
        [Inject] private ILevelSaveService _levelSaveService;
        [Inject] private AllLevelSettings _allLevelSettings;

        [Title("Levels")]
        [ShowInInspector, ReadOnly]
        [ListDrawerSettings(Expanded = true, IsReadOnly = true)]
        private List<LevelRow> Levels => BuildRows();

        [PropertySpace(10)]
        [Button(ButtonSizes.Large)]
        private void UnlockAll()
        {
            foreach (var level in _allLevelSettings.Levels)
            {
                _levelSaveService.Unlock(level.TypeId);
            }
        }
        

        private List<LevelRow> BuildRows()
        {
            return _allLevelSettings.Levels
                .Select(x => new LevelRow(x.TypeId, _levelSaveService))
                .ToList();
        }

        public class LevelRow
        {
            private readonly ILevelSaveService _service;

            public LevelRow(string levelTypeId, ILevelSaveService service)
            {
                LevelTypeId = levelTypeId;
                _service = service;
            }

            [HorizontalGroup("Row", Width = 260)]
            [ShowInInspector, ReadOnly]
            public string LevelTypeId { get; }

            [HorizontalGroup("Row", Width = 80)]
            [ShowInInspector, ReadOnly]
            public bool IsUnlocked => _service.IsUnlocked(LevelTypeId);

            [HorizontalGroup("Row", Width = 90)]
            [Button("Unlock")]
            private void Unlock() => _service.Unlock(LevelTypeId);
            
        }
    }
}
