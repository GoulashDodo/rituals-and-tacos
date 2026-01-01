using System;
using _game.Scripts.Game.Gameplay.Rituals.Controllers;
using _game.Scripts.Game.Gameplay.Rituals.Items.Data;
using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using TMPro;
using UnityEngine;
using Zenject;

namespace _game.Scripts.Game.Gameplay.Rituals.UI.Rituals
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

        private void UpdateRitualUI(Ritual currentRitual)
        {
            DisableRitualComponentsUI();

            _riteNameTMP.text = currentRitual.RiteName;

            int componentCount = Mathf.Min(currentRitual.Components.Length, _uiComponents.Length);

            for (int i = 0; i < componentCount; i++)
            {
                _uiComponents[i].Display(currentRitual.Components[i]);
            }
           
        }

        private void SetItemTMPStrikeThrough(ItemData item, Ritual currentRitual)
        {
            int index = Array.FindIndex(
                currentRitual.Components,
                c => c.RequiredItem == item);

            if (index >= 0 && index < _uiComponents.Length)
            {
                _uiComponents[index].SetTMPStrikeThrough();
            }


        }

        private void RemoveItemTMPStrikeThrough(ItemData item, Ritual currentRitual)
        {
            int index = Array.FindIndex(
                currentRitual.Components,
                c => c.RequiredItem == item);

            if (index >= 0 && index < _uiComponents.Length)
            {
                _uiComponents[index].SetTMPStrikeThrough();
            }


        }


    }
}
