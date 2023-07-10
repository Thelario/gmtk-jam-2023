using TMPro;
using UnityEngine;

namespace Game
{
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private BoxCollider2D bc2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private ItemSO _currentItemSO;

        public ItemSO CurrentItemSO => _currentItemSO;

        public void ConfigureItem(ItemSO itemSo)
        {
            _currentItemSO = itemSo;
            spriteRenderer.sprite = itemSo.itemSprite;
        }

        public void DropItem()
        {
            transform.SetParent(null);
            bc2D.enabled = true;
        }

        public void UseItem()
        {
            transform.SetParent(null);
            bc2D.enabled = false;
            spriteRenderer.enabled = false;
        }

        public InteractableType GetInteractableType()
        {
            return InteractableType.Item;
        }

        public void EnterInteractable()
        {
            spriteRenderer.sprite = _currentItemSO.itemSpriteOutline;
        }

        public Item Interact()
        {
            spriteRenderer.sprite = _currentItemSO.itemSprite;
            bc2D.enabled = false;
            return this;
        }

        public void ExitInteractable()
        {
            spriteRenderer.sprite = _currentItemSO.itemSprite;
        }
    }
}