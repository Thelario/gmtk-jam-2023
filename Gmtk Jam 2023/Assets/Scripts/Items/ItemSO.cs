using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemSO : ScriptableObject
    {
        public Sprite itemSprite;
        public Sprite itemSpriteOutline;
        public List<ItemSO> derivedItems;
    }
}