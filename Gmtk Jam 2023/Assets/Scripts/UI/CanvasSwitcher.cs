using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class CanvasSwitcher : MonoBehaviour
    {
        [SerializeField] private CanvasType desiredCanvasType;
        [SerializeField] private bool transition;

        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            switch (desiredCanvasType)
            {
                case CanvasType.GameMenu:
                    //SoundManager.Instance.PlaySound(SoundType.ButtonClicked);
                    if (transition)
                    {
                        TimeManager.Instance.Resume(.5f);
                        CanvasManager.Instance.SwitchCanvas(desiredCanvasType, .5f);
                    }
                    else
                    {
                        TimeManager.Instance.Resume();
                        CanvasManager.Instance.SwitchCanvas(desiredCanvasType);
                    }
                    break;
                default:
                    //SoundManager.Instance.PlaySound(SoundType.ButtonClicked);
                    if (transition)
                    {
                        TimeManager.Instance.Pause(.5f);
                        CanvasManager.Instance.SwitchCanvas(desiredCanvasType, .5f);
                    }
                    else
                    {
                        TimeManager.Instance.Pause();
                        CanvasManager.Instance.SwitchCanvas(desiredCanvasType);
                    }
                    break;
            }
        }
    }
}
