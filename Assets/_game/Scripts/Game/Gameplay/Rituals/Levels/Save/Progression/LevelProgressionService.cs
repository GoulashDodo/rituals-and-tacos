namespace _game.Scripts.Game.Gameplay.Rituals.Levels.Save.Progression
{
    public interface ILevelProgressionService
    {
        void UnlockNextLevel(string currentLevelTypeId);
    }

    public sealed class LevelProgressionService : ILevelProgressionService
    {
        private readonly AllLevelSettings _allLevelSettings;
        private readonly ILevelSaveService _levelSaveService;

        public LevelProgressionService(AllLevelSettings allLevelSettings, ILevelSaveService levelSaveService)
        {
            _allLevelSettings = allLevelSettings;
            _levelSaveService = levelSaveService;
        }

        public void UnlockNextLevel(string currentLevelTypeId)
        {
            int currentIndex = FindLevelIndex(currentLevelTypeId);
            if (currentIndex < 0)
            {
                return;
            }

            int nextIndex = currentIndex + 1;
            if (nextIndex >= _allLevelSettings.Levels.Length)
            {
                return; 
            }

            var nextLevelTypeId = _allLevelSettings.Levels[nextIndex].TypeId;
            _levelSaveService.Unlock(nextLevelTypeId);
        }

        private int FindLevelIndex(string levelTypeId)
        {
            for (int i = 0; i < _allLevelSettings.Levels.Length; i++)
            {
                if (_allLevelSettings.Levels[i].TypeId == levelTypeId)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}