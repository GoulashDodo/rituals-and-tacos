using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Game._2.Gameplay.Items.Settings
{
    [CreateAssetMenu(menuName = "Game/Gameplay/Items/Item")]
    public class ItemSettings : ScriptableObject
    {

        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public Sprite ItemIcon { get; private set; }

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
