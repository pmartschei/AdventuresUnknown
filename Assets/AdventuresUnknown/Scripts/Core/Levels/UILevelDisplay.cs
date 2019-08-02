using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Levels
{
    public class UILevelDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private JourneyDataIdentifier m_JourneyData = null;
        [SerializeField] private int m_NextLevelIndex = 0;
        [SerializeField] private Animator m_Animator = null;
        [SerializeField] private IGameText m_TagText = null;

        private RectTransform m_RectTransform = null;

        private Level m_CurrentLevel = null;
        private AbstractLevelDisplay m_CurrentDisplay = null;
        private bool m_Enabled = false;

        #region Properties
        public Level CurrentLevel { get => m_CurrentLevel;}
        #endregion
        #region Methods
        private void Start()
        {
            m_RectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            if (!m_JourneyData.ConsistencyCheck())
            {
                GameConsole.LogError("Missing JourneyData for UILevelDisplay");
                return;
            }
            m_Enabled = true;
            m_JourneyData.Object.OnNextLevelChange.AddListener(OnNextLevelChange);
            GameSettingsManager.OnLanguageChange.AddListener(OnLanguageChange);
            OnLanguageChange();
            OnNextLevelChange();
        }


        private void OnDisable()
        {
            if (!m_Enabled) return;
            GameSettingsManager.OnLanguageChange.RemoveListener(OnLanguageChange);
            m_JourneyData.Object.OnNextLevelChange.RemoveListener(OnNextLevelChange);
        }
        public void Display(Level level)
        {
            m_CurrentLevel = level;
            if (m_CurrentLevel == null) return;
            if (m_TagText)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < level.TagList.Count; i++)
                {
                    sb.Append("<color=#");
                    sb.Append(level.TagList[i].HTMLColor);
                    sb.Append(">");
                    sb.Append(level.TagList[i].TagName);
                    sb.Append("</color>");
                    if (i != level.TagList.Count - 1)
                    {
                        sb.AppendLine();
                    }
                }
                m_TagText.SetText(sb.ToString());
            }
        }

        private void Update()
        {
            if (m_CurrentDisplay)
            {
                RectTransform rect = m_CurrentDisplay.GetComponent<RectTransform>();
                StartCoroutine(rect.PositionNextTo(m_RectTransform, RectTransformExtension.PositionAlignment.Center));
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Animator)
            {
                m_Animator.SetTrigger("Highlighted");
            }

            if (m_CurrentLevel != null && m_CurrentLevel.Display)
            {
                m_CurrentDisplay = Instantiate(m_CurrentLevel.Display, transform.root);

                RectTransform rect = m_CurrentDisplay.GetComponent<RectTransform>();

                rect.position = new Vector3(-5000, -5000, 0);
                StartCoroutine(rect.PositionNextTo(m_RectTransform, RectTransformExtension.PositionAlignment.Center));

                m_CurrentDisplay.Display(m_CurrentLevel);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Animator)
            {
                m_Animator.SetTrigger("Normal");
            }
            if (m_CurrentDisplay)
            {
                Destroy(m_CurrentDisplay.gameObject);
                m_CurrentDisplay = null;
            }
        }

        public void OnToggleChanged(bool value)
        {
            if (!m_Animator) return;
            if (value)
            {
                m_Animator.SetTrigger("Selected");
            }
            else
            {
                m_Animator.SetTrigger("Unselected");
            }
        }

        private void OnLanguageChange()
        {
            Display(m_CurrentLevel);
            Update();
        }
        private void OnNextLevelChange()
        {
            if (m_JourneyData.Object.NextLevels.Length <= m_NextLevelIndex) return;
            Display(m_JourneyData.Object.NextLevels[m_NextLevelIndex]);
        }
        #endregion
    }
}