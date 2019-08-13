using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnLevelFail : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup = null;
    private void OnEnable()
    {
        LevelManager.OnFail.AddListener(OnFail);
    }

    private void OnFail()
    {
        if (m_CanvasGroup)
            m_CanvasGroup.Show();
    }

    private void OnDisable()
    {
        LevelManager.OnFail.RemoveListener(OnFail);
    }
}
