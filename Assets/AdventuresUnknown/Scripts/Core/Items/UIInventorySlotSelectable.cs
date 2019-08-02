using AdventuresUnknownSDK.Core.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using AdventuresUnknownSDK.Core.UI.Items;

namespace Assets.AdventuresUnknown.Scripts.Core.Items
{
    public class UIInventorySlotSelectable : MonoBehaviour
    {
        [SerializeField] private UIInventorySlot m_InventorySlot = null;
        [SerializeField] private ExtensionsToggle m_Toggle = null;
        #region Properties

        #endregion

        #region Methods
        public void OnSelectableValueChange(bool value)
        {
            if (!value) return;
            if (m_InventorySlot.Inventory.Items[m_InventorySlot.Slot].IsEmpty)
            {
                m_Toggle.IsOn = false;
            }
        }
        #endregion
    }
}
