using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.Enemies;
using AdventuresUnknownSDK.Core.Objects.Levels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.AdventuresUnknown.Scripts.Core.Managers
{
    public class LevelManagerImpl : LevelManager
    {

        [SerializeField] private LevelGeneratorDescription m_LevelGeneratorDescription = null;

        private Level m_CurrentLevel = null;
        private UnityEvent m_SuccessEvent = new UnityEvent();
        private UnityEvent m_FailEvent = new UnityEvent();


        #region Properties
        protected override Level CurrentLevelImpl { get => m_CurrentLevel; set => m_CurrentLevel = value; }
        protected override UnityEvent OnSuccessImpl => m_SuccessEvent;
        protected override UnityEvent OnFailImpl => m_FailEvent;
        #endregion

        #region Methods

        protected override Level GenerateLevelImpl(int difficulty)
        {
            return GenerateProceduralLevel(difficulty);
        }

        protected override void SetLevelGeneratorImpl(LevelGeneratorDescription levelGeneratorDescription)
        {
            m_LevelGeneratorDescription = levelGeneratorDescription;
        }

        private ProceduralLevel GenerateProceduralLevel(int difficulty)
        {
            ProceduralLevel proceduralLevel = new ProceduralLevel(difficulty);

            GenerateFromSeed(proceduralLevel,Random.Range(int.MinValue, int.MaxValue));

            return proceduralLevel;
        }

        private void GenerateFromSeed(ProceduralLevel level,int seed)
        {
            level.Seed = seed;
            Random.InitState(seed + level.Difficulty);
            if (m_LevelGeneratorDescription == null) return;
            m_LevelGeneratorDescription.Init(level);
            m_LevelGeneratorDescription.GenerateTags(level);
            m_LevelGeneratorDescription.GenerateAttributes(level);
            ContextData cd = ObjectsManager.FindObjectByIdentifier<ContextData>("core.datas.context");
            int playerLevel = 0;
            if (cd)
            {
                playerLevel = cd.Level;
            }
            m_LevelGeneratorDescription.GenerateWaveCount(level);
            m_LevelGeneratorDescription.GenerateWaveDelay(level);
            m_LevelGeneratorDescription.GenerateEnemyLevel(level, playerLevel);
            m_LevelGeneratorDescription.GeneratePossibleEnemies(level);
            m_LevelGeneratorDescription.GenerateSpawnCredits(level);
            m_LevelGeneratorDescription.GenerateName(level);
        }

        protected override void SuccessImpl()
        {
            m_SuccessEvent.Invoke();
        }

        protected override void FailImpl()
        {
            m_FailEvent.Invoke();
        }

        protected override EnemyModel SpawnEnemyImpl(Enemy enemy, Vector3 pos)
        {
            if (enemy == null) return null;
            EnemyModel enemyModel = Instantiate(enemy.Model, pos, Quaternion.identity,UIManager.EntityTransform);
            enemyModel.EntityBehaviour.Entity.Description.Enemy = enemy;
            EnemyController ec = enemyModel.EntityController as EnemyController;
            if (ec)
            {
                ec.RotateTowardsPosition(ec.transform.position + Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)) * new Vector3(0, 1, 0));
            }
            return enemyModel;

        }
        #endregion
    }
}
