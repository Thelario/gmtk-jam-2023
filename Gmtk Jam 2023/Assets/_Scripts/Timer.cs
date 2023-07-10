using Game.Managers;
using Game.UI;
using TMPro;
using UnityEngine;

namespace Game
{
	public class Timer : MonoBehaviour
	{
		[SerializeField] private TMP_Text timerText;
		[SerializeField] private int startTime;

		private float _timeSinceGameStarted;
		private int _minutes;
		private int _seconds;
		private string _auxText;

		private void Start()
		{
			ResetTimer();
		}
		
		public void ResetTimer()
		{
			_timeSinceGameStarted = startTime;
		}

		private void Update()
		{
			CalculateMinutesAndSeconds();

			SetTimerText();
		}

		private void CalculateMinutesAndSeconds()
		{
			_timeSinceGameStarted -= Time.deltaTime;

			if (_timeSinceGameStarted <= 0f)
			{
				TimeManager.Instance.Pause();
				CanvasManager.Instance.SwitchCanvas(CanvasType.GameLevelLostMenu);
			}

			_minutes = Mathf.FloorToInt(_timeSinceGameStarted / 60);
			_seconds = Mathf.FloorToInt(_timeSinceGameStarted % 60);
		}

		private void SetTimerText()
		{
			_auxText = "";

			if (_minutes == 0)
				_auxText += "00:";
			else if (_minutes < 10)
				_auxText += "0" + _minutes + ":";
			else
				_auxText += _minutes + ":";

			if (_seconds < 10)
				_auxText += "0" + _seconds;
			else
				_auxText += "" + _seconds;
			
			timerText.text = _auxText;
		}
	}
}