using AdventuresUnknown.Core.Visuals;
using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

namespace AdventuresUnknown.Scripts.Core.Visuals
{
    public class ParticleSystemStartColorVisualChanger : VisualChanger<MinMaxGradient>
    { 
        [SerializeField] private ParticleSystem m_ParticleSystem = null;

        public override void OnVisualFriendly()
        {
            ChangeColors(FriendlyT1);
        }

        private void ChangeColors(MinMaxGradient gradient)
        {
            if (!m_ParticleSystem) return;
            MainModule main = m_ParticleSystem.main;
            main.startColor = gradient;
            int particles = m_ParticleSystem.particleCount;
            Particle[] particlesArray = new Particle[particles];
            particles = m_ParticleSystem.GetParticles(particlesArray);
            for (int i = 0; i < particles; i++)
            {
                particlesArray[i].startColor = gradient.color;
            }
            m_ParticleSystem.SetParticles(particlesArray);
        }
        public override void OnVisualHostile()
        {
            ChangeColors(HostileT1);
        }
    }
}
