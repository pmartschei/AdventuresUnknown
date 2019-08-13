using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Logic.Attacks;
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
        public override IEnumerator Activate(EntityController controller, Entity stats, Vector3 origin, Vector3 destination)
        {
            yield return base.Activate(controller, stats, origin, destination);
            Stat projectiles = stats.GetStat("core.modtypes.skills.projectiles");
            Vector3[] destinations = GenerateCircularPositions(controller.LookingDestination - controller.transform.position, controller.transform.position, (int)projectiles.Calculated,0,360.0f);

            for (int i = 0; i < projectiles.Calculated; i++)
            {
                SpawnSingleInstance(controller, stats, controller.transform.position, destinations[i]);
            }
        }
        #endregion
    }
}
