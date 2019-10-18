using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Currencies;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.UI.Items.Interfaces;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Logic.Hangar
{
    [RequireComponent(typeof(ExtensionsToggleGroup))]
    public class UIVendorBuyController : MonoBehaviour
    {
        [SerializeField] private ICurrencyText m_CurrencyText = null;
        [SerializeField] private Button m_BuyButton = null;
        [SerializeField] private VendorDataIdentifier m_VendorData = null;
        private ExtensionsToggleGroup m_ToggleGroup = null;
        private ItemStack m_SelectedItem = null;
        private IInventorySlot m_SelectedInventorySlot = null;

        #region Methods
        private void Awake()
        {
            m_ToggleGroup = GetComponent<ExtensionsToggleGroup>();
        }
        private void Start()
        {
            if (!m_VendorData.ConsistencyCheck())
            {
                GameConsole.LogWarningFormat("VendorData is invalid {0}", this);
            }
        }
        private void OnEnable()
        {
            PlayerManager.OnWalletDisplayChange.AddListener(OnWalletChange);
        }
        private void OnDisable()
        {
            PlayerManager.OnWalletDisplayChange.RemoveListener(OnWalletChange);
        }

        private void OnWalletChange()
        {
            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            m_SelectedItem = null;
            m_SelectedInventorySlot = null;
            m_BuyButton.interactable = false;
            foreach (ExtensionsToggle toggle in m_ToggleGroup.ActiveToggles())
            {
                IInventorySlot inventorySlot = toggle.GetComponent<IInventorySlot>();
                if (!inventorySlot) continue;
                m_SelectedInventorySlot = inventorySlot;
                m_SelectedItem = inventorySlot.Inventory.Items[inventorySlot.Slot];
                break;
            }
            CurrencyValue cv = new CurrencyValue();
            cv.Currency.Identifier = "core.currencies.gold";
            cv.Currency.ConsistencyCheck();
            cv.Value = 0;
            if (m_SelectedItem != null && !m_SelectedItem.IsEmpty)
            {
                cv = m_SelectedItem.Item.CurrencyValue;
                m_CurrencyText.gameObject.SetActive(true);
            }
            else
            {
                m_CurrencyText.gameObject.SetActive(false);
            }
            if (m_CurrencyText)
            {
                m_CurrencyText.SetText(cv.Value * (m_SelectedItem != null ? m_SelectedItem.ExplicitMods.Length : 0));
                m_CurrencyText.SetCurrency(cv.Currency.Object);
            }
            m_BuyButton.interactable = PlayerCanBuySelectedItem();
        }
        public void BuySelectedItem()
        {
            if (!PlayerCanBuySelectedItem()) return;

            Inventory inventory = ObjectsManager.FindObjectByIdentifier<Inventory>("core.inventories.inventory");
            if (!inventory) return;
            ItemStack newItemStack = m_SelectedItem.Copy();
            //TODO IsFull Check
            //if (inventory.CanAddItemStack(newItemStack)) return;

            if (m_VendorData.Object)
            {
                m_VendorData.Object.SlotsBought.Add(m_SelectedInventorySlot.Slot);
            }
            CurrencyValue cv = m_SelectedItem.Item.CurrencyValue;
            PlayerManager.PlayerWallet.AddValue(cv.Currency.Identifier, -cv.Value*m_SelectedItem.ExplicitMods.Length);
            m_SelectedInventorySlot.Inventory.RemoveItemStack(m_SelectedInventorySlot.Slot);

            inventory.AddItemStack(newItemStack);

            m_ToggleGroup.SetAllTogglesOff();
            UpdateVisuals();
        }

        private bool PlayerCanBuySelectedItem()
        {
            if (m_SelectedItem == null || m_SelectedItem.IsEmpty) return false;
            CurrencyValue cv = m_SelectedItem.Item.CurrencyValue;

            if (PlayerManager.PlayerWallet.GetValue(cv.Currency.Identifier) < cv.Value) return false;

            return true;
        }
        #endregion
    }
}
