using AdventuresUnknown.Core.Utils;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using Assets.AdventuresUnknown.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace AdventuresUnknown.Core.UIGameConsole
{
    public class UIGameConsole : MonoBehaviour
    {
        [SerializeField] private bool m_IsVisible = false;
        [SerializeField] private float m_TransitionTime = 0.5f;
        [SerializeField] private Transform m_LogTransform = null;
        [SerializeField] private UIGameConsoleEntry m_PrefabGameConsoleEntry = null;
        [SerializeField] private Log m_Log = null;
        [SerializeField] private TMPro.TMP_InputField m_CommandField = null;

        [NonSerialized] private float m_CurrentTransitionTime = 0.0f;
        [NonSerialized] private bool m_IsTransitioning = false;
        [NonSerialized] private float m_StartPosition = 0.0f;
        [NonSerialized] private float m_EndPosition = 0.0f;

        private RectTransform m_RectTransform;
        // Start is called before the first frame update
        void Start()
        {
            m_RectTransform = GetComponent<RectTransform>();
            foreach (LogEntry logEntry in m_Log.LogEntries)
            {
                OnLogAddEntry(logEntry);
            }
            m_Log.OnAddEntry.AddListener(OnLogAddEntry);
            m_Log.OnClear.AddListener(OnLogClear);
            m_Log.OnRemoveEntry.AddListener(OnLogRemoveEntry);
        }

        private void OnLogRemoveEntry(LogEntry logEntry)
        {
            UIGameConsoleEntry[] gces = m_LogTransform.GetComponentsInChildren<UIGameConsoleEntry>();
            for (int i = gces.Length - 1; i >= 0; i--)
            {
                if (gces[i].LogEntry == logEntry)
                {
                    Destroy(gces[i].gameObject);
                    break;
                }
            }
        }

        private void OnLogClear()
        {
            m_LogTransform.Clear();
        }

        private void OnLogAddEntry(LogEntry logEntry)
        {
            UIGameConsoleEntry gce = Instantiate(m_PrefabGameConsoleEntry, m_LogTransform);
            gce.Init(logEntry);
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                m_IsVisible = !m_IsVisible;
                if (m_IsTransitioning)
                {
                    m_CurrentTransitionTime = m_TransitionTime - m_CurrentTransitionTime;
                }
                else
                {
                    m_CurrentTransitionTime = 0.0f;
                }
                m_StartPosition = m_RectTransform.sizeDelta.y * (m_IsVisible ? 1 : 0);
                m_EndPosition = m_RectTransform.sizeDelta.y * (m_IsVisible ? 0 : 1);
                m_IsTransitioning = true;
                if (m_CommandField)
                {
                    if (!m_IsVisible)
                    {
                        m_CommandField.DeactivateInputField();
                        EventSystem.current.SetSelectedGameObject(null);
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if (m_IsTransitioning)
            {
                m_CurrentTransitionTime += Time.fixedUnscaledDeltaTime;
                m_CurrentTransitionTime = Mathf.Clamp(m_CurrentTransitionTime, 0.0f, m_TransitionTime);
                Vector3 position = m_RectTransform.anchoredPosition3D;
                position.y = Mathf.Lerp(m_StartPosition, m_EndPosition, m_CurrentTransitionTime / m_TransitionTime);
                m_RectTransform.anchoredPosition3D = position;
                if (m_CurrentTransitionTime >= m_TransitionTime)
                {
                    m_IsTransitioning = false;
                }
            }
        }

        private void OnValidate()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_RectTransform.anchoredPosition3D = new Vector3(0, m_RectTransform.sizeDelta.y * (m_IsVisible ? 0 : 1));
        }
    }
}