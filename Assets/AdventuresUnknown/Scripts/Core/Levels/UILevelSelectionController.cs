using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Levels
{
    public class UILevelSelectionController : MonoBehaviour
    { 
        [SerializeField] private Button m_Button = null;
        [SerializeField] private ExtensionsToggleGroup m_ExtensionsToggleGroup = null;

        private UILevelDisplay m_SelectedDisplay = null;

        private void Start()
        {
            m_ExtensionsToggleGroup.onToggleGroupToggleChanged.AddListener(OnToggleChange);
            OnToggleChange(false);
        }
        public void OnToggleChange(bool b)
        {
            ExtensionsToggle selectedToggle = m_ExtensionsToggleGroup.SelectedToggle;
            m_Button.interactable = b;
            if (selectedToggle != null)
            {
                UILevelDisplay levelDisplay =  selectedToggle.GetComponent<UILevelDisplay>();
                if (!levelDisplay)
                {
                    m_Button.interactable = false;
                    return;
                }
                m_SelectedDisplay = levelDisplay;
            }
        }
        public void ChooseLevel()
        {
            if (m_SelectedDisplay == null) return;
            LevelManager.CurrentLevel = m_SelectedDisplay.CurrentLevel;
            SceneManager.LoadScene("core.scenes.play");
        }
    }
}