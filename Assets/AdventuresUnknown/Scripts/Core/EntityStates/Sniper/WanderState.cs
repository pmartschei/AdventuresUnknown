using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.EntityStates.Sniper
{
    [CreateAssetMenu]
    public class WanderState : EntityState
    {

        [SerializeField] EntityState m_NextState = null;
        [SerializeField] float m_Radius = 5.0f;
        [SerializeField] private float m_WanderAnglePerFrame = 3.0f;
        [SerializeField] private float m_AvoidanceChangePerFrame = 10.0f;
        [SerializeField] private LayerMask m_LayerAvoidance;

        private EntityController m_EntityController = null;
        private float m_WanderAngle = 0.0f;
        private bool m_AvoidanceLeft = true;
        private float m_AvoidanceTimer = 0.0f;
        #region Properties

        #endregion

        #region Methods
        public override void OnEnter()
        {
            base.OnEnter();
            m_EntityController = gameObject.GetComponent<EntityController>();
            m_WanderAngle = UnityEngine.Random.Range(0.0f, 360.0f);
        }
        public override void Update()
        {
            base.Update();
            bool targetFound = false;
            Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position,m_Radius,~(1 << this.gameObject.layer));

            m_AvoidanceTimer -= Time.deltaTime;
            if (m_AvoidanceTimer <= 0.0f)
            {
                m_AvoidanceLeft = UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f;
                m_AvoidanceTimer = UnityEngine.Random.Range(1.5f, 10.0f);
            }

            List<EntityController> availableTargets = new List<EntityController>();

            foreach(Collider collider in colliders)
            {
                if (Physics.GetIgnoreLayerCollision(this.gameObject.layer, collider.gameObject.layer)) continue;
                EntityController entityController = collider.gameObject.GetComponentInParent<EntityController>();
                if (!entityController) continue;
                if (entityController.Entity.Description.EntityType != EntityType.SpaceShip) continue;
                availableTargets.Add(entityController);
            }
            if (availableTargets.Count > 0)
            {
                m_EntityController.Target = availableTargets[0];
                targetFound = true;
            }
            if (targetFound && m_NextState != null)
            {
                EntityStateMachine.SetNextState(Instantiate(m_NextState));
                return;
            }
            else
            {
                m_WanderAngle += UnityEngine.Random.Range(0, m_WanderAnglePerFrame * 2) - m_WanderAnglePerFrame;
                Vector3 displacement = Quaternion.Euler(0, 0, m_WanderAngle) * new Vector3(0, 1, 0);
                EnemyController enemyController = m_EntityController as EnemyController;

                if (!enemyController) return;

                Stat movementSpeed = enemyController.SpaceShip.Entity.GetStat("core.modtypes.ship.movementspeed");
                Vector3 frontPosition = enemyController.transform.position + (enemyController.LookingDestination+displacement - enemyController.transform.position).normalized * movementSpeed.Calculated * 2;
                
                RaycastHit hitInfo;
                if (Physics.Linecast(enemyController.transform.position,frontPosition,out hitInfo, m_LayerAvoidance.value))
                {
                    if (m_AvoidanceLeft)
                    {
                        m_WanderAngle += m_AvoidanceChangePerFrame;
                    }
                    else
                    {
                        m_WanderAngle -= m_AvoidanceChangePerFrame;
                    }
                }
                enemyController.AimTowardsPosition(frontPosition);
                enemyController.MoveTowardsPosition(frontPosition);
            }
        }
        public override void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.gameObject.transform.position, m_Radius);
            Vector3 displacement = Quaternion.Euler(0, 0, m_WanderAngle) * new Vector3(0, 1, 0);
            Gizmos.DrawSphere(m_EntityController.LookingDestination + displacement,0.1f);
        }
        #endregion
    }
}
