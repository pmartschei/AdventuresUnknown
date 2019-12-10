using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Objects.Mods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AdventuresUnknown.Scripts.Core.Enemies
{
    public class DistributedSpawnObject : SpawnObject
    {
        [SerializeField] private float m_Radius = 1.0f;
        [SerializeField] private int m_Retries = 10;
        protected override IEnumerator Spawn()
        {
            yield return new WaitForSeconds(SpawnDelay);
            for (int i = 0; i < Count; i++)
            {
                NavMeshPath path = new NavMeshPath();
                for (int retry = 0; retry < m_Retries; retry++)
                {
                    Vector3 direction = UnityEngine.Random.insideUnitSphere * m_Radius;
                    direction.y = 0;
                    Vector3 position = this.transform.position + direction;
                    if (!NavMesh.CalculatePath(position, Vector3.zero, NavMesh.AllAreas, path))
                        continue;

                    if (path.status != NavMeshPathStatus.PathComplete) continue;

                    EnemyModel em = LevelManager.SpawnEnemy(Enemy, position);
                    em.EntityBehaviour.Entity.GetStat("core.modtypes.utility.level").AddStatModifier(new StatModifier(EnemyLevel, CalculationType.Flat, this));
                    CooldownManager.AddCooldown(em.EntityController, UnityEngine.Random.Range(CooldownMin, CooldownMax));
                    m_EnemySpawnEvent?.Invoke(em);
                    SpawnedEnemies++;
                    break;
                }
                yield return new WaitForSeconds(SpawnDelay);
            }
            IsSpawning = false;
            GameObject.Destroy(this.gameObject);
            yield break;
        }
    }
}
