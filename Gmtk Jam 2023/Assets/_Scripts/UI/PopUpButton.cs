using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
	public class PopUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[Header("PopUp Animation Fields")]
		[SerializeField] private Vector3 defaultScale;
		[SerializeField] private Vector3 maxScale;
		[SerializeField] private float timeFromDefaultToMaxScale;

		private RectTransform _thisRectransform;
		
		private void Awake()
		{
			_thisRectransform = GetComponent<RectTransform>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			LeanTween.scale(_thisRectransform, maxScale, timeFromDefaultToMaxScale).setIgnoreTimeScale(true);
			SoundManager.Instance.PlaySound(SoundType.Blop);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			LeanTween.scale(_thisRectransform, defaultScale, timeFromDefaultToMaxScale).setIgnoreTimeScale(true);
		}
	}
}