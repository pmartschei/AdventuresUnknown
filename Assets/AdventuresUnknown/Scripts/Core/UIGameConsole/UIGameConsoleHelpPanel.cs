using AdventuresUnknownSDK.Core.Utils.Extensions;
using Assets.AdventuresUnknown.Core.Managers;
using UnityEngine;
using System.Collections.Generic;

namespace AdventuresUnknown.Core.UIGameConsole
{
    public class UIGameConsoleHelpPanel : MonoBehaviour
    {

        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private TMPro.TMP_Text m_TextPrefab;

        private TMPro.TMP_Text[] m_HelpTexts = new TMPro.TMP_Text[0];

        private TMPro.TMP_Text m_CurrentSelection = null;
        private int m_SelectionIndex = 0;

        public void Show()
        {
            if (m_CanvasGroup)
                m_CanvasGroup.Show();
        }
        public void Hide()
        {
            if (m_CanvasGroup)
                m_CanvasGroup.Hide();
        }
        public void UpdateHelp(string cmd)
        {
            this.transform.Clear();
            string[] helps = CommandManagerImpl.GetHelpForCommand(cmd);
            if (helps == null || helps.Length == 0 || cmd.Equals(string.Empty))
            {
                Hide();
                return;
            }
            Show();
            List<string> sortedHelps = new List<string>(helps);
            sortedHelps.Sort();
            sortedHelps.Reverse();
            m_HelpTexts = new TMPro.TMP_Text[sortedHelps.Count];
            for(int i=0;i<sortedHelps.Count;i++)
            {
                TMPro.TMP_Text text = Instantiate(m_TextPrefab, this.transform);
                text.text = helps[i];
                m_HelpTexts[i] = text;
            }
            m_SelectionIndex = m_HelpTexts.Length-1;
            HighlightSelection();
        }
        public void MoveSelection(int value)
        {
            m_SelectionIndex -= value;
            m_SelectionIndex = Mathf.Clamp(m_SelectionIndex, 0, m_HelpTexts.Length-1);
            HighlightSelection();
        }
        protected void HighlightSelection()
        {
            if (m_CurrentSelection)
            {
                m_CurrentSelection.color = Color.white;
                m_CurrentSelection = null;
            }
            if (m_HelpTexts.Length == 0) return;
            m_CurrentSelection = m_HelpTexts[m_SelectionIndex];
            m_CurrentSelection.color = Color.blue;
        }
        public string ChooseSelection()
        {
            if (!m_CurrentSelection) return "";
            return m_CurrentSelection.text;
        }
    }
}