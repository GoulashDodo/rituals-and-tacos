using System;
using System.Collections;
using _game.Scripts.Game.Gameplay.Rituals.Controllers.Singletons;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.Gameplay.Rituals.UI;
using _game.Scripts.Game.MainMenu.Data;
using _game.Scripts.Game.MainMenu.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _game.Scripts.Game.Root._Root
{
    public class LevelLoader
    {
        private readonly FadeCanvas _fadeCanvas;
        private readonly AllLevelSettings _allLevelSettings;
        private static string _currentLevelTypeId = "Level_Tutorial";

        [Inject]
        public LevelLoader(FadeCanvas fadeCanvas, AllLevelSettings allLevelSettings)
        {
            _fadeCanvas = fadeCanvas;
            _allLevelSettings = allLevelSettings;
        }

        public void LoadNextLevel()
        {
            
            var levels = _allLevelSettings.Levels;

            var currentIndex = Array.FindIndex(
                levels,
                level => level.TypeId == _currentLevelTypeId);

            if (currentIndex < 0)
            {
                Debug.LogError($"Level with id '{_currentLevelTypeId}' not found.");
                return;
            }

            var nextIndex = currentIndex + 1;

            if (nextIndex < levels.Length)
            {
                _currentLevelTypeId = levels[nextIndex].TypeId;
                LoadLevelWithFade(_currentLevelTypeId);
            }
            else
            {
                LoadMainMenu();
            }
            
            
        }

        public void LoadCurrentLevel()
        {
            LoadLevelWithFade(_currentLevelTypeId);
        }

        public void RestartLevel()
        {
            var name = SceneManager.GetActiveScene().name;
            LoadLevelWithFade(name);
        }
        
        public void LoadMainMenu()
        {
            LoadLevelWithFade(Scenes.MainMenu);
        }

        public void LoadLevel(string levelTypeId)
        {
            LoadLevelWithFade(levelTypeId);
        }
        
        private void LoadLevelWithFade(string sceneName)
        {
            _fadeCanvas.StartCoroutine(FadeAndLoad(sceneName));
        }

        private IEnumerator FadeAndLoad(string sceneName)
        {

            bool wasPausedBeforeLoading = PauseController.Instance.IsPaused; 

            PauseController.Instance.PauseGame();

            yield return _fadeCanvas.Fade(0.5f, 0.5f);
    
            _fadeCanvas.SetAlpha(1f);

            SceneManager.LoadScene(sceneName);

            yield return null;

            _fadeCanvas.SetAlpha(1f);

            yield return _fadeCanvas.Fade(0f, 0.5f);


        }

    }
}
