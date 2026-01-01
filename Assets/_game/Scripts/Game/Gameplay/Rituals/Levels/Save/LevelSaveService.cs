using System.Linq;
using _game.Scripts.Game.Root.Save;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels.Save
{
    public class LevelSaveService : ILevelSaveService
    {
        private const string SaveKey = "levels_save_v1";
        
        
        private readonly IKeyValueStorage _storage;
        private readonly AllLevelSettings _allLevelSettings;

        private LevelsSaveData _data;
        
        public LevelSaveService(IKeyValueStorage storage, AllLevelSettings allLevelSettings)
        {
            _storage = storage;
            _allLevelSettings = allLevelSettings;

            _data = LoadOrCreate();
            
            EnsureAllLevelsExist();
            SaveInternal();
        }
        
        
        
        
        public bool IsUnlocked(string typeId)
        {
            var entry = _data.Levels.FirstOrDefault(x => x.TypeId == typeId);
            return entry != null && entry.IsUnlocked;
        }

        public void Unlock(string typeId)
        {
            var entry = _data.Levels.FirstOrDefault(x => x.TypeId == typeId);

            if (entry == null)
            {
                _data.Levels.Add(new LevelEntry(typeId, true));
            }
            else
            {
                entry.IsUnlocked = true;
            }

            SaveInternal();

        }

        public void ResetToDefault()
        {
            _data = CreateDefault();
            SaveInternal();

        }

        private LevelsSaveData LoadOrCreate()
        {
            if (_storage.TryGetString(SaveKey, out var jsonData) && !string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    var loaded = JsonUtility.FromJson<LevelsSaveData>(jsonData);
                    return loaded ?? CreateDefault();
                }
                catch
                {
                    return CreateDefault();
                }
            }

            return CreateDefault();

        }

        private LevelsSaveData CreateDefault()
        {
            var data = new LevelsSaveData();

            foreach (var level in _allLevelSettings.Levels)
            {
                data.Levels.Add(new LevelEntry(level.TypeId, level.UnlockedByDefault));
            }
            
            
            return data;
        }

        private void EnsureAllLevelsExist()
        {
            foreach (var level in _allLevelSettings.Levels)
            {
                if (_data.Levels.Any(x => x.TypeId == level.TypeId))
                {
                    continue;
                }
                
                _data.Levels.Add(new LevelEntry(level.TypeId, level.UnlockedByDefault));
            }
        }

        private void SaveInternal()
        {
            var json = JsonUtility.ToJson(_data);
            _storage.SetString(SaveKey, json);
            _storage.Save();
        }
        
        
    }
}