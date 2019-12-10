using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShrinkOverSpawnCount : MonoBehaviour
{
    [SerializeField] private SpawnObject m_SpawnObject = null;
    [SerializeField] private Transform m_Transform = null;
    [SerializeField] private Vector3 m_StartingScale = Vector3.one;
    [SerializeField] private Vector3 m_FinishingScale = Vector3.zero;


    private int m_CurrentEnemies = 0;
    private float m_TimeLerped = 0.0f;
    private bool doLerp;

    private void Start()
    {
        if (!m_SpawnObject)
        {
            this.enabled = false;
            GameConsole.LogWarningFormat("Disabled Component {1} without SpawnObject reference",this);
            return;
        }
    }
    void Update()
    {
        Transform transform = m_Transform == null ? this.transform : m_Transform;


        if (m_CurrentEnemies != m_SpawnObject.SpawnedEnemies)
        {
            doLerp = true;
        }

        if (doLerp)
            m_TimeLerped += Time.deltaTime;

        if (m_TimeLerped >= m_SpawnObject.SpawnDelay)
        {
            m_CurrentEnemies = m_SpawnObject.SpawnedEnemies;
            doLerp = false;
            m_TimeLerped = 0.0f;
        }

        Vector3 scale = Vector3.Lerp(m_StartingScale, m_FinishingScale, (m_CurrentEnemies + m_TimeLerped / m_SpawnObject.SpawnDelay) / (float)m_SpawnObject.Count);

        transform.localScale = scale;
    }
}
