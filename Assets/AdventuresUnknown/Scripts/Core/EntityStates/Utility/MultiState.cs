using AdventuresUnknownSDK.Core.Entities.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.EntityStates.Utility
{
    [CreateAssetMenu]
    public class MultiState : EntityState
    {
        [SerializeField] EntityState[] m_EntityStates = null;
        
        private EntityState[] m_InstancedEntityStates = null;
        #region Properties

        #endregion

        #region Methods
        public override void OnEnter()
        {
            base.OnEnter();

            m_InstancedEntityStates = new EntityState[m_EntityStates.Length];
            for(int i = 0; i < m_EntityStates.Length; i++)
            {
                m_InstancedEntityStates[i] = Instantiate(m_EntityStates[i]);
                m_InstancedEntityStates[i].EntityStateMachine = this.EntityStateMachine;
            }
            foreach(EntityState entityState in m_InstancedEntityStates)
            {
                if (entityState == null) continue;
                entityState.OnEnter();
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            foreach (EntityState entityState in m_InstancedEntityStates)
            {
                if (entityState == null) continue;
                entityState.OnExit();
            }
        }
        public override void Update()
        {
            base.Update();
            foreach (EntityState entityState in m_InstancedEntityStates)
            {
                if (entityState == null) continue;
                entityState.Update();
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            foreach (EntityState entityState in m_InstancedEntityStates)
            {
                if (entityState == null) continue;
                entityState.FixedUpdate();
            }
        }
        #endregion
    }
}
