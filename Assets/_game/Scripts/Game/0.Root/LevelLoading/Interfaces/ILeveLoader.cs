namespace _game.Scripts.Game._0.Root.LevelLoading.Interfaces
{
    public interface ILevelLoader
    {
        void LoadLevel(string levelTypeId);
        void LoadNextLevel();
        void ReloadCurrentLevel();
        void LoadMainMenu();
    }
}