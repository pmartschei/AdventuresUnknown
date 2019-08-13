using AdventuresUnknownSDK.Core.Logic.Hangar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AdventuresUnknown.Core.Logic.Hangar
{
    public class UIHangarController : MonoBehaviour
    {
        [SerializeField] private Transform m_LeftTransform = null;
        [SerializeField] private Transform m_RightTransform = null;
        [SerializeField] private Transform m_FullTransform = null;
        [SerializeField] private Transform m_InactiveTransform = null;
        [SerializeField] private UnityEvent m_OnStartEvent = new UnityEvent();

        private UIHangerFrame m_LeftHangarFrame = null;
        private UIHangerFrame m_RightHangarFrame = null;
        private UIHangerFrame m_FullHangarFrame = null;

        #region Properties

        #endregion

        #region Methods
        private void Start()
        {
            DisableAll();
            m_OnStartEvent.Invoke();
        }

        private void DisableAll()
        {
            DisableFrame(m_LeftHangarFrame);
            DisableFrame(m_RightHangarFrame);
            DisableFrame(m_FullHangarFrame);
        }

        public void ShowFrame(UIHangerFrame hangarFrame)
        {
            if (!hangarFrame) return;
            switch (hangarFrame.FramePosition)
            {
                case UIHangerFrame.PositionType.Right:
                    DisableFrame(m_RightHangarFrame);
                    DisableFrame(m_FullHangarFrame);
                    break;
                case UIHangerFrame.PositionType.Left:
                    DisableFrame(m_LeftHangarFrame);
                    DisableFrame(m_FullHangarFrame);
                    break;
                case UIHangerFrame.PositionType.Full:
                    DisableFrame(m_LeftHangarFrame);
                    DisableFrame(m_RightHangarFrame);
                    DisableFrame(m_FullHangarFrame);
                    break;
            }
            EnableFrame(hangarFrame, hangarFrame.FramePosition);
        }
        private void DisableFrame(UIHangerFrame hangarFrame)
        {
            if (!hangarFrame) return;
            hangarFrame.gameObject.SetActive(false);
            hangarFrame.gameObject.transform.SetParent(m_InactiveTransform);
        }
        private void EnableFrame(UIHangerFrame hangarFrame, UIHangerFrame.PositionType positionType)
        {
            if (!hangarFrame) return;
            Transform parent = m_InactiveTransform.transform;
            switch (positionType)
            {
                case UIHangerFrame.PositionType.Left:
                    parent = m_LeftTransform;
                    break;
                case UIHangerFrame.PositionType.Right:
                    parent = m_RightTransform;
                    break;
                case UIHangerFrame.PositionType.Full:
                    parent = m_FullTransform;
                    break;
            }
            hangarFrame.gameObject.SetActive(true);
            hangarFrame.gameObject.transform.SetParent(parent);
            switch (positionType)
            {
                case UIHangerFrame.PositionType.Left:
                    m_LeftHangarFrame = hangarFrame;
                    break;
                case UIHangerFrame.PositionType.Right:
                    m_RightHangarFrame = hangarFrame;
                    break;
                case UIHangerFrame.PositionType.Full:
                    m_FullHangarFrame = hangarFrame;
                    break;
            }
        }
        #endregion
    }
}
