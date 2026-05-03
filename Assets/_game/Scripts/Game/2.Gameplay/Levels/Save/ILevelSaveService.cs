namespace _game.Scripts.Game._2.Gameplay.Levels.Save
{
    public interface ILevelSaveService
    {
        bool IsUnlocked(string typeId);
        void Unlock(string typeId);
        void ResetToDefault();
    }
}