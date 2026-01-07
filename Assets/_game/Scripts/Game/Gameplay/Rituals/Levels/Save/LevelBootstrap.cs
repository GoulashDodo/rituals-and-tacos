using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels.Save
{
    public class LevelBootstrap : IInitializable
    {
        private readonly Level _level;

        public LevelBootstrap(Level level)
        {
            _level = level;
        }
        
        public void Initialize()
        {
            
            _level.StartLevel();
        }
    }
}