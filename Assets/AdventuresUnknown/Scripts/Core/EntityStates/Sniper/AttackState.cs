using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.EntityStates.Sniper
{
    [CreateAssetMenu]
    public class AttackState : EntityState
    {
        [SerializeField] private float m_KeepTargetDistance = 5.0f;
        private EntityController m_EntityController = null;
        private float m_SqrKeepTargetDistance = 0.0f;

        #region Properties
        public EntityController EntityController { get => m_EntityController; set => m_EntityController = value; }
        #endregion

        #region Methods
        public override void OnEnter()
        {
            base.OnEnter();
            m_EntityController = gameObject.GetComponent<EntityController>();
            m_SqrKeepTargetDistance = m_KeepTargetDistance * m_KeepTargetDistance;
        }
        public override void Update()
        {
            base.Update();

            if (m_EntityController.Target == null)
            {
                EntityStateMachine.SetNextStateToMain();
                return;
            }

            float distance = Vector3.SqrMagnitude(gameObject.transform.position - m_EntityController.Target.transform.position);
            if (distance > m_SqrKeepTargetDistance)
            {
                m_EntityController.Target = null;
                EntityStateMachine.SetNextStateToMain();
                return;
            }
        }
        public override void OnDrawGizmosSelected()
        {
            if (m_EntityController.Target == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_EntityController.Target.transform.position, 0.5f);
        }
        #endregion
    }
}
