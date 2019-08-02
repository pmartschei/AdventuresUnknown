using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.UI.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Items
{
    public class UICraftingSlotDisplay : AbstractItemStackDisplay
    {
        [SerializeField] private Transform m_DisplayTransform = null;
        private AbstractItemStackDisplay m_Display = null;
        private ItemStack m_LastItemStack = null;

        #region Properties

        #endregion

        #region Methods

        public override bool Display(ItemStack itemStack)
        {
            if (itemStack == null) return true;
            if (m_LastItemStack != itemStack)
            {
                m_LastItemStack = itemStack;
                if (m_Display)
                {
                    Destroy(m_Display.gameObject);
                }
                if (itemStack.IsEmpty)
                {
                    return true;
                }
                m_Display = Instantiate(itemStack.Item.ItemStackDisplay, m_DisplayTransform);
            }
            m_Display.Display(itemStack);
            return true;
        }


        #endregion
    }
}
