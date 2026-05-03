using _game.Scripts.Game._2.Gameplay.Levels;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay._root
{
    public class LevelBackgroundMusic : MonoBehaviour
    {


        [SerializeField] private AudioClip _backgroundMusic;

        private Level _level;


        [Inject]
        private void Initialize(Level level)
        {
            _level = level;
        }

        private void Start()
        {
            _level.StartLevel();

            //MusicManager.Instance.PlayMusic(_backgroundMusic);
        }

    }
}
