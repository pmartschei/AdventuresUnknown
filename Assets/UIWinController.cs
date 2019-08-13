using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinController : MonoBehaviour
{

    [SerializeField] private GameObject m_Header = null;
    [SerializeField] private GameObject m_Overlay = null;
    [SerializeField] private GameObject m_AdditionalObjects = null;

    private void OnEnable()
    {
        LevelManager.OnSuccess.AddListener(OnSuccess);
    }
    private void OnDisable()
    {
        LevelManager.OnSuccess.RemoveListener(OnSuccess);
    }

    private void OnSuccess()
    {
        StartCoroutine(OnSuccessCoroutine());
    }

    private IEnumerator OnSuccessCoroutine()
    {
        
        yield return new WaitForSecondsRealtime(3.0f);

        if (m_Header)
            m_Header.SetActive(true);
        
        yield return new WaitForSecondsRealtime(5.0f);

        ShowOverlay();

        if (m_AdditionalObjects)
            m_AdditionalObjects.SetActive(true);
    }

    public void ShowOverlay()
    {
        if (m_Overlay)
            m_Overlay.SetActive(true);
    }
    public void HideOverlay()
    {
        if (m_Overlay)
            m_Overlay.SetActive(false);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
