﻿using System.Collections;
using Game.Managers;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public enum Scenes { Menu, Level1, Level2, Level3, Level4, Level5 }
    
    public class ScenesManager : Singleton<ScenesManager>
    {
        [SerializeField] private int maxScenes;
        
        public void ChangeToNextScene(CanvasType canvasToSwitch)
        {
            CanvasManager.Instance.SwitchCanvas(canvasToSwitch);
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == maxScenes - 1)
            {
                SoundManager.Instance.ChangeMusicAudioclip(false);
                SceneManager.LoadScene(0);
            }
            else
            {
                SoundManager.Instance.ChangeMusicAudioclip(true);
                SceneManager.LoadScene(sceneIndex + 1);
            }
        }

        public void RestartCurrentScene(CanvasType canvasToSwitch)
        {
            StartCoroutine(Co_WaitBeforeLoading(canvasToSwitch));
        }

        private IEnumerator Co_WaitBeforeLoading(CanvasType canvasToSwitch)
        {
            CanvasManager.Instance.SwitchCanvas(canvasToSwitch);
            yield return new WaitForSecondsRealtime(.1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void ChangeScene(Scenes scene)
        {
            SoundManager.Instance.ChangeMusicAudioclip(scene != Scenes.Menu);

            SceneManager.LoadScene((int)scene);
        }
    }
}