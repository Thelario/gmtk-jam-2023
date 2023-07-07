using UnityEngine;

namespace Game
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		[SerializeField] private float rayLength;
		[SerializeField] private LayerMask ignoreLayer;
		[SerializeField] private Rigidbody2D rb2D;
		[SerializeField] private Transform itemOnTop;

		private float _horizontal;
		private float _vertical;

		private Vector2 _facingDirection;
		
		private IInteractable _currentInteractable;
		private Item _currentItem;
		
		private void Update()
		{
			GetMoveInput();
			GetDropInput();
			GetInteractionInput();
		}

		private void GetMoveInput()
		{
			_horizontal = Input.GetAxisRaw("Horizontal");
			_vertical = Input.GetAxisRaw("Vertical");
		}

		private void GetInteractionInput()
		{
			if (_currentInteractable == null)
				return;

			if (!Input.GetMouseButtonDown(0)) 
				return;

			if (_currentItem != null)
				_currentItem.GetComponent<Item>().DropItem();
			
			_currentItem = _currentInteractable.Interact();
			_currentItem.transform.SetParent(itemOnTop);
			_currentItem.transform.localPosition = Vector3.zero;
		}

		private void GetDropInput()
		{
			if (!Input.GetMouseButtonDown(1))
				return;

			if (_currentItem == null)
				return;
			
			if (_currentItem.transform.childCount <= 0)
				return;
			
			_currentItem.GetComponent<Item>().DropItem();
			_currentItem = null;
		}
		
		private void FixedUpdate()
		{
			Move();
			ThrowRay();
		}

		private void Move()
		{
			rb2D.velocity = moveSpeed * Time.fixedDeltaTime * new Vector3(_horizontal, _vertical).normalized;
		}

		private void ThrowRay()
		{
			Vector2 direction = new Vector2(_horizontal, _vertical).normalized;
			if (direction != Vector2.zero)
				_facingDirection = direction;
			
			RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingDirection, rayLength, ~ignoreLayer);
			
			Debug.DrawRay(transform.position, rayLength * _facingDirection, Color.red);

			if (hit.collider == null)
			{
				if (_currentInteractable == null)
					return;

				_currentInteractable.ExitInteractable();
				_currentInteractable = null;
				return;
			}

			_currentInteractable = hit.collider.GetComponent<IInteractable>();
			_currentInteractable.EnterInteractable();
		}
	}
}