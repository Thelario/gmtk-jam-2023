using System.Collections;
using UnityEngine;

namespace Game.Managers
{
    public class TimeManager : Singleton<TimeManager>
    {
        public delegate void TimeChange();
        public static event TimeChange OnGamePause;
        public static event TimeChange OnGameResume;

        public void Pause()
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke();
        }

        public void Pause(float time)
        {
            StartCoroutine(Co_PauseAfterTime(time));
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            OnGameResume?.Invoke();
        }

        public void Resume(float time)
        {
            StartCoroutine(Co_ResumeAfterTime(time));
        }

        public void SlowTime(float slowTime)
        {
            StartCoroutine(nameof(Co_SlowTime), slowTime);
        }

        public void FreezeScreen(float freezeTime)
        {
            StartCoroutine(nameof(Co_FreezeTime), freezeTime);
        }

        public bool IsTimePause()
        {
            return Time.timeScale == 0f;
        }

        private IEnumerator Co_SlowTime(float slowTime)
        {
            Time.timeScale = 0.25f;
            
            yield return new WaitForSecondsRealtime(slowTime);

            if (Time.timeScale > 0f)
                Time.timeScale = 1f;
        }
        
        private IEnumerator Co_FreezeTime(float slowTime)
        {
            Time.timeScale = 0f;
            
            yield return new WaitForSecondsRealtime(slowTime);

            Time.timeScale = 1f;
        }

        private IEnumerator Co_PauseAfterTime(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            
            Pause();
        }

        private IEnumerator Co_ResumeAfterTime(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            
            Resume();
        }
    }
}
