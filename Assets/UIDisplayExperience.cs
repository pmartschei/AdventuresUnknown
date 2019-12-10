using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Utils.UnityEvents;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdventuresUnknownSDK.Core.UI.Tooltip;
using System.Text;

public class UIDisplayExperience : MonoBehaviour
{
    [SerializeField] private ContextDataIdentifier m_ContextData = null;
    [SerializeField] private FloatEvent m_OnExperiencePercentageChange = new FloatEvent();
    private void OnEnable()
    {
        if (m_ContextData.ConsistencyCheck())
        {
            m_ContextData.Object.OnExperienceChange.AddListener(OnExperienceChange);
            OnExperienceChange(m_ContextData.Object.Experience);
        }
    }

    private void OnExperienceChange(int exp)
    {
        if (m_ContextData.Object.Level == ExperienceManager.MaxLevel)
        {
            m_OnExperiencePercentageChange.Invoke(1.0f);
        }
        else {
            int requiredExp = ExperienceManager.GetExperienceForLevel(m_ContextData.Object.Level + 1);
            if (requiredExp > 0)
            {
                m_OnExperiencePercentageChange.Invoke(exp / (float)requiredExp);
            }
        }
    }

    private void OnDisable()
    {
        if (m_ContextData.ConsistencyCheck())
            m_ContextData.Object.OnExperienceChange.RemoveListener(OnExperienceChange);
    }
}
