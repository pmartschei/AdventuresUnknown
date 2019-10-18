using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.Weapons;
using AdventuresUnknownSDK.Core.Logic.Attacks;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions.ActionObjects;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
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
        [SerializeField] private ModTypeIdentifier m_MinionCountModType = null;
        #region Properties

        #endregion

        #region Methods
        public override IEnumerator Activate(ActivationParameters activationParameters)
        {
            PreActivate(activationParameters);
            EntityController controller = activationParameters.EntityController;
            Entity stats = activationParameters.Stats;
            if (m_Minion == null) yield return null;
            if (m_IsMinion && controller.SpaceShip.EntityController == null) yield return null; //only spawn minions if parent alive

            EnemyModel enemyModel = LevelManager.SpawnEnemy(m_Minion, controller.Head.position);
            enemyModel.transform.MoveToLayer(controller.gameObject.layer);
            if (m_IsMinion)
            {
                enemyModel.EntityController.Entity.Description.IsMinion = true;
                enemyModel.EntityController.Entity.Description.Parent = controller.SpaceShip;
                enemyModel.EntityController.Entity.Description.MinionCountModType = m_MinionCountModType.Identifier;
                controller.SpaceShip.Entity.AddMinion(m_MinionCountModType.Identifier, enemyModel.EntityController.Entity);
            }
        }
        public override float GetAIPriority(EntityController controller, Entity stats, EntityController target)
        {
            Stat stat = controller.Entity.GetStat(m_MinionCountModType.Identifier);
            return (1.0f - stat.Percentage);
        }
        #endregion
    }
}
