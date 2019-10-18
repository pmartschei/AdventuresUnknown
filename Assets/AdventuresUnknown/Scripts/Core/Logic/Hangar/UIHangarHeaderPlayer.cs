using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuresUnknown.Core.Logic.Hangar
{
    public class UIHangarHeaderPlayer : MonoBehaviour
    {

        [SerializeField] private ContextDataIdentifier m_ContextData = null;
        [SerializeField] private IGameText m_ShipNameText = null;
        [SerializeField] private IGameText m_LevelText = null;

        private void OnEnable()
        {
            if (!m_ContextData.ConsistencyCheck()) return;
            if (m_ShipNameText)
            {
                m_ShipNameText.SetText(m_ContextData.Object.ShipName);
            }
            m_ContextData.Object.OnLevelChange.AddListener(OnLevelChange);
            OnLevelChange(m_ContextData.Object.Level);
        }

        private void OnDisable()
        {
            if (!m_ContextData.ConsistencyCheck()) return;
            m_ContextData.Object.OnLevelChange.RemoveListener(OnLevelChange);
        }

        private void OnLevelChange(int level)
        {
            m_LevelText.SetText(level);
        }

    }

}