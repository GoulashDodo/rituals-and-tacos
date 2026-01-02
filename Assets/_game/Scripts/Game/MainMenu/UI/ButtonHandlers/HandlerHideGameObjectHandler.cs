using UnityEngine;

namespace _game.Scripts.Game.MainMenu.UI.ButtonHandlers
{
    public class HandlerHideGameObjectHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        
        public void HideGameObject()
        {
            _gameObject.SetActive(false);
        }
        
        
    }
}