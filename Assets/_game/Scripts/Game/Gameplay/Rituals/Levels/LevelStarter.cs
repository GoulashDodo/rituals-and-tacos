using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels
{
    public class LevelStarter : IInitializable
    {
        
        [Inject] private RitualService _ritualService;
        
        
        
        
        public void Initialize()
        {
            _ritualService.SetRandomRite();
        }
    }
}