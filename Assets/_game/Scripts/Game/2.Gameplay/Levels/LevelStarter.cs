using _game.Scripts.Game._2.Gameplay.Rituals.Service;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.Levels
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