using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Machine : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject outline;
        [SerializeField] private List<ItemSO> validInputs;
        
        public void EnterInteractable()
        {
            outline.SetActive(true);
        }

        public bool CanInteract(Item itemToGive)
        {
            if (!IsItemValid(itemToGive.CurrentItemSO))
                return false;

            print("Do things");
            
            return true;
        }

        public void ExitInteractable()
        {
            outline.SetActive(false);
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