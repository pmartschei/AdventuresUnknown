using System;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Scripts.Core.Logic.Play
{
    public class UIHotkeyDisplay : MonoBehaviour
    {

        [SerializeField] private Image m_Image = null;
        [SerializeField] private UIHotkeyPopup m_Source = null;
        [SerializeField] private UITooltip m_Tooltip = null;
        private HotkeyDisplayData m_HotkeyDisplayData = null;

        private void OnEnable()
        {
            if (m_Tooltip)
            {
                m_Tooltip.Callback = GetValueCallback;
            }
        }

        private ActiveGemData GetValueCallback()
        {
            if (!m_HotkeyDisplayData.Container) return null;
            return new ActiveGemData(m_HotkeyDisplayData.ActiveGem,
                m_HotkeyDisplayData.Container.GetEntityWithApply(m_HotkeyDisplayData.Slot),
                m_HotkeyDisplayData.Container.CalculateDisplayMods(m_HotkeyDisplayData.Slot));
        }

        //could be abstract
        public void Display(HotkeyDisplayData hotkeyDisplayData)
        {
            m_HotkeyDisplayData = hotkeyDisplayData;
            if (m_Image)
            {
                m_Image.sprite = hotkeyDisplayData.ActiveGem.Sprite;
            }
            m_Tooltip?.UpdateDisplay();
        }

        public void Choose()
        {
            m_Source.SelectHotKeyDisplayData(m_HotkeyDisplayData);
        }

    }
}