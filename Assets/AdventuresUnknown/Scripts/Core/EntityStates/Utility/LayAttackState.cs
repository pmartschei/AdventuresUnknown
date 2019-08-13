using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.EntityStates.Utility
{
    [CreateAssetMenu]
    public class LayAttackState : EntityState
    {

        private EntityController m_EntityController = null;

        #region Properties

        #endregion

        #region Methods
        public override void OnEnter()
        {
            base.OnEnter();
            m_EntityController = gameObject.GetComponent<EntityController>();
        }
        public override void Update()
        {
            base.Update();

            EnemyController enemyController = m_EntityController as EnemyController;

            if (!enemyController) return;

            if (enemyController.HasCooldown()) return;

            enemyController.Attack();
        }
        #endregion
    }
}
