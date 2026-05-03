using _game.Scripts.Game._2.Gameplay.Levels.Service;
using TMPro;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.UI.Rituals
{
    public class UICompletedRituals : MonoBehaviour
    {
        [SerializeField] private TMP_Text _completedTMP;

        private Score _score;

        //private int _targetRiteCount;

        [Inject]
        private void Initialize(Score score)
        {

            _score = score;

            _score.OnAmountOfCompletedRitualsChanged += UpdateCompletedRitualsUI;


            //_targetRiteCount = score.TargetRiteCount;

            UpdateCompletedRitualsUI(0);


        }

        private void UpdateCompletedRitualsUI(int completedRites)
        {
            _completedTMP.text = completedRites.ToString();
        }

        private void OnDestroy()
        {
            if (_score != null)
            {
                _score.OnAmountOfCompletedRitualsChanged -= UpdateCompletedRitualsUI;
            }
        }
    }
}
