using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.Weapons;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Attacks
{
    public class NovaAttack : GenericProjectileAttack
    {


        #region Properties

        #endregion

        #region Methods
        public override IEnumerator Activate(ActivationParameters activationParameters)
        {
            PreActivate(activationParameters);

            EntityController controller = activationParameters.EntityController;
            Entity stats = activationParameters.Stats;
            Stat projectiles = stats.GetStat("core.modtypes.skills.projectiles");
            Vector3[] destinations = GenerateCircularPositions(controller.LookingDestination - controller.transform.position, controller.transform.position, (int)projectiles.Calculated,0,360.0f);

            for (int i = 0; i < projectiles.Calculated; i++)
            {
                SpawnSingleInstance(activationParameters, controller.transform.position, destinations[i]);
            }
            yield return null;
        }
        #endregion
    }
}
