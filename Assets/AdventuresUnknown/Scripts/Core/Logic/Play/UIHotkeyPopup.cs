using AdventuresUnknownSDK.Core.Objects.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace AdventuresUnknown.Scripts.Core.Logic.Play
{
    public class UIHotkeyPopup : MonoBehaviour
    {
        [SerializeField] private UIHotkeyDisplay m_HotkeyDisplayPrefab = null;
        [SerializeField] private Transform m_HotkeyDisplayParent = null;
        private UIHotkeySlot m_Source = null;

        //could be abstract
        public void Display(UIHotkeySlot source, List<HotkeyDisplayData> hotkeyDisplayDatas)
        {
            m_Source = source;
            foreach (HotkeyDisplayData hotkeyDisplayData in hotkeyDisplayDatas)
            {
                UIHotkeyDisplay hotkeyDisplay = Instantiate(m_HotkeyDisplayPrefab, m_HotkeyDisplayParent);
                hotkeyDisplay.gameObject.SetActive(true);

                hotkeyDisplay.Display(hotkeyDisplayData);
            }
        }

        private void Update()
        {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            if (selectedObject != null && selectedObject.transform.IsChildOf(this.transform))
            {
                return;
            }
            Close();
        }
        //could be abstract
        public void Close()
        {
            Destroy(this.gameObject);
        }

        public void SelectHotKeyDisplayData(HotkeyDisplayData hotkeyDisplayData)
        {
            m_Source.HotKeyDisplayData = hotkeyDisplayData;
        }
    }
}
