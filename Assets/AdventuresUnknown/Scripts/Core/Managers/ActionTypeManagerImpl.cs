using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Managers
{
    public class ActionTypeManagerImpl : ActionTypeManager
    {
        [SerializeField] private ActionType m_CalculationAction = null;
        [SerializeField] private ActionType m_TickAction = null;
        [SerializeField] private ActionType m_DeathAction = null;
        [SerializeField] private ActionType m_PostDeathAction = null;
        [SerializeField] private ActionType m_SpawnAction = null;
        [SerializeField] private ActionType m_BlockAction = null;
        [SerializeField] private ActionType m_HitGenerationOffensiveAction = null;
        [SerializeField] private ActionType m_HitGenerationDefensiveAction = null;
        [SerializeField] private ActionType m_HitCalculationAction = null;
        [SerializeField] private ActionType m_HitApplyAction = null;
        [SerializeField] private ActionType m_AttackCooldownGenerationAction = null;
        [SerializeField] private ActionType m_AttackCooldownApplyAction = null;
        [SerializeField] private ActionType m_AttackApplyAction = null;
        [SerializeField] private ActionType m_AttackGenerationAction = null;

        #region Properties
        protected override ActionType CalculationImpl { get => m_CalculationAction; set => m_CalculationAction = value; }
        protected override ActionType TickImpl { get => m_TickAction; set => m_TickAction = value; }
        protected override ActionType DeathImpl { get => m_DeathAction; set => m_DeathAction = value; }
        protected override ActionType PostDeathImpl { get => m_PostDeathAction; set => m_PostDeathAction = value; }
        protected override ActionType SpawnImpl { get => m_SpawnAction; set => m_SpawnAction = value; }
        protected override ActionType BlockImpl { get => m_BlockAction; set => m_BlockAction = value; }
        protected override ActionType HitGenerationOffensiveImpl { get => m_HitGenerationOffensiveAction; set => m_HitGenerationOffensiveAction = value; }
        protected override ActionType HitGenerationDefensiveImpl { get => m_HitGenerationDefensiveAction; set => m_HitGenerationDefensiveAction = value; }
        protected override ActionType HitCalculationImpl { get => m_HitCalculationAction; set => m_HitCalculationAction = value; }
        protected override ActionType HitApplyImpl { get => m_HitApplyAction; set => m_HitApplyAction = value; }
        protected override ActionType AttackCooldownGenerationImpl { get => m_AttackCooldownGenerationAction; set => m_AttackCooldownGenerationAction = value; }
        protected override ActionType AttackCooldownApplyImpl { get => m_AttackCooldownApplyAction; set => m_AttackCooldownApplyAction = value; }
        protected override ActionType AttackApplyImpl { get => m_AttackApplyAction; set => m_AttackApplyAction = value; }
        protected override ActionType AttackGenerationImpl { get => m_AttackGenerationAction; set => m_AttackGenerationAction = value; }
        #endregion

        #region Methods

        #endregion
    }
}
