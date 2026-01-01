namespace _game.Scripts.Game.Gameplay.Rituals.Levels.Save
{
    public interface ILevelSaveService
    {
        bool IsUnlocked(string typeId);
        void Unlock(string typeId);
        void ResetToDefault();
    }
}