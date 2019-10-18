using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.StateMachine.EntityStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.EntityStates.Sniper
{
    [CreateAssetMenu]
    public class SniperAttackState : AttackState
    {
        [SerializeField] private float m_AimingDuration = 1.0f;
        [SerializeField] private float m_LockPosition = 0.75f;
        [SerializeField] private float m_AttackPosition = 0.75f;
        [SerializeField] private GameObject m_TargetSpritePrefab = null;
        [SerializeField] private LineRenderer m_LineRendererPrefab = null;
        private float m_CurrentAimingDuration = 0.0f;
        private GameObject m_TargetSpriteInstance = null;
        private LineRenderer m_LineRendererInstance = null;
        private Vector3 targetPosition = Vector3.zero;
        #region Properties

        #endregion

        #region Methods
        public override void OnEnter()
        {
            base.OnEnter();
            m_TargetSpriteInstance = Instantiate(m_TargetSpritePrefab, this.gameObject.transform);
            m_LineRendererInstance = Instantiate(m_LineRendererPrefab, this.gameObject.transform);
            m_TargetSpriteInstance.SetActive(false);
            m_LineRendererInstance.gameObject.SetActive(false);
            m_LineRendererInstance.positionCount = 2;
            //Entity entity = CommonComponents.AttackController.GetEntity(SkillIndex);
            //m_AimingDuration = entity.GetStat("core.modtypes.skills.casttime").Calculated;
            //m_LockPosition = m_AimingDuration * 0.5f;
            //m_AttackPosition = m_AimingDuration * 0.75f;
        }
        protected override void NotNearTarget()
        {
            base.NotNearTarget();
            m_TargetSpriteInstance.SetActive(false);
            m_LineRendererInstance.gameObject.SetActive(false);
            m_CurrentAimingDuration = 0.0f;
        }
        protected override void TargetAttack()
        {
            if (CommonComponents.AttackController.IsNearTarget(SkillIndex) || m_CurrentAimingDuration > m_LockPosition)
            {
                NearTarget();
            }
            else
            {
                NotNearTarget();
            }
        }
        protected override void NearTarget()
        {
            if (!CommonComponents.AttackController.HasCooldown(SkillIndex) || m_CurrentAimingDuration > 0.0f)
            {
                if (CommonComponents.TranslationalController != null)
                    CommonComponents.TranslationalController.StopMove();
                m_LineRendererInstance.SetPosition(0, gameObject.transform.position);
                m_CurrentAimingDuration += Time.deltaTime;

                if (m_CurrentAimingDuration <= m_LockPosition)
                {
                    targetPosition = CommonComponents.EntityController.Target.transform.position;
                }

                if (CommonComponents.RotationalController.AimTowardsPosition(targetPosition) && m_CurrentAimingDuration >= m_AttackPosition)
                {
                    DoAttack();
                    m_CurrentAimingDuration = 0.0f;
                    Entity entity = CommonComponents.AttackController.GetEntity(SkillIndex);
                    m_AimingDuration = entity.GetStat("core.modtypes.skills.casttime").Calculated;
                    m_LockPosition = m_AimingDuration * 0.5f;
                    m_AttackPosition = m_AimingDuration * 0.75f;
                }
                if (m_CurrentAimingDuration > m_AimingDuration)
                {
                    m_CurrentAimingDuration -= m_AimingDuration;
                }
                m_TargetSpriteInstance.SetActive(true);
                m_LineRendererInstance.gameObject.SetActive(true);
                m_TargetSpriteInstance.transform.position = targetPosition;
                m_LineRendererInstance.SetPosition(1, targetPosition);
            }
            else
            {
                m_TargetSpriteInstance.SetActive(false);
                m_LineRendererInstance.gameObject.SetActive(false);
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            Destroy(m_TargetSpriteInstance.gameObject);
            Destroy(m_LineRendererInstance.gameObject);
        }
        #endregion
    }
}
