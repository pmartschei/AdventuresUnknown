using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions.ActionObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Attacks
{
    public class MineAttack : GenericAttack
    {
        [SerializeField] private GenericAttack m_AttackOnCollisionOrDuration = null;

        #region Properties

        #endregion

        #region Methods
        public override IEnumerator Activate(EntityController controller, Entity stats, Vector3 origin, Vector3 destination)
        {
            yield return base.Activate(controller, stats, origin, destination);
            SpawnSingleInstance(controller, stats, origin, destination);
        }
        protected override void AttackUpdate()
        {

        }
        protected override void DestroyAttack()
        {
            base.DestroyAttack();
            Entity.Notify(ActionTypeManager.AttackGeneration);
            ExecuteCoroutine(m_AttackOnCollisionOrDuration.Activate(this, Entity, this.transform.position, this.LookingDestination));
        }
        protected override void OnAttackHit(HitContext hitContext)
        {
            base.OnAttackHit(hitContext);
            DestroyAttack();
        }
        #endregion
    }
}
