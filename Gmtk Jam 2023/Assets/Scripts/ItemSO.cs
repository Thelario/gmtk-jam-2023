using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemSO : ScriptableObject
    {
        public string name;
        public Sprite itemSprite;
        public Color itemColor;
    }
}