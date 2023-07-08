using Game.Managers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class NextLevelSceneChanger : MonoBehaviour
    {
        [SerializeField] private CanvasType canvasToSwitch;
        
        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(ChangeScene);
        }
        
        private void ChangeScene()
        {
            TimeManager.Instance.Resume();
            ScenesManager.Instance.ChangeToNextScene(canvasToSwitch);
        }
    }
}