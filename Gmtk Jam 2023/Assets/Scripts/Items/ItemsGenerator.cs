using UnityEngine;

namespace Game
{
	public class ItemsGenerator : MonoBehaviour
	{
		[SerializeField] private GameObject itemPrefab;
		[SerializeField] private ItemSO[] items;
		[SerializeField] private Transform[] slots;
		[SerializeField] private float timeBetweenItemsBeingGenerated;

		private float _timeBetweenItemsBeingGeneratedCounter;
		
		private void Start()
		{
			CreateItem();
		}

		private void Update()
		{
			if (!IsAnySlotAvailable())
				return;

			_timeBetweenItemsBeingGeneratedCounter -= Time.deltaTime;
			if (_timeBetweenItemsBeingGeneratedCounter <= 0f)
				CreateItem();
		}

		private bool IsAnySlotAvailable()
		{
			foreach (Transform slot in slots)
			{
				if (slot.childCount == 0)
					return true;
			}

			return false;
		}
		
		private void CreateItem()
		{
			_timeBetweenItemsBeingGeneratedCounter = timeBetweenItemsBeingGenerated;
			
			Instantiate(itemPrefab, FindAvailableSlot()).GetComponent<Item>()
				.ConfigureItem(items[Random.Range(0, items.Length)]);
		}

		private Transform FindAvailableSlot()
		{
			foreach (Transform slot in slots)
			{
				if (slot.childCount == 0)
					return slot;
			}

			return null;
		}
	}
}