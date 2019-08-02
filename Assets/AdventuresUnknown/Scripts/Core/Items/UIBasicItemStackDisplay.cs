using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Items
{
    public class UIBasicItemStackDisplay : AbstractItemStackDisplay
    {

        [SerializeField] private Image m_ItemImage = null;
        [SerializeField] private IGameText m_ItemNameText = null;
        [SerializeField] private IGameText m_LevelText = null;
        [SerializeField] private IGameText m_ItemTypeText = null;
        [SerializeField] private IGameText m_DescriptionText = null;
        [SerializeField] private ICurrencyText m_ValueText = null;

        protected ItemStack m_CurrentItemStack;

        #region Properties

        #endregion

        #region Methods
        private void OnEnable()
        {
            GameSettingsManager.OnLanguageChange.AddListener(OnLanguageChange);
            OnLanguageChange();
        }
        private void OnDisable()
        {
            GameSettingsManager.OnLanguageChange.RemoveListener(OnLanguageChange);
        }
        public override bool Display(ItemStack itemStack)
        {
            m_CurrentItemStack = itemStack;
            if (itemStack == null) return false;
            if (itemStack.Item == null) return false;
            Item item = itemStack.Item;
            if (m_ItemImage)
            {
                m_ItemImage.sprite = item.Sprite;
            }
            if (m_ItemNameText != null)
            {
                m_ItemNameText.SetText(itemStack.ItemStackName);
            }
            if (m_LevelText != null)
            {
                m_LevelText.SetText(item.Level.ToString());
            }
            if (m_ItemTypeText != null)
            {
                m_ItemTypeText.SetText(item.ItemType.TypeName);
            }
            if (m_DescriptionText != null)
            {
                m_DescriptionText.SetText(item.Description);
            }
            if (m_ValueText != null)
            {
                long sum = item.CurrencyValue.Value * itemStack.Amount;
                m_ValueText.SetText(sum.ToString());
                m_ValueText.SetCurrency(item.CurrencyValue.Currency.Object);
                PlayerManager.PlayerWallet.AddValue(item.CurrencyValue.Currency.Identifier, sum);
                PlayerManager.SetWalletDisplay(item.CurrencyValue.Currency.Identifier);
            }
            return true;
        }
        public void OnLanguageChange()
        {
            Display(m_CurrentItemStack);
        }
        #endregion

    }
}
