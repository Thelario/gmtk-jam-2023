using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI
{
    public enum CanvasType
    {
        MainMenu,
        MainLevelSelector,
        GameMenu,
        GameLevelVictoryMenu,
        GameLevelLostMenu,
        GamePauseMenu
    }

    public class CanvasManager : Singleton<CanvasManager>
    {
        List<CanvasController> canvasControllerList;
        public CanvasController lastActiveCanvas;

        [SerializeField] private bool startInMainMenu;
        
        protected override void Awake()
        {
            base.Awake();

            canvasControllerList = GetComponentsInChildren<CanvasController>(true).ToList();

            canvasControllerList.ForEach(x => x.gameObject.SetActive(false));

            if (startInMainMenu)
            {
                SwitchCanvas(CanvasType.MainMenu);
                TimeManager.Instance.Pause();
            }
            else
            {
                SwitchCanvas(CanvasType.GameMenu);
            }
        }

        public void SwitchCanvas(CanvasType _type)
        {
            if (lastActiveCanvas != null)
                lastActiveCanvas.gameObject.SetActive(false);

            CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActiveCanvas = desiredCanvas;
            }
            else { Debug.LogWarning("The desired menu canvas was not found!"); }
        }

        public void SwitchCanvas(CanvasType _type, float timeToWaitBeforeSwitch)
        {
            StartCoroutine(SwitchCanvasWaiting(_type, timeToWaitBeforeSwitch));
        }

        private IEnumerator SwitchCanvasWaiting(CanvasType _type, float time)
        {
            yield return new WaitForSecondsRealtime(time);

            if (lastActiveCanvas != null)
            {
                lastActiveCanvas.gameObject.SetActive(false);
            }

            CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActiveCanvas = desiredCanvas;
            }
            else { Debug.LogWarning("The desired menu canvas was not found!"); }
        }
    }
}
