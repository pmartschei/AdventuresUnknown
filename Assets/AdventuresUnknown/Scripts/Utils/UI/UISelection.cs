using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventuresUnknown.Utils.UI
{
    public class UISelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Animator m_Animator = null;

        #region Properties
        #endregion
        #region Methods
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Animator)
            {
                m_Animator.SetTrigger("Highlighted");
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Animator)
            {
                m_Animator.SetTrigger("Normal");
            }
        }

        public void OnToggleChanged(bool value)
        {
            if (!m_Animator) return;
            if (value)
            {
                m_Animator.SetTrigger("Selected");
            }
            else
            {
                m_Animator.SetTrigger("Unselected");
            }
        }
        #endregion
    }
}