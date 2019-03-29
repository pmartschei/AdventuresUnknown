using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Items
{
    public class UIBasicItemStackDisplay : AbstractItemStackDisplay
    {

        [SerializeField] private Image m_ItemImage = null;
        [SerializeField] private TMPro.TMP_InputField m_ItemNameText = null;
        [SerializeField] private TMPro.TMP_InputField m_LevelText = null;
        [SerializeField] private TMPro.TMP_InputField m_ItemTypeText = null;
        [SerializeField] private TMPro.TMP_InputField m_DescriptionText = null;
        [SerializeField] private TMPro.TMP_InputField m_ValueText = null;

        protected ItemStack m_CurrentItemStack;

        #region Properties

        #endregion

        #region Methods
        public override bool Display(ItemStack itemStack)
        {
            m_CurrentItemStack = itemStack;
            if (itemStack.Item == null) return false;
            Item item = itemStack.Item;
            if (m_ItemImage)
            {
                m_ItemImage.sprite = item.Sprite;
            }
            if (m_ItemNameText)
            {
                m_ItemNameText.text = itemStack.ItemStackName;
            }
            if (m_LevelText)
            {
                m_LevelText.text = item.Level.ToString();
            }
            if (m_ItemTypeText)
            {
                m_ItemTypeText.text = item.ItemType.TypeName;
            }
            if (m_DescriptionText)
            {
                m_DescriptionText.text = item.Description;
            }
            if (m_ValueText)
            {
                int sum = item.Value * itemStack.Amount;
                m_ValueText.text = sum.ToString();
            }
            return true;
        }
        public override void OnLanguageChange()
        {
            Display(m_CurrentItemStack);
        }
        #endregion

    }
}
