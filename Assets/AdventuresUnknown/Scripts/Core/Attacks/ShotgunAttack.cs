using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Logic.Attacks;
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
    public class ShotgunAttack : GenericProjectileAttack
    {
        [SerializeField] private float m_MinimumProjectileSpeedModifier = 0.75f;
        #region Properties

        #endregion

        #region Methods

        public override IEnumerator Activate(EntityController controller, Entity spaceShip, Vector3 origin, Vector3 destination)
        {
            yield return base.Activate(controller, spaceShip, origin, destination);
            Stat projectiles = spaceShip.GetStat("core.modtypes.skills.projectiles");
            Vector3[] startPositions = GenerateRandomCircularPositions(
                controller.LookingDestination - controller.transform.position,
                controller.transform.position,
                (int)projectiles.Calculated,
                -25.0f, 50.0f);
            Stat stat = spaceShip.GetStat("core.modtypes.skills.projectilespeed");
            float maxProjectileSpeed = stat.Calculated;
            float minProjectileSpeed = maxProjectileSpeed * m_MinimumProjectileSpeedModifier;
            controller.gameObject.GetComponent<Rigidbody>().AddForce(-(controller.LookingDestination-controller.transform.position).normalized * 100.0f);
            for (int i = 0; i < projectiles.Calculated; i++)
            {
                stat.RemoveAllStatModifiers();
                stat.AddStatModifier(new StatModifier(UnityEngine.Random.Range(minProjectileSpeed, maxProjectileSpeed), AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Flat, this));
                SpawnSingleInstance(controller, spaceShip, controller.transform.position, startPositions[i]);
            }
            yield return null;
        }
        
        #endregion
    }
}
