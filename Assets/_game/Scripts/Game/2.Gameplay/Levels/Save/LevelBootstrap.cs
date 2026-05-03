using System.Collections;
using _game.Scripts.Game._0.Root.LevelLoading;
using _game.Scripts.Game._2.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.Levels.Save
{
    public class LevelBootstrap : MonoBehaviour
    {

        [SerializeField] private float _introDuration = 2f;
        [SerializeField] private SelectedLevelRuntime _selectedLevelRuntime;
        [SerializeField] private UILevelIntro _levelIntro;
        
        private Level _level;
        
        [Inject]
        public void Construct(Level level)
        {
            _level = level;
        }
        
        private void Start()
        {
            StartCoroutine(LevelFlowRoutine());
        }

        private IEnumerator LevelFlowRoutine()
        {
            yield return _levelIntro.Show(_selectedLevelRuntime.CurrentLevelSettings.Title, _introDuration);

            _level.StartLevel();
        }
    }
}