using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions.ActionObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Attacks
{
    public class CustomBarrageAttack : GenericProjectileAttack
    {
        [SerializeField] private GenericAttack m_AttackOnDestroy = null;
        #region Properties

        #endregion

        #region Methods

        public override IEnumerator Activate(EntityController controller, Entity spaceShip, Vector3 origin, Vector3 destination)
        {
            yield return base.Activate(controller, spaceShip, origin, destination);
            Stat projectiles = spaceShip.GetStat("core.modtypes.skills.projectiles");
            Vector3[] circularPositions;
            float maxAngle = 160.0f;
            float minAngle = Mathf.Min(5.0f * projectiles.Calculated,maxAngle);
            float angle = Vector3.Angle(destination - origin, destination + controller.gameObject.transform.right * 0.5f - origin) * projectiles.Calculated;

            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            Vector3[] startPositions = GenerateCircularPositions(controller.LookingDestination - controller.transform.position, controller.transform.position, (int)projectiles.Calculated, -angle/2.0f, angle);
            circularPositions = GenerateVolleyPositions(controller.LookingDestination, controller.transform.position, (int)projectiles.Calculated, 1.0f,false);
            for (int i = 0; i < projectiles.Calculated; i++)
            {
                //do circle etc
                SpawnSingleInstance(controller, spaceShip, controller.transform.position, startPositions[i]);
            }
            yield return null;
        }
        protected override IEnumerator OnAttackDestroy()
        {
            this.LookingDestination = Vector3.zero;
            Entity.Notify(ActionTypeManager.AttackGeneration);
            yield return m_AttackOnDestroy.Activate(this, Entity, this.transform.position, Vector3.zero);
        }
        #endregion
    }
}
