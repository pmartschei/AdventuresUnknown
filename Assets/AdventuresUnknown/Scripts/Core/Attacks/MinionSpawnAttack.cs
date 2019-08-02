using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions.ActionObjects;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Attacks
{
    public class MinionSpawnAttack : GenericProjectileAttack
    {
        [SerializeField] private Enemy m_Minion = null;
        [SerializeField] private bool m_IsMinion = false;
        #region Properties

        #endregion

        #region Methods

        public override IEnumerator Activate(EntityController controller, Entity spaceShip, Vector3 origin, Vector3 destination)
        {
            yield return base.Activate(controller, spaceShip, origin, destination);
            if (m_Minion == null) yield return null;
            EnemyModel enemyModel = LevelManager.SpawnEnemy(m_Minion, controller.transform.position);
            enemyModel.transform.MoveToLayer(controller.gameObject.layer);
            if (m_IsMinion)
            {
                enemyModel.EnemyActiveGemContainer.EntityStats.Entity.Description.IsMinion = true;
                enemyModel.EnemyActiveGemContainer.EntityStats.Entity.Description.Parent = controller.gameObject;
            }
            yield return null;
        }
        
        #endregion
    }
}
