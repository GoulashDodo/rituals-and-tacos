using System;
using _game.Scripts.Game.Gameplay.Rituals.Levels;
using _game.Scripts.Game.MainMenu.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _game.Scripts.Game.Root.LevelLoading
{
    public interface ILevelLoader
    {
        void LoadLevel(string levelTypeId);
        void LoadNextLevel();
        void ReloadCurrentLevel();
        void LoadMainMenu();
    }

    public sealed class LevelLoader : ILevelLoader
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly AllLevelSettings _allLevelSettings;
        private readonly SelectedLevelRuntime _selectedLevelRuntime;

        private static string _currentLevelTypeId = "Level_Tutorial";
        private string _loadedAdditiveLevelSceneName;

        private const string GameplaySceneName = Scenes.Gameplay;

        [Inject]
        public LevelLoader(
            ISceneLoader sceneLoader,
            AllLevelSettings allLevelSettings,
            SelectedLevelRuntime selectedLevelRuntime)
        {
            _sceneLoader = sceneLoader;
            _allLevelSettings = allLevelSettings;
            _selectedLevelRuntime = selectedLevelRuntime;
        }

        public void LoadLevel(string levelTypeId)
        {
            var levelSettings = FindLevelSettings(levelTypeId);
            if (levelSettings == null)
            {
                Debug.LogError($"LevelLoader: LevelSettings not found for typeId '{levelTypeId}'.");
                return;
            }

            _currentLevelTypeId = levelTypeId;
            _selectedLevelRuntime.Set(levelSettings);

            UnloadPreviousAdditiveIfNeeded();

            _sceneLoader.LoadSingleThenAdditive(GameplaySceneName, levelSettings.TypeId);
            _loadedAdditiveLevelSceneName = levelSettings.TypeId;
        }

        public void LoadNextLevel()
        {
            var levels = _allLevelSettings.Levels;

            var currentIndex = Array.FindIndex(levels, x => x.TypeId == _currentLevelTypeId);
            if (currentIndex < 0)
            {
                Debug.LogError($"LevelLoader: Current level id '{_currentLevelTypeId}' not found.");
                return;
            }

            var nextIndex = currentIndex + 1;
            if (nextIndex >= levels.Length)
            {
                LoadMainMenu();
                return;
            }

            LoadLevel(levels[nextIndex].TypeId);
        }

        public void ReloadCurrentLevel()
        {
            LoadLevel(_currentLevelTypeId);
        }

        public void LoadMainMenu()
        {
            _selectedLevelRuntime.Clear();
            _loadedAdditiveLevelSceneName = null;

            _sceneLoader.LoadSingle(Scenes.MainMenu);
        }

        private LevelSettings FindLevelSettings(string typeId)
        {
            foreach (var level in _allLevelSettings.Levels)
            {
                if (level.TypeId == typeId)
                {
                    return level;
                }
            }

            return null;
        }

        private void UnloadPreviousAdditiveIfNeeded()
        {
            if (string.IsNullOrEmpty(_loadedAdditiveLevelSceneName))
            {
                return;
            }

            var scene = SceneManager.GetSceneByName(_loadedAdditiveLevelSceneName);
            if (!scene.isLoaded)
            {
                _loadedAdditiveLevelSceneName = null;
                return;
            }

            _sceneLoader.Unload(_loadedAdditiveLevelSceneName);
            _loadedAdditiveLevelSceneName = null;
        }
    }
}
