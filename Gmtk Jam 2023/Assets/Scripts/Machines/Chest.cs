using UnityEngine;

namespace Game
{
	public class Chest : MonoBehaviour, IInteractable
	{
    	[SerializeField] private GameObject outline;
        [SerializeField] private ItemSO validInput;
        
        public void EnterInteractable()
        {
            outline.SetActive(true);
        }

        public bool CanInteract(Item itemToGive)
        {
            if (!IsItemValid(itemToGive.CurrentItemSO))
                return false;

            GoalsManager.Instance.UpdateGoal(validInput);
            
            return true;
        }
        
        public InteractableType GetInteractableType()
        {
            return InteractableType.Machine;
        }

        public void ExitInteractable()
        {
            outline.SetActive(false);
        }

        private bool IsItemValid(ItemSO itemSO)
        {
            return itemSO == validInput;
        }
	}
}