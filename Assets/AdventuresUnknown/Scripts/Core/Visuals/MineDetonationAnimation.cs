using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AdventuresUnknown.Core.Visuals
{
    public class MineDetonationAnimation : VisualChanger<Color,Color>
    {
        [SerializeField] private EntityController m_EntityController = null;
        [SerializeField] private Renderer m_Renderer = null;

        private MaterialPropertyBlock m_PropertyBlock;
        private void Awake()
        {
            m_PropertyBlock = new MaterialPropertyBlock();
        }
        public override void OnVisualFriendly()
        {
            ChangeColors(FriendlyT1, FriendlyT2);
        }
        public override void OnVisualHostile()
        {
            ChangeColors(HostileT1, HostileT2);
        }
        private void ChangeColors(Color startingColor, Color detonationColor)
        {
            if (!m_Renderer) return;
            if (!m_EntityController) return;

            m_Renderer.GetPropertyBlock(m_PropertyBlock);

            Color color = Color.Lerp(startingColor, detonationColor, 1 - m_EntityController.Entity.GetStat("core.modtypes.skills.duration").Percentage);

            m_PropertyBlock.SetColor("_Color", color);

            m_PropertyBlock.SetColor("_EmissionColor", color);

            m_Renderer.SetPropertyBlock(m_PropertyBlock);
        }
    }
}
