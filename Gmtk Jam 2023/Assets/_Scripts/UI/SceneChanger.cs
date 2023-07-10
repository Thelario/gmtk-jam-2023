using Game.Managers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private Scenes sceneToLoad;
        
        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(ChangeScene);
        }
        
        private void ChangeScene()
        {
            if (sceneToLoad != Scenes.Menu)
                TimeManager.Instance.Resume();
            
            ScenesManager.Instance.ChangeScene(sceneToLoad);
        }
    }
}