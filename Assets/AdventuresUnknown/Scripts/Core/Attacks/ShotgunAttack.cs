using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.Weapons;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
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
    public class ShotgunAttack : BasicProjectileAttack
    {
        [SerializeField] private float m_MinimumProjectileSpeedModifier = 0.75f;
        #region Properties

        #endregion

        #region Methods
        public override void SpawnProjectiles(ActivationParameters activationParameters, Vector3 origin, Vector3 destination, int projectiles, Muzzle muzzle = null)
        {
            EntityController controller = activationParameters.EntityController;
            Entity stats = activationParameters.Stats;

            Vector3[] startPositions = GenerateRandomCircularPositions(
                controller.LookingDestination - controller.transform.position,
                controller.transform.position,
                projectiles,
                -25.0f, 50.0f);
            Stat stat = stats.GetStat("core.modtypes.skills.projectilespeed");
            float maxProjectileSpeed = stat.Calculated;
            float minProjectileSpeed = maxProjectileSpeed * m_MinimumProjectileSpeedModifier;
            for (int i = 0; i < projectiles; i++)
            {
                stat.RemoveAllStatModifiers();
                stat.AddStatModifier(new StatModifier(UnityEngine.Random.Range(minProjectileSpeed, maxProjectileSpeed), AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Flat, this));
                SpawnSingleInstance(activationParameters, controller.transform.position, startPositions[i]);
            }
        }
        #endregion
    }
}
