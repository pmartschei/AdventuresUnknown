using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Scripts.Utils
{
    public class UIExperiencePercentageImage : MonoBehaviour
    {
        [SerializeField] private Image m_Image = null;
        private ContextData m_ContextData = null;

        #region Properties

        #endregion

        #region Methods
        private void OnEnable()
        {
            m_ContextData = ObjectsManager.FindObjectByIdentifier<ContextData>("core.datas.context");
            if (!m_ContextData) return;
            m_ContextData.OnExperienceChange.AddListener(OnExperienceChange);
            OnExperienceChange(m_ContextData.Experience);
        }

        private void OnDisable()
        {
            if (m_ContextData)
                m_ContextData.OnExperienceChange.RemoveListener(OnExperienceChange);
        }
        private void OnExperienceChange(int value)
        {
            if (m_Image)
                m_Image.fillAmount = value / 100.0f;
        }
        #endregion
    }
}
