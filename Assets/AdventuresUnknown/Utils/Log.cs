using Assets.AdventuresUnknown.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AdventuresUnknown.Core.Utils
{
    [CreateAssetMenu(menuName ="AdventuresUnknown/Core/Utils/Log",fileName = "Log.asset")]
    public class Log : ScriptableObject
    {
        [SerializeField] private int m_MaxEntries = 100;
        [SerializeField] private List<LogEntry> m_LogEntries = new List<LogEntry>();

        private LogEntryEvent m_OnAddEntryEvent = new LogEntryEvent();
        private LogEntryEvent m_OnRemoveEntryEvent = new LogEntryEvent();
        private UnityEvent OnClearEvent = new UnityEvent();

        #region Properties
        public LogEntryEvent OnAddEntry => m_OnAddEntryEvent;
        public LogEntryEvent OnRemoveEntry => m_OnRemoveEntryEvent;
        public UnityEvent OnClear => OnClearEvent;
        public LogEntry[] LogEntries { get => m_LogEntries.ToArray(); }
        #endregion

        #region Methods
        public void AddEntry(LogEntry logEntry)
        {
            if (m_LogEntries.Count >= m_MaxEntries)
            {
                LogEntry removedEntry = m_LogEntries[0];
                m_LogEntries.RemoveAt(0);
                OnRemoveEntry.Invoke(removedEntry);
            }
            m_LogEntries.Add(logEntry);
            OnAddEntry.Invoke(logEntry);
        }
        public void Clear()
        {
            m_LogEntries.Clear();
            OnClear.Invoke();
        }
        private void OnValidate()
        {
            int logsToDelete = m_LogEntries.Count - m_MaxEntries;
            if (logsToDelete > 0)
            {
                for (int i = 0; i < logsToDelete; i++)
                {
                    LogEntry logEntry = m_LogEntries[0];
                    m_LogEntries.RemoveAt(0);
                    if (Application.isPlaying)
                        OnRemoveEntry.Invoke(logEntry);
                }
            }
        }
        #endregion
    }
}
