using System.Collections;
using _game.Scripts.Game._1.MainMenu.UI;
using _game.Scripts.Game._2.Gameplay.Levels.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _game.Scripts.Game._0.Root.LevelLoading
{
    public interface ISceneLoader
    {
        void LoadSingle(string sceneName);
        void LoadAdditive(string sceneName);
        void Unload(string sceneName);
        void LoadSingleThenAdditive(string sceneName, string additiveSceneName);
        bool IsLoaded(string sceneName);
    }

    public class SceneLoader : ISceneLoader
    {
        private readonly FadeCanvas _fadeCanvas;

        private readonly float _fadeOutDuration;
        private readonly float _fadeInDuration;

        private bool _isLoading;
        private readonly PauseController _pauseController;

        public SceneLoader(FadeCanvas fadeCanvas, PauseController pauseController)
        {
            _pauseController =  pauseController;
            
            _fadeCanvas = fadeCanvas;
            _fadeOutDuration = 1f;
            _fadeInDuration = 1f;
        }

        public void LoadSingle(string sceneName)
        {
            TryStartLoading(LoadSingleRoutine(sceneName));
        }

        public void LoadAdditive(string sceneName)
        {
            TryStartLoading(LoadAdditiveRoutine(sceneName));
        }

        public void Unload(string sceneName)
        {
            TryStartLoading(UnloadRoutine(sceneName));
        }

        public void LoadSingleThenAdditive(string singleSceneName, string additiveSceneName)
        {
            TryStartLoading(LoadSingleThenAdditiveRoutine(singleSceneName, additiveSceneName));
        }

        public bool IsLoaded(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            return scene.isLoaded;
        }

        private void TryStartLoading(IEnumerator loadingRoutine)
        {
            if (_isLoading)
            {
                Debug.LogWarning("SceneLoader: scene loading is already in progress.");
                return;
            }

            _fadeCanvas.StartCoroutine(LoadingWrapperRoutine(loadingRoutine));
        }

        private IEnumerator LoadingWrapperRoutine(IEnumerator loadingRoutine)
        {
            _isLoading = true;

            yield return loadingRoutine;

            _isLoading = false;
        }

        private IEnumerator LoadSingleRoutine(string sceneName)
        {
            yield return FadeOut();

            yield return LoadSceneAsync(sceneName, LoadSceneMode.Single);

            yield return FadeIn();
        }

        private IEnumerator LoadAdditiveRoutine(string sceneName)
        {
            yield return FadeOut();

            yield return LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            yield return FadeIn();
        }

        private IEnumerator UnloadRoutine(string sceneName)
        {
            yield return FadeOut();

            yield return UnloadSceneAsync(sceneName);

            yield return FadeIn();
        }

        private IEnumerator LoadSingleThenAdditiveRoutine(string singleSceneName, string additiveSceneName)
        {
            yield return FadeOut();

            yield return LoadSceneAsync(singleSceneName, LoadSceneMode.Single);
            yield return LoadSceneAsync(additiveSceneName, LoadSceneMode.Additive);

            yield return FadeIn();
        }

        private IEnumerator FadeOut()
        {
            _pauseController.PauseGame();

            yield return _fadeCanvas.Fade(1f, _fadeOutDuration);

            _fadeCanvas.SetAlpha(1f);
        }

        private IEnumerator FadeIn()
        {
            _fadeCanvas.SetAlpha(1f);

            yield return _fadeCanvas.Fade(0f, _fadeInDuration);

            _pauseController.ResumeGame();
        }

        private static IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode mode)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, mode);

            if (operation == null)
            {
                Debug.LogError($"SceneLoader: LoadSceneAsync returned null for '{sceneName}'.");
                yield break;
            }

            while (!operation.isDone)
            {
                yield return null;
            }
        }

        private static IEnumerator UnloadSceneAsync(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);

            if (!scene.isLoaded)
            {
                yield break;
            }

            var operation = SceneManager.UnloadSceneAsync(scene);

            if (operation == null)
            {
                Debug.LogError($"SceneLoader: UnloadSceneAsync returned null for '{sceneName}'.");
                yield break;
            }

            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}