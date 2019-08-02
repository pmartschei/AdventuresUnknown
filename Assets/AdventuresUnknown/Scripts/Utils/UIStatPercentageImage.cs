using AdventuresUnknownSDK.Core.Entities;
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
    public class UIStatPercentageImage : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        #region Properties

        #endregion

        #region Methods
        public void OnStatChange(Stat stat)
        {
            if (m_Image)
                m_Image.fillAmount = stat.Percentage;
        }
        #endregion
    }
}
