using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace AdventuresUnknown.Core.Levels
{
    public class UILevelSelectionController : MonoBehaviour
    { 
        [SerializeField] private Button m_Button = null;
        [SerializeField] private ExtensionsToggleGroup m_ExtensionsToggleGroup = null;
        [SerializeField] private RectTransform[] m_LevelDisplays = null;
        [SerializeField] private RadialLayout m_RadialLayout = null;
        
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
        public void OnWidthChange(float width)
        {
            float unitWidth = width / 3.0f;
            if (m_RadialLayout)
            {
                m_RadialLayout.fDistance = unitWidth;
            }
            foreach(RectTransform levelDisplay in m_LevelDisplays)
            {
                if (!levelDisplay) continue;
                levelDisplay.sizeDelta = new Vector2(unitWidth, unitWidth);
            }
        }
    }
}