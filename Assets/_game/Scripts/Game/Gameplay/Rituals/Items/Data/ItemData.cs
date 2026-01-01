using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game.Gameplay.Rituals.Items.Data
{
    [CreateAssetMenu(menuName = "Game/Item/Item Data")]
    public class ItemData : ScriptableObject
    {
        [TabGroup("General"), LabelText("Item Name")]
        [SerializeField] private string _itemName;

        [TabGroup("General"), PreviewField(70)]
        [SerializeField] private Sprite _itemIcon;

        public string ItemName => _itemName;
        public Sprite ItemIcon => _itemIcon;

        [TabGroup("View"), Header("Item View")]
        [SerializeField, PreviewField(70), HideLabel]
        private Sprite[] _itemSprites;

        [Button("Get Random Sprite")]
        public Sprite GetRandomSprite()
        {
            if (_itemSprites == null || _itemSprites.Length == 0)
                return null;

            return _itemSprites[Random.Range(0, _itemSprites.Length)];
        }
    }
}
