using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Machine : MonoBehaviour, IInteractable
    {
        [SerializeField] private float timeToProcessItem;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite spriteWithOutline;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform slotsParent;
        [SerializeField] private Slider processingSlider;
        [SerializeField] private Animator animator;
        [SerializeField] private List<ItemSO> validInputs;

        private float _timeToProcessItemCounter;
        
        private ItemSO _currentItemBeingProcessed;
        private static readonly int Squash = Animator.StringToHash("squash");

        private void Update()
        {
            if (_timeToProcessItemCounter <= 0f)
                return;

            _timeToProcessItemCounter -= Time.deltaTime;
            processingSlider.value = _timeToProcessItemCounter;
            if (_timeToProcessItemCounter <= 0f)
                CreateItemsOnSlots();
        }

        public void EnterInteractable()
        {
            spriteRenderer.sprite = spriteWithOutline;
        }

        public bool CanInteract(Item itemToGive)
        {
            if (_timeToProcessItemCounter > 0f)
                return false;
            
            if (!IsItemValid(itemToGive.CurrentItemSO))
                return false;

            if (!ThereAreAvailableSlots(itemToGive.CurrentItemSO))
                return false;

            _currentItemBeingProcessed = itemToGive.CurrentItemSO;
            StartProcessingItem();

            return true;
        }

        private bool ThereAreAvailableSlots(ItemSO itemSO)
        {
            int availableSlots = 0;
            foreach (Transform slot in slotsParent)
            {
                if (slot.childCount > 0)
                    continue;

                availableSlots++;
            }

            print("Avaialbe slots: " + availableSlots + ", slots needed: " + itemSO.derivedItems.Count);
            return availableSlots >= itemSO.derivedItems.Count;
        }

        protected virtual void CreateItemsOnSlots()
        {
            int j = 0;
            for (int i = 0; i < _currentItemBeingProcessed.derivedItems.Count; i++)
            {
                print("Slot " + i + ": " + slotsParent.GetChild(i));
                while (slotsParent.GetChild(i).childCount > 0)
                    i++;

                Instantiate(itemPrefab, slotsParent.GetChild(i))
                    .GetComponent<Item>().ConfigureItem(_currentItemBeingProcessed.derivedItems[j]);

                j++;
            }
            
            animator.SetTrigger(Squash);
        }

        protected virtual void StartProcessingItem()
        {
            animator.SetTrigger(Squash);
            processingSlider.gameObject.SetActive(true);
            processingSlider.maxValue = timeToProcessItem;
            processingSlider.value = timeToProcessItem;
            _timeToProcessItemCounter = timeToProcessItem;
        }
        
        public InteractableType GetInteractableType()
        {
            return InteractableType.Machine;
        }

        public void ExitInteractable()
        {
            spriteRenderer.sprite = defaultSprite;
        }

        private bool IsItemValid(ItemSO itemSO)
        {
            foreach (ItemSO item in validInputs)
            {
                if (itemSO != item)
                    continue;

                return true;
            }

            return false;
        }
    }
}