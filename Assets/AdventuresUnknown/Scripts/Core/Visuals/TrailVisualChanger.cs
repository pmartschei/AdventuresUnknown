using AdventuresUnknown.Core.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Visuals
{
    public class TrailVisualChanger : VisualChanger<Gradient>
    {
        [SerializeField] private TrailRenderer m_TrailRenderer = null;

        public override void OnVisualFriendly()
        {
            if (!m_TrailRenderer) return;
            m_TrailRenderer.colorGradient = FriendlyT1;
        }
        public override void OnVisualHostile()
        {
            if (!m_TrailRenderer) return;
            m_TrailRenderer.colorGradient = HostileT1;
        }
    }
}
