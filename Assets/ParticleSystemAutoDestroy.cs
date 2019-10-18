using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] m_ParticleSystems;
    [SerializeField] private bool m_ShouldDestroy = true;

    public bool ShouldDestroy { get => m_ShouldDestroy; set => m_ShouldDestroy = value; }

    public void Update()
    {
        if (!m_ShouldDestroy) return;
        if (m_ParticleSystems != null)
        {
            foreach(ParticleSystem particleSystem in m_ParticleSystems)
            {
                if (!particleSystem) continue;
                if (particleSystem.IsAlive()) return; //If one particle System is alive just return
            }
        }
        Destroy(this.gameObject); //If all particle Systems are dead destroy this gameobject
    }
}
