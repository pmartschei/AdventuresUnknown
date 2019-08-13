using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuresUnknown.Utils.UI
{
    public class UIPauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            GameSettingsManager.IsPaused = true;
        }
        private void OnDisable()
        {
            GameSettingsManager.IsPaused = false;
        }
        public void CloseMenu()
        {
            this.gameObject.SetActive(false);
        }
        public void OpenMenu()
        {
            this.gameObject.SetActive(true);
        }

        public void QuitToDesktop()
        {
            PlayerManager.Save();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SwitchScene(string scene)
        {
            PlayerManager.Save();
            SceneManager.LoadScene(scene);
        }
    }
}