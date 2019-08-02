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
    // Update is called once per frame
    void Update()
    {
        ProceduralLevel level = LevelManager.CurrentLevel as ProceduralLevel;
        if (level != null)
        {
            if (m_WaveText)
            {
                m_WaveText.SetText(level.CurrentWave);
            }
            if (m_TimerText)
            {
                m_TimerText.SetText(level.CurrentWaveTimer);
            }
        }
        else
        {
            Debug.Log("Level is null");
        }
    }
}
