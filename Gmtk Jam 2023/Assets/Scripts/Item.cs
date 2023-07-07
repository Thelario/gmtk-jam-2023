using TMPro;
using UnityEngine;

namespace Game
{
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject outline;
        [SerializeField] private BoxCollider2D bc2D;
        [SerializeField] private ItemSO itemSO;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TMP_Text text;

        private ItemSO _currentItemSO;

        public ItemSO CurrentItemSO => _currentItemSO;
        
        private void Start()
        {
            ConfigureItem(itemSO);
        }

        public void ConfigureItem(ItemSO itemSo)
        {
            _currentItemSO = itemSo;
            text.text = _currentItemSO.name;
            spriteRenderer.color = _currentItemSO.itemColor;
        }

        public void DropItem()
        {
            transform.SetParent(null);
            bc2D.enabled = true;
        }
        
        public void EnterInteractable()
        {
            outline.SetActive(true);
        }

        public Item Interact()
        {
            outline.SetActive(false);
            bc2D.enabled = false;
            return this;
        }

        public void ExitInteractable()
        {
            outline.SetActive(false);
        }
    }
}