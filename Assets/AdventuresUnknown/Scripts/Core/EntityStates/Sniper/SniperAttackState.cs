using AdventuresUnknownSDK.Core.Entities.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.EntityStates.Sniper
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
        }
        public override void Update()
        {
            base.Update();
            EnemyController enemyController = EntityController as EnemyController;

            if (!enemyController || enemyController.Target == null) return;

            m_LineRendererInstance.SetPosition(0, gameObject.transform.position);
            
            if (!enemyController.IsNearTarget() && m_CurrentAimingDuration <= m_LockPosition)
            {
                enemyController.MoveTowardsTarget();
                enemyController.AimTowardsTarget();
                m_TargetSpriteInstance.SetActive(false);
                m_LineRendererInstance.gameObject.SetActive(false);
                m_CurrentAimingDuration = 0.0f;
            }
            //if too close (0.3 up to 0.6) move back but try to aim
            else
            {
                if (!enemyController.HasCooldown() || m_CurrentAimingDuration > 0.0f)
                {
                    m_CurrentAimingDuration += Time.deltaTime;


                    if (m_CurrentAimingDuration <= m_LockPosition)
                    {
                        targetPosition = enemyController.Target.transform.position;
                    }
                    
                    enemyController.AimTowardsPosition(targetPosition);

                    if (m_CurrentAimingDuration >= m_AttackPosition && !enemyController.HasCooldown())
                    {
                        m_CurrentAimingDuration = 0.0f;
                        enemyController.Attack();
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
