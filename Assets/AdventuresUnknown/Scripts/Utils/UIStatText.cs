using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Utils
{
    public class UIStatText : MonoBehaviour
    {
        [SerializeField] private IGameText m_Text = null;
        [SerializeField] private ValueType m_Type = ValueType.Current;
        [SerializeField] private string m_Format = "##,0";
        public enum ValueType
        {
            Flat,
            FlatExtra,
            Current,
            Calculated,
            CalculatedNoExtra,
            More,
            Increased,
            Percentage,
        }

        #region Properties

        #endregion

        #region Methods
        public void OnStatChange(Stat stat)
        {
            if (stat == null) return;
            float value = stat.Current;

            switch (m_Type)
            {
                case ValueType.Flat:
                    value = stat.Flat;
                    break;
                case ValueType.FlatExtra:
                    value = stat.FlatExtra;
                    break;
                case ValueType.Calculated:
                    value = stat.Calculated;
                    break;
                case ValueType.CalculatedNoExtra:
                    value = stat.CalculatedNoExtra;
                    break;
                case ValueType.More:
                    value = stat.More;
                    break;
                case ValueType.Increased:
                    value = stat.Increased;
                    break;
                case ValueType.Percentage:
                    value = stat.Percentage;
                    break;
            }
            if (m_Text)
                m_Text.SetText(value.ToString(m_Format));
        }
        #endregion
    }
}
