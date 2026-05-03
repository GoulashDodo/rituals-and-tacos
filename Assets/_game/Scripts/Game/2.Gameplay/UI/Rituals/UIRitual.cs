using System;
using _game.Scripts.Game._2.Gameplay.Items.Settings;
using _game.Scripts.Game._2.Gameplay.Rituals.Service;
using _game.Scripts.Game._2.Gameplay.Rituals.Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game._2.Gameplay.UI.Rituals
{
    public class UIRitual : MonoBehaviour
    {
        [SerializeField] private TMP_Text _riteNameTMP;

        [Space(10)]
        [SerializeField] private UIRitualComponent[] _uiComponents;


        private RitualService _ritualService;

        [Inject]
        private void Initialize(RitualService ritualService)
        {
            _ritualService = ritualService;
            _ritualService.OnCurrentRitualChanged += UpdateRitualUI;
            _ritualService.OnAllItemsOfOneTypeAdded += SetItemTMPStrikeThrough;
            _ritualService.OnItemReturnedToAltar += RemoveItemTMPStrikeThrough;

        }

        private void OnEnable()
        {
            if (_ritualService != null)
            {
                _ritualService.OnCurrentRitualChanged += UpdateRitualUI;
                _ritualService.OnAllItemsOfOneTypeAdded += SetItemTMPStrikeThrough;
                _ritualService.OnItemReturnedToAltar += RemoveItemTMPStrikeThrough;
            }
        }
        private void OnDisable()
        {
            if (_ritualService != null)
            {
                _ritualService.OnCurrentRitualChanged -= UpdateRitualUI;
                _ritualService.OnAllItemsOfOneTypeAdded -= SetItemTMPStrikeThrough;
                _ritualService.OnItemReturnedToAltar -= RemoveItemTMPStrikeThrough;
            }
        }

        private void DisableRitualComponentsUI()
        {
            foreach (var component in _uiComponents)
            {
                component.Hide();
            }

        }

        private void UpdateRitualUI(RitualSettings currentRitualSettings)
        {
            DisableRitualComponentsUI();

            _riteNameTMP.text = currentRitualSettings.RiteName;

            int componentCount = Mathf.Min(currentRitualSettings.Components.Length, _uiComponents.Length);

            for (int i = 0; i < componentCount; i++)
            {
                _uiComponents[i].Display(currentRitualSettings.Components[i]);
            }
           
        }

        private void SetItemTMPStrikeThrough(ItemSettings item, RitualSettings currentRitualSettings)
        {
            int index = Array.FindIndex(
                currentRitualSettings.Components,
                c => c.RequiredItem == item);

            if (index >= 0 && index < _uiComponents.Length)
            {
                _uiComponents[index].SetTMPStrikeThrough();
            }


        }

        private void RemoveItemTMPStrikeThrough(ItemSettings item, RitualSettings currentRitualSettings)
        {
            int index = Array.FindIndex(
                currentRitualSettings.Components,
                c => c.RequiredItem == item);

            if (index >= 0 && index < _uiComponents.Length)
            {
                _uiComponents[index].SetTMPStrikeThrough();
            }


        }


    }
}
