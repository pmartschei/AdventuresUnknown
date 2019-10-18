using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevels : MonoBehaviour
{
    [SerializeField] private JourneyDataIdentifier m_JourneyData = null;

    private void Start()
    {
        if (m_JourneyData.ConsistencyCheck())
        {
            m_JourneyData.Object.GenerateNextLevels();
        }
    }
}
