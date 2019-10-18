using AdventuresUnknownSDK.Core.Entities.StateMachine.EntityStates;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.EntityStates.Exploder
{
    [CreateAssetMenu]
    public class ExploderAttackState : MoveTowardsTargetState
    {
        [SerializeField] private float m_Distance = 1.0f;
        public override void Update()
        {
            base.Update();
            if (CommonComponents.EntityController.Target == null) return;
            float sqrDistance = Vector3.SqrMagnitude(CommonComponents.EntityController.Head.position - CommonComponents.EntityController.Target.Head.position);
            if (sqrDistance < m_Distance * m_Distance)
            {
                CommonComponents.EntityController.Animator.SetBool("Dead", true);
            }
        }
    }
}
