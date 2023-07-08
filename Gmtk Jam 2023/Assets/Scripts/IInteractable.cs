namespace Game
{
	public enum InteractableType { Item, Machine }
	
	public interface IInteractable
	{
		public InteractableType GetInteractableType();
		
		public void EnterInteractable();

		public virtual Item Interact() { return null; }

		public virtual bool CanInteract(Item itemToGive) { return false; }

		public void ExitInteractable();
	}
}