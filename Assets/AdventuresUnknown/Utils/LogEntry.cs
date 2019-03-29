using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Utils
{
    [Serializable]
    public class LogEntry
    {
        [SerializeField] private LogLevel m_LogLevel = LogLevel.Information;
        [SerializeField] private string m_Text = "";

        #region Properties
        public LogLevel LogLevel { get => m_LogLevel; set => m_LogLevel = value; }
        public string Text { get => m_Text; set => m_Text = value; }
        #endregion

        #region Methods
        public LogEntry(LogLevel logLevel, string text)
        {
            this.m_LogLevel = logLevel;
            this.m_Text = text;
        }
        #endregion
    }
}
