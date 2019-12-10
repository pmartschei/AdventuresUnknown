using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.UI.Tooltip;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Logic
{
    public class UIExperienceTooltipCallback : MonoBehaviour
    {
        [SerializeField] private ContextDataIdentifier m_ContextData = null;
        [SerializeField] private UITooltip m_Tooltip = null;
        [SerializeField] private bool m_ShowLevel = false;
        private void OnEnable()
        {
            if (m_ContextData.ConsistencyCheck())
            {
                m_ContextData.Object.OnExperienceChange.AddListener(OnExperienceChange);
                OnExperienceChange(m_ContextData.Object.Experience);
                if (m_Tooltip)
                    m_Tooltip.Callback = GetTooltipText;
            }
        }
        private string GetTooltipText()
        {
            StringBuilder sb = new StringBuilder();

            if (m_ShowLevel)
            {
                sb.Append("Level ");
                sb.Append(m_ContextData.Object.Level);
                sb.Append(" | ");
            }
            sb.AppendFormat("{0:0.##%}", m_ContextData.Object.Experience / (float)ExperienceManager.GetExperienceForLevel(m_ContextData.Object.Level + 1));

            return sb.ToString();
        }
        private void OnExperienceChange(int exp)
        {
            if (m_Tooltip)
            {
                m_Tooltip.UpdateDisplay();
            }
        }
        private void OnDisable()
        {
            if (m_ContextData.ConsistencyCheck())
                m_ContextData.Object.OnExperienceChange.RemoveListener(OnExperienceChange);
        }
    }
}
