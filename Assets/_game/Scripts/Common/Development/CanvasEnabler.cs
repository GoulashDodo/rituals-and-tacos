using UnityEngine;

namespace _game.Scripts.Common.Development
{
    public class CanvasEnabler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Start()
        {
            var camera = Camera.main;
            
            _canvas.worldCamera = camera;
            
        }
        
        
    }
}