using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.UI.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.AdventuresUnknown.Scripts.Core.Items
{
    public class UIActiveGemDisplay : AbstractActiveGemDisplay
    {
        [SerializeField] private Image m_ItemImage = null;
        [SerializeField] private IGameText m_ItemNameText = null;
        [SerializeField] private IGameText m_DescriptionText = null;
        [SerializeField] private IEntityDisplay m_EntityDisplay = null;

        #region Properties

        #endregion

        #region Methods
        public override bool Display(ActiveGem activeGem,Entity entity,string[] modTypes)
        {
            if (activeGem == null) return false;
            if (m_ItemImage)
                m_ItemImage.sprite = activeGem.Sprite;
            if (m_ItemNameText)
                m_ItemNameText.SetText(activeGem.ItemName);
            if (m_DescriptionText)
                m_DescriptionText.SetText(activeGem.Description);
            if (m_EntityDisplay)
                m_EntityDisplay.Display(entity,"activegem",modTypes);
            return true;
        }
        #endregion
    }
}
