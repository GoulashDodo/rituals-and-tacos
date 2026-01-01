using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using TMPro;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreTMP;

        private Score _score;

        [Inject]
        private void Initialize(Score score)
        {
            _score = score;
            _score.OnScoreChanged += UpdateScoreUI;

            UpdateScoreUI(score.CurrentScore);
        }

        private void UpdateScoreUI(int score)
        {
            _scoreTMP.text = score.ToString();
        }

        private void OnDestroy()
        {
            if (_score != null)
            {
                _score.OnScoreChanged -= UpdateScoreUI; 
            }
        }
    }
}
