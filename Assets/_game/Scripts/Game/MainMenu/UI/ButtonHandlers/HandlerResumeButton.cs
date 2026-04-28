using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.UI;
using UnityEngine;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
{
    public class HandlerResumeButton : MonoBehaviour
    {

        [SerializeField] private UIPauseMenu _pauseMenu;

        public void ResumeGame()
        {
            _pauseMenu.HidePauseMenu();
        }

    }
}
