using Game.Managers;
using UnityEngine;

namespace Game
{
	public class Chest : MonoBehaviour, IInteractable
    {
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite outlineSprite;
    	[SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemSO validInput;
        
        public void EnterInteractable()
        {
            spriteRenderer.sprite = outlineSprite;
        }

        public bool CanInteract(Item itemToGive)
        {
            if (!IsItemValid(itemToGive.CurrentItemSO))
                return false;

            SoundManager.Instance.PlaySound(SoundType.EnntregarItem);
            GoalsManager.Instance.UpdateGoal(validInput);
            
            return true;
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
            return itemSO == validInput;
        }
	}
}