using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using UnityEngine;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
{
    public class HandlerResumeButton : MonoBehaviour
    {

        [SerializeField] private GameObject _pauseMenu;

        public void ResumeGame()
        {
            PauseController.Instance.TogglePause();
            _pauseMenu.gameObject.SetActive(false);
        }

    }
}
