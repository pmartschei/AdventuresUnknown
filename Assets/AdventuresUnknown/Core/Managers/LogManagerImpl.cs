using AdventuresUnknown.Core.Utils;
using AdventuresUnknownSDK.Core.Managers;
using Assets.AdventuresUnknown.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AdventuresUnknown.Core.Managers
{
    public class LogManagerImpl : LogManager
    {
        [SerializeField] private Log m_Log = null;
        protected override void ClearLogImpl()
        {
            m_Log.Clear();
        }
        protected override void LogErrorFormatImpl(string s, params object[] objs)
        {
            GenerateLog(LogLevel.Error, string.Format(s, objs), true);
        }

        protected override void LogErrorImpl(string s)
        {
            GenerateLog(LogLevel.Error, s, true);
        }

        protected override void LogFormatImpl(string s, params object[] objs)
        {
            GenerateLog(LogLevel.Information, string.Format(s, objs), true);
        }

        protected override void LogImpl(string s)
        {
            GenerateLog(LogLevel.Information, s, true);
        }

        protected override void LogWarningFormatImpl(string s, params object[] objs)
        {
            GenerateLog(LogLevel.Warning, string.Format(s,objs), true);
        }

        protected override void LogWarningImpl(string s)
        {
            GenerateLog(LogLevel.Warning, s, true);
        }
        protected void GenerateLog(LogLevel logLevel, string s, bool additionalInfos)
        {
            StringBuilder sb = new StringBuilder();
            if (additionalInfos)
            {
                sb.Append("[");
                sb.Append(DateTime.Now.ToLocalTime().ToLongTimeString());
                sb.Append("] ");
                sb.Append("[");
                sb.Append(logLevel.ToString());
                sb.Append("] ");
            }
            sb.Append(s);
            m_Log.AddEntry(new LogEntry(logLevel, sb.ToString()));
        }
    }
}
