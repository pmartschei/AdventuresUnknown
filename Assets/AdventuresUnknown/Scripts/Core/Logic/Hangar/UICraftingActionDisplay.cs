using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Currencies;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items.Actions;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.AdventuresUnknown.Scripts.Core.Logic.Hangar
{
    public class UICraftingActionDisplay : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private IGameText m_ActionText = null;
        [SerializeField] private ICurrencyText m_CurrencyTextPrefab = null;
        [SerializeField] private Transform m_PrefabParent = null;
        [SerializeField] private LayoutElement m_LayoutElement = null;
        [SerializeField] private float m_BaseHeight = 50;
        [SerializeField] private float m_AdditionalHeightPerCurrency = 30;
        [SerializeField] private Button m_Button = null;

        private CraftingAction m_CurrentCraftingAction = null;
        private ItemStack m_CurrentItemStack = null;
        #region Properties

        #endregion

        #region Methods
        private void OnEnable()
        {
            PlayerManager.OnWalletDisplayChange.AddListener(OnWalletChange);
        }

        private void OnDisable()
        {
            PlayerManager.OnWalletDisplayChange.RemoveListener(OnWalletChange);
        }
        public virtual void Display(CraftingAction craftingAction,ItemStack itemStack)
        {
            m_CurrentCraftingAction = craftingAction;
            m_CurrentItemStack = itemStack;

            CurrencyValue[] currencies = craftingAction.GetCosts(itemStack);
            if (m_LayoutElement)
                m_LayoutElement.minHeight = m_BaseHeight + m_AdditionalHeightPerCurrency * (currencies.Length-1);

            if (m_ActionText)
                m_ActionText.SetText(craftingAction.CraftingText);

            ClearParent();

            foreach(CurrencyValue currency in currencies)
            {
                ICurrencyText currencyInstance = Instantiate(m_CurrencyTextPrefab, m_PrefabParent);
                currencyInstance.SetText(currency.Value.ToString());
                currencyInstance.SetCurrency(currency.Currency.Object);
            }
            if (m_Button)
                m_Button.interactable = craftingAction.IsEnabled(itemStack) && HasEnoughCurrency();
        }

        private void ClearParent()
        {
            foreach(Transform child in m_PrefabParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnWalletChange()
        {
            Display(m_CurrentCraftingAction, m_CurrentItemStack);
        }
        public void InvokeCurrentCraftingAction()
        {
            if (m_CurrentCraftingAction == null) return;
            if (!HasEnoughCurrency()) return;
            m_CurrentCraftingAction.Invoke(m_CurrentItemStack);
            RemoveCurrency();
        }

        private void RemoveCurrency()
        {
            CurrencyValue[] currencies = m_CurrentCraftingAction.GetCosts(m_CurrentItemStack);
            if (currencies == null) return;
            foreach (CurrencyValue currency in currencies)
            {
                PlayerManager.PlayerWallet.AddValue(currency.Currency.Identifier, -currency.Value);
            }
        }

        private bool HasEnoughCurrency()
        {
            CurrencyValue[] currencies = m_CurrentCraftingAction.GetCosts(m_CurrentItemStack);
            if (currencies == null) return true;
            foreach (CurrencyValue currency in currencies)
            {
                if (PlayerManager.PlayerWallet.GetValue(currency.Currency.Identifier) < currency.Value)
                    return false;
            }
            return true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CurrencyValue[] currencies = m_CurrentCraftingAction.GetCosts(m_CurrentItemStack);
            if (currencies == null) return;
            string[] currencyIdentifiers = new string[currencies.Length];
            for(int i=0;i<currencies.Length;i++)
            {
                currencyIdentifiers[i] = currencies[i].Currency.Identifier;
            }
            PlayerManager.SetWalletDisplay(currencyIdentifiers);
        }
        #endregion
    }
}
