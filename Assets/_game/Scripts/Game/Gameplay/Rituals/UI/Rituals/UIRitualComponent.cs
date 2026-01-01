using _game.Scripts.Game.Gameplay.Rituals.Rituals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.Game.Gameplay.Rituals.UI.Rituals
{
    public class UIRitualComponent : MonoBehaviour  
    {
        [SerializeField] private Image _componentIcon;
        [SerializeField] private TMP_Text _componentName;




        public void Display(RitualComponent component)
        {
            _componentIcon.enabled = true;
            _componentIcon.sprite = component.RequiredItem.ItemIcon;
            
            _componentName.enabled = true;
            _componentName.text = $"{component.RequiredItem.ItemName}: {component.Count}";
            _componentName.fontStyle = FontStyles.Bold;
        }
        public void Hide()
        {
            _componentIcon.enabled = false;
            
            _componentName.enabled = false;
            _componentName.fontStyle = FontStyles.Bold;
        }

        public void SetTMPStrikeThrough()
        {
            _componentName.fontStyle = FontStyles.Bold | FontStyles.Strikethrough;
        }

        public void RemoveTMPStrikeThrough()
        {
            _componentName.fontStyle = FontStyles.Bold;
        }
        
        
        
    }
}