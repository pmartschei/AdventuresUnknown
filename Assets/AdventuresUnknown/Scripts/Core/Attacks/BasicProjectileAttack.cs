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

namespace AdventuresUnknown.Scripts.Core.Attacks
{
    public class BasicProjectileAttack : GenericProjectileAttack
    {

        [SerializeField] private float m_MaxAngle = 160.0f;
        [SerializeField] private float m_MinAngle = 5.0f;
        [SerializeField] private bool m_RotateThroughMuzzles = false;
        [SerializeField] private string m_UniqueIdentifier = "";

        #region Properties

        #endregion

        #region Methods
        public override IEnumerator Activate(ActivationParameters activationParameters)
        {
            PreActivate(activationParameters);

            EntityController controller = activationParameters.EntityController;
            Entity stats = activationParameters.Stats;
            Muzzle[] muzzles = activationParameters.Muzzles;

            Stat projectiles = stats.GetStat("core.modtypes.skills.projectiles");

            int projectilePerMuzzle = (int)projectiles.Calculated;
            projectilePerMuzzle = Mathf.Max(projectilePerMuzzle, 1);
            if (muzzles.Length > 0 && !m_RotateThroughMuzzles)
            {
                int moduloProjectiles = projectilePerMuzzle % muzzles.Length;
                projectilePerMuzzle = projectilePerMuzzle / muzzles.Length;

                for (int i = 0; i < muzzles.Length; i++)
                {
                    SpawnProjectiles(activationParameters,
                        controller.transform.position,
                        controller.LookingDestination,
                        projectilePerMuzzle + (moduloProjectiles > i ? 1 : 0),
                        muzzles[i]);
                }
            }
            else
            {
                Muzzle muzzle = null;
                int index = controller.SpaceShip.Entity.GetObject<int>(m_UniqueIdentifier);
                index++;
                if (index >= muzzles.Length) index = 0;
                controller.SpaceShip.Entity.AddObject(m_UniqueIdentifier, index, true);
                if (muzzles.Length > 0)
                    muzzle = muzzles[index];
                SpawnProjectiles(activationParameters, controller.transform.position, controller.LookingDestination, projectilePerMuzzle,muzzle);
            }
            yield return null;
        }

        public virtual void SpawnProjectiles(ActivationParameters activationParameters, Vector3 origin, Vector3 destination, int projectiles,Muzzle muzzle = null)
        {
            EntityController controller = activationParameters.EntityController;

            float minAngle = Mathf.Min(m_MinAngle * projectiles, m_MaxAngle);
            float angle = Vector3.Angle(destination - origin, destination + controller.gameObject.transform.right * 0.5f - origin) * projectiles;
            angle = Mathf.Clamp(angle, minAngle, m_MaxAngle);
            Vector3[] destinations = GenerateCircularPositions(controller.LookingDestination - controller.transform.position, controller.transform.position, projectiles, -angle / 2.0f, angle);
            for (int i = 0; i < projectiles; i++)
            {
                SpawnSingleInstance(activationParameters, controller.transform.position, destinations[i],muzzle);
            }
        }
        #endregion
    }
}
