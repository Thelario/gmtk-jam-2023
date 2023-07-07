namespace Game
{
	public interface IInteractable
	{
		public void EnterInteractable();

		public virtual Item Interact() { return null; }

		public virtual bool CanInteract(Item itemToGive) { return false; }

		public void ExitInteractable();
	}
}