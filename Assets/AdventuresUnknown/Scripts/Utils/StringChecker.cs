using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace AdventuresUnknown.Utils
{
    public class StringChecker : MonoBehaviour
    {
        [Serializable]
        public class BoolEvent : UnityEvent<bool> { }
        [SerializeField] private BoolEvent m_BoolOutputEvent = null;
        [SerializeField] private BoolEvent m_InversBoolOutputEvent = null;
        [SerializeField] private string m_EqualsCheck = "";
        #region Properties

        #endregion

        #region Methods
        public void IsEmpty(string s)
        {
            bool empty = s.Equals(String.Empty);
            m_BoolOutputEvent.Invoke(empty);
            m_InversBoolOutputEvent.Invoke(!empty);
        }

        public void Equals(string s)
        {
            bool equals = s.Equals(m_EqualsCheck);
            m_BoolOutputEvent.Invoke(equals);
            m_InversBoolOutputEvent.Invoke(!equals);
        }
        #endregion
    }
}
