using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnLevelSuccess : MonoBehaviour
{

    [SerializeField] private CanvasGroup m_CanvasGroup = null;
    private void OnEnable()
    {
        LevelManager.OnSuccess.AddListener(OnSuccess);
    }

    private void OnSuccess()
    {
        if (m_CanvasGroup)
            m_CanvasGroup.Show();
    }

    private void OnDisable()
    {
        LevelManager.OnSuccess.RemoveListener(OnSuccess);
    }
}
