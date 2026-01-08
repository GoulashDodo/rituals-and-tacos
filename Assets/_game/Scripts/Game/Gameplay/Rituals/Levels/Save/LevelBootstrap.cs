using System.Collections;
using System.Collections.Generic;
using _game.Scripts.Game.Gameplay.Rituals.UI;
using _game.Scripts.Game.Root.LevelLoading;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.Levels.Save
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