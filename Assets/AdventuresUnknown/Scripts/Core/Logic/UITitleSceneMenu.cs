using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuresUnknown.Core.Logic
{
    public class UITitleSceneMenu : MonoBehaviour
    {

        [SerializeField] GameObject m_MainMenu = null;
        [SerializeField] GameObject m_PlayMenu = null;
        [SerializeField] GameObject m_SettingsMenu = null;

        private GameObject m_CurrentMenu = null;
        private Stack<GameObject> m_CurrentMenuStack = new Stack<GameObject>();

        private void Start()
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            ShowMenu(m_MainMenu);
        }
        
        public void ShowSettings()
        {
            ShowMenu(m_SettingsMenu);
        }
        public void ShowPlay()
        {
            ShowMenu(m_PlayMenu);
        }
        public void ShowMain()
        {
            ShowMenu(m_MainMenu);
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void ShowMenu(GameObject menu)
        {
            if (m_CurrentMenu)
                m_CurrentMenuStack.Push(m_CurrentMenu);
            SwitchToMenu(menu);
        }
        private void SwitchToMenu(GameObject menu)
        {
            if (m_CurrentMenu)
                m_CurrentMenu.SetActive(false);
            m_CurrentMenu = menu;
            m_CurrentMenu.SetActive(true);
        }
        public void Back()
        {
            if (m_CurrentMenuStack.Count > 0)
            {
                SwitchToMenu(m_CurrentMenuStack.Pop());
            }
        }
    }
}
