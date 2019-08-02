using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.EventSystems;

namespace AdventuresUnknown.Core.UIGameConsole
{
    [DefaultExecutionOrder(-1001)]
    public class UICommandField : MonoBehaviour 
    {
        [SerializeField] private UIGameConsoleHelpPanel m_UIGameConsoleHelpPanel = null;
        [SerializeField] private TMPro.TMP_InputField m_InputField = null;

        private int m_SavedCaretPosition = 0;
        private bool m_CaretChanged = false;
        private void OnEnable()
        {
            m_InputField.onSubmit.AddListener(SendCommand);
        }
        private void OnDisable()
        {
            m_InputField.onSubmit.RemoveListener(SendCommand);
        }
        public void SendCommand(string cmd)
        {
            if (cmd.Equals(string.Empty)) return;
            GameConsole.Log(cmd);
            CommandManager.ExecuteCommand(cmd);
            m_InputField.text = "";
            m_InputField.ActivateInputField();
            m_InputField.Select();
        }
        public void SanitizeInput()
        {
            if (!m_InputField.text.Equals(string.Empty))
            {
                string[] splits = m_InputField.text.Split(' ');
                List<string> removedEmptySplits = new List<string>();
                bool lastWasEmpty = false;
                foreach (string split in splits)
                {
                    if (split.Equals(""))
                    {
                        if (lastWasEmpty) continue;
                        lastWasEmpty = true;
                    }
                    else
                    {
                        lastWasEmpty = false;
                    }
                    removedEmptySplits.Add(split);
                }
                StringBuilder sb = new StringBuilder();
                foreach (string split in removedEmptySplits)
                {
                    sb.Append(split);
                    sb.Append(" ");
                }
                sb.Remove(sb.Length - 1, 1);
                m_InputField.text = sb.ToString();
            }
        }
        public void UpdateHelpPanel()
        {
            SanitizeInput();
            if (m_UIGameConsoleHelpPanel)
                m_UIGameConsoleHelpPanel.UpdateHelp(m_InputField.text);
        }
        public void ShowHelpPanel()
        {
            if (m_UIGameConsoleHelpPanel)
            {
                m_UIGameConsoleHelpPanel.Show();
                m_UIGameConsoleHelpPanel.UpdateHelp(m_InputField.text);
            }
        }
        public void HideHelpPanel()
        {
            if (m_UIGameConsoleHelpPanel)
                m_UIGameConsoleHelpPanel.Hide();
        }
        private void Update()
        {
            if (!m_UIGameConsoleHelpPanel) return;
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    m_CaretChanged = true;
                    m_SavedCaretPosition = m_InputField.caretPosition;
                    m_UIGameConsoleHelpPanel.MoveSelection(1);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    m_CaretChanged = true;
                    m_SavedCaretPosition = m_InputField.caretPosition;
                    m_UIGameConsoleHelpPanel.MoveSelection(-1);
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    string[] splits = m_InputField.text.Split(' ');
                    string selection = m_UIGameConsoleHelpPanel.ChooseSelection();
                    if (!selection.Equals(string.Empty))
                    {
                        splits[splits.Length - 1] = selection;
                    }
                    StringBuilder sb = new StringBuilder();
                    foreach(string split in splits)
                    {
                        sb.Append(split);
                        sb.Append(" ");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    m_InputField.text = sb.ToString();
                    StartCoroutine(MoveCaret());
                }
            }
        }
        private void LateUpdate()
        {
            if (m_CaretChanged)
            {
                StartCoroutine(RestoreCaret());
                m_CaretChanged = false;
            }
        }

        private IEnumerator RestoreCaret()
        {
            m_InputField.caretPosition = m_SavedCaretPosition;
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator MoveCaret()
        {
            yield return new WaitForEndOfFrame();
            m_InputField.MoveToEndOfLine(false, false);
            m_SavedCaretPosition = m_InputField.caretPosition;
        }
    }

}