using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventuresUnknown.Scripts.Core.Levels
{
    public class UIHistoryDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private IGameText m_TagText = null;
        private Level m_Level = null;
        private AbstractLevelDisplay m_CurrentDisplay = null;

        private RectTransform m_RectTransform = null;
        
        #region Methods
        private void Start()
        {
            m_RectTransform = GetComponent<RectTransform>();
        }
        private void OnEnable()
        {
            GameSettingsManager.OnLanguageChange.AddListener(OnLanguageChange);
            OnLanguageChange();
        }
        private void OnDisable()
        {

            GameSettingsManager.OnLanguageChange.RemoveListener(OnLanguageChange);
        }
        public void Display(CompletedLevel completedLevel)
        {
            m_Level = LevelManager.GenerateFromCompletedLevel(completedLevel);

            DisplayLevel(m_Level);
        }

        private void DisplayLevel(Level level)
        {
            if (level == null) return;
            if (m_TagText)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < m_Level.TagList.Count; i++)
                {
                    sb.Append("<color=#");
                    sb.Append(m_Level.TagList[i].HTMLColor);
                    sb.Append(">");
                    sb.Append(m_Level.TagList[i].TagName);
                    sb.Append("</color>");
                    if (i != m_Level.TagList.Count - 1)
                    {
                        sb.AppendLine();
                    }
                }
                m_TagText.SetText(sb.ToString());
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Level != null && m_Level.Display)
            {
                m_CurrentDisplay = Instantiate(m_Level.Display, transform.root);

                RectTransform rect = m_CurrentDisplay.GetComponent<RectTransform>();

                rect.position = new Vector3(-5000, -5000, 0);
                StartCoroutine(rect.PositionNextTo(m_RectTransform, RectTransformExtension.PositionAlignment.Center));

                m_CurrentDisplay.Display(m_Level);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_CurrentDisplay)
            {
                Destroy(m_CurrentDisplay.gameObject);
                m_CurrentDisplay = null;
            }
        }
        private void OnLanguageChange()
        {
            DisplayLevel(m_Level);
        }
        #endregion
    }
}