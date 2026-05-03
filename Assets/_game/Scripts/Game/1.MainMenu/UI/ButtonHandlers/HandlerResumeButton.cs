using _game.Scripts.Game._2.Gameplay.UI;
using UnityEngine;

namespace _game.Scripts.Game._1.MainMenu.UI.ButtonHandlers
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
