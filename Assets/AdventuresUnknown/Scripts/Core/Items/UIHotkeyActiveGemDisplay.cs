using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
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
    public class UIHotkeyActiveGemDisplay : AbstractActiveGemDisplay, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image m_Image = null;
        [SerializeField] private AbstractActiveGemDisplay m_DisplayPrefab = null;

        private ActiveGem m_LastActiveGem = null;
        private Entity m_LastEntity = null;
        private string[] m_LastModTypes = null;
        private AbstractActiveGemDisplay m_Display = null;

        public override bool Display(ActiveGem activeGem, Entity entity,string[] modTypes)
        {
            m_LastActiveGem = activeGem;
            m_LastEntity = entity;
            m_LastModTypes = modTypes;
            if (m_LastActiveGem == null)
            {
                if (m_Image)
                    m_Image.enabled = false;
                return true;
            }
            if (m_Display)
                m_Display.Display(m_LastActiveGem,m_LastEntity,m_LastModTypes);

            if (m_Image)
            {
                m_Image.enabled = true;
                m_Image.sprite = m_LastActiveGem.Sprite;
                if (m_LastActiveGem is AuraGem)
                {
                    if (entity.EntityBehaviour.Entity.GetObject<ActiveGem>(m_LastActiveGem) == m_LastActiveGem)
                    {
                        m_Image.color = Color.red;
                    }
                    else
                    {
                        m_Image.color = Color.white;
                    }
                }
            }
            return true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerExit(eventData);
            if (m_LastActiveGem == null) return;
            if (!m_LastActiveGem.ItemStackDisplay) return;
            if (m_LastActiveGem.DisplayPrefab != null)
            {
                m_Display = Instantiate(m_LastActiveGem.DisplayPrefab, transform.root);
            }
            else
            {
                m_Display = Instantiate(m_DisplayPrefab, transform.root);
            }
            CalculateItemStackPosition(m_Display, eventData.position);
            m_Display.Display(m_LastActiveGem,m_LastEntity, m_LastModTypes);
        }

        private void CalculateItemStackPosition(AbstractActiveGemDisplay activeGemDisplay,Vector2 position)
        {
            RectTransform rect = activeGemDisplay.GetComponent<RectTransform>();
            RectTransform thisRect = GetComponent<RectTransform>();

            if (!rect)
            {
                activeGemDisplay.transform.position = position;
                return;
            }
            else
            {
                activeGemDisplay.transform.position = new Vector3(-5000, -5000, 0);
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
