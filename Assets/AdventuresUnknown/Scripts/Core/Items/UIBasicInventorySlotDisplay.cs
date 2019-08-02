using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Items
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBasicInventorySlotDisplay : AbstractItemStackDisplay, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image m_Image = null;
        [SerializeField] private TMPro.TMP_Text m_AmountText = null;

        private ItemStack m_LastItemStack = null;
        private AbstractItemStackDisplay m_Display = null;
        private CanvasGroup m_CanvasGroup = null;

        private void OnEnable()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }
        public override bool Display(ItemStack itemStack)
        {
            m_LastItemStack = itemStack;
            if (m_Display)
                m_Display.Display(itemStack);
            if (itemStack.IsEmpty)
            {
                m_CanvasGroup.Hide();
                return true;
            }
            m_CanvasGroup.Show();
            if (m_Image)
            {
                if (!itemStack.Item)
                {
                    m_Image.sprite = null;
                }
                else
                {
                    m_Image.sprite = itemStack.Item.Sprite;
                }
            }
            if (m_AmountText)
            {
                m_AmountText.text = itemStack.Amount.ToString();
            }
            return true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerExit(eventData);
            if (m_LastItemStack.Item == null)
            {
                m_Display = Instantiate(UIManager.MissingItemDisplay, transform.root);
            }
            else
            {
                if (!m_LastItemStack.Item.ItemStackDisplay) return;
                m_Display = Instantiate(m_LastItemStack.Item.ItemStackDisplay, transform.root);
            }
            CalculateItemStackPosition(m_Display, eventData.position);
            m_Display.Display(m_LastItemStack);
        }

        private void CalculateItemStackPosition(AbstractItemStackDisplay itemStackDisplay,Vector2 position)
        {
            RectTransform rect = itemStackDisplay.GetComponent<RectTransform>();
            RectTransform thisRect = GetComponent<RectTransform>();

            if (!rect)
            {
                itemStackDisplay.transform.position = position;
                return;
            }
            else
            {
                itemStackDisplay.transform.position = new Vector3(-5000, -5000, 0);
            }
            StartCoroutine(rect.PositionNextTo(thisRect));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Display)
            {
                Destroy(m_Display.gameObject);
            }
        }
        
    }
}
