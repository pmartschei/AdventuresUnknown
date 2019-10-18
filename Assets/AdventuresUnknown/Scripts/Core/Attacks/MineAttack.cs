using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.Weapons;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
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
        #region Properties

        #endregion

        #region Methods
        public override IEnumerator Activate(ActivationParameters activationParameters)
        {
            PreActivate(activationParameters);
            EntityController controller = activationParameters.EntityController;
            Vector3 destination = controller.LookingDestination;
            Vector3 origin = controller.Head.position;
            SpawnSingleInstance(activationParameters, origin, destination);
            yield return null;
        }
        protected override void DestroyAttack()
        {
            base.DestroyAttack();
            ActivateSecondaryActiveGem(0);
        }
        protected override void OnAttackHit(HitContext hitContext)
        {
            base.OnAttackHit(hitContext);
            DestroyAttack();
        }
        #endregion
    }
}
