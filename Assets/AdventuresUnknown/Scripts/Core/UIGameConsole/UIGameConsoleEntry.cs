using AdventuresUnknown.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AdventuresUnknown.Core.UIGameConsole
{
    public class UIGameConsoleEntry : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text m_Text;

        public LogEntry LogEntry { get; private set; }
        public void Init(LogEntry logEntry)
        {
            LogEntry = logEntry;
            if (m_Text)
            {
                m_Text.text = logEntry.Text;
                switch (logEntry.LogLevel)
                {
                    case LogLevel.Warning:
                        m_Text.color = new Color(255, 165, 0);
                        break;
                    case LogLevel.Error:
                        m_Text.color = Color.red;
                        break;
                }
            }
        }
    }
}