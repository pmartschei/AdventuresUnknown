using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWaveDisplay : MonoBehaviour
{
    [SerializeField] private IGameText m_WaveText = null;
    [SerializeField] private IGameText m_TimerText = null;
    [SerializeField] private IGameText m_RemainingText = null;
    // Update is called once per frame
    void Update()
    {
        ProceduralLevel level = LevelManager.CurrentLevel as ProceduralLevel;
        if (level != null)
        {
            bool flag = level.NextWave == null;
            if (m_WaveText)
            {
                m_WaveText.SetText(level.CurrentWave);
            }
            if (m_TimerText)
            {
                m_TimerText.gameObject.SetActive(!flag);
                m_TimerText.SetText(level.CurrentWaveTimer);
            }
            if (m_RemainingText)
            {
                m_RemainingText.gameObject.SetActive(flag);
                m_RemainingText.SetText(level.RemainingEnemies);
            }
        }
        else
        {
            Debug.Log("Level is null");
        }
    }
}
