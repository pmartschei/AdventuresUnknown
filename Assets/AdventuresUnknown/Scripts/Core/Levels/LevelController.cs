using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AdventuresUnknownSDK.Core.Objects.Inventories.ItemStack;

namespace AdventuresUnknown.Core.Levels
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(EntityBehaviour))]
    public class LevelController : MonoBehaviour, IActiveStat
    {
        [SerializeField] private Transform m_BorderParent = null;
        [SerializeField] private PushingBorder m_PushingBorderPrefab = null;
        [SerializeField] private BlockingBorder m_BlockingBorderPrefab = null;
        [SerializeField] private EntityController m_PlayerControllerPrefab = null;


        private EntityBehaviour m_EntityBehaviour = null;
        private BoxCollider m_BoxCollider = null;

        private List<Entity> m_RegisteredEntities = new List<Entity>();

        #region Properties
        #endregion
        #region Methods
        void Awake()
        {
            m_EntityBehaviour = GetComponent<EntityBehaviour>();
            m_BoxCollider = GetComponent<BoxCollider>();
            if (Application.isPlaying)
            {
                ReloadCurrentLevel();
                GenerateLevelBorders();
            }
        }

        private void Start()
        {
            if (!Application.isPlaying) return;
            SpawnPlayer();
        }
        private void Update()
        {
            if (GameSettingsManager.IsPaused) return;
            if (PlayerManager.PlayerController == null)
            {
                LevelManager.IsPaused = true;
                LevelManager.Fail();
            }
            if (LevelManager.CurrentLevel != null)
                LevelManager.CurrentLevel.Update();
        }

        private void SpawnPlayer()
        {
            if (m_PlayerControllerPrefab)
            {
                PlayerManager.PlayerController = Instantiate(m_PlayerControllerPrefab, Vector3.zero, Quaternion.identity,UIManager.EntityTransform);
                PlayerManager.SpaceShip.Entity.Notify(ActionTypeManager.Spawn);
            }
        }


        public void GenerateLevelBorders()
        {
            float pushingSize = 4.0f;
            float blockingSize = 50.0f;
            m_BorderParent.Clear();
            if (m_PushingBorderPrefab)
            {
                PushingBorder pushingBorderXNegative = Instantiate(m_PushingBorderPrefab, m_BorderParent);
                PushingBorder pushingBorderXPositive = Instantiate(m_PushingBorderPrefab, m_BorderParent);
                PushingBorder pushingBorderYNegative = Instantiate(m_PushingBorderPrefab, m_BorderParent);
                PushingBorder pushingBorderYPositive = Instantiate(m_PushingBorderPrefab, m_BorderParent);
                pushingBorderXNegative.BoxCollider.size = new Vector3(pushingSize, m_BoxCollider.size.y);
                pushingBorderXPositive.BoxCollider.size = new Vector3(pushingSize, m_BoxCollider.size.y);
                pushingBorderYNegative.BoxCollider.size = new Vector3(pushingSize, m_BoxCollider.size.x);
                pushingBorderYPositive.BoxCollider.size = new Vector3(pushingSize, m_BoxCollider.size.x);
                pushingBorderXNegative.transform.position = new Vector3(-pushingBorderXNegative.BoxCollider.size.x / 2.0f - m_BoxCollider.size.x / 2.0f + pushingSize, 0, 0);
                pushingBorderXPositive.transform.position = new Vector3(pushingBorderXPositive.BoxCollider.size.x / 2.0f + m_BoxCollider.size.x / 2.0f - pushingSize, 0, 0);
                pushingBorderYNegative.transform.position = new Vector3(0, -pushingBorderYNegative.BoxCollider.size.x / 2.0f - m_BoxCollider.size.y / 2.0f + pushingSize, 0);
                pushingBorderYPositive.transform.position = new Vector3(0, pushingBorderYPositive.BoxCollider.size.x / 2.0f + m_BoxCollider.size.y / 2.0f - pushingSize, 0);
                pushingBorderYPositive.transform.Rotate(new Vector3(0, 0, 1), -90.0f);
                pushingBorderXPositive.transform.Rotate(new Vector3(0, 0, 1), -180.0f);
                pushingBorderYNegative.transform.Rotate(new Vector3(0, 0, 1), -270.0f);
            }
            if (m_BlockingBorderPrefab)
            {
                BlockingBorder blockingBorderXNegative = Instantiate(m_BlockingBorderPrefab, m_BorderParent);
                BlockingBorder blockingBorderXPositive = Instantiate(m_BlockingBorderPrefab, m_BorderParent);
                BlockingBorder blockingBorderYNegative = Instantiate(m_BlockingBorderPrefab, m_BorderParent);
                BlockingBorder blockingBorderYPositive = Instantiate(m_BlockingBorderPrefab, m_BorderParent);
                blockingBorderXNegative.BoxCollider.size = new Vector3(blockingSize, m_BoxCollider.size.y + blockingSize * 2);
                blockingBorderXPositive.BoxCollider.size = new Vector3(blockingSize, m_BoxCollider.size.y + blockingSize * 2);
                blockingBorderYNegative.BoxCollider.size = new Vector3(blockingSize, m_BoxCollider.size.x + blockingSize * 2);
                blockingBorderYPositive.BoxCollider.size = new Vector3(blockingSize, m_BoxCollider.size.x + blockingSize * 2);
                blockingBorderXNegative.transform.position = new Vector3(-blockingBorderXNegative.BoxCollider.size.x / 2.0f - m_BoxCollider.size.x / 2.0f, 0, 0);
                blockingBorderXPositive.transform.position = new Vector3(blockingBorderXPositive.BoxCollider.size.x / 2.0f + m_BoxCollider.size.x / 2.0f, 0, 0);
                blockingBorderYNegative.transform.position = new Vector3(0, -blockingBorderYNegative.BoxCollider.size.x / 2.0f - m_BoxCollider.size.y / 2.0f, 0);
                blockingBorderYPositive.transform.position = new Vector3(0, blockingBorderYPositive.BoxCollider.size.x / 2.0f + m_BoxCollider.size.y / 2.0f, 0);
                blockingBorderYPositive.transform.Rotate(new Vector3(0, 0, 1), -90.0f);
                blockingBorderXPositive.transform.Rotate(new Vector3(0, 0, 1), -180.0f);
                blockingBorderYNegative.transform.Rotate(new Vector3(0, 0, 1), -270.0f);
            }
        }

        public void ReloadCurrentLevel()
        {
            LevelManager.IsPaused = false;
            m_EntityBehaviour.Entity.Reset();
            Register(m_EntityBehaviour.Entity);
            Level level = LevelManager.CurrentLevel;
            if (level != null)
            {
                level.Reset();
                m_BoxCollider.size = new Vector3(level.Width, level.Height, 0.0f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            EntityController controller = other.GetComponentInParent<EntityController>();
            if (controller)
            {
                Register(controller.SpaceShip.Entity);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            EntityController controller = other.GetComponentInParent<EntityController>();
            if (controller)
            {
                Unregister(controller.SpaceShip.Entity);
            }
        }

        private void OnDestroy()
        {
            foreach(Entity entity in m_RegisteredEntities)
            {
                RemoveAllModifiers(entity);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(m_BoxCollider.center, m_BoxCollider.size);
        }

        public void Retry()
        {
            SceneManager.LoadScene("core.scenes.play");
        }

        public void GotoHangar()
        {
            SceneManager.LoadScene("core.scenes.hangar");
        }

        private void ChangeModifiersAll()
        {
            foreach (Entity entity in m_RegisteredEntities)
            {
                AddModifiers(entity);
            }
        }

        private void AddModifiers(Entity entity)
        {
            RemoveAllModifiers(entity);

            Level level = LevelManager.CurrentLevel;
            if (level == null) return;
            foreach (ValueMod valueMod in level.Attributes)
            {
                valueMod.BasicModBase.AddStatModifierTo(entity, valueMod.Value, this);
            }
        }

        private void RemoveAllModifiers(Entity entity)
        {
            entity.RemoveAllModifiersBySource(this);
        }

        public void Register(Entity entity)
        {
            if (m_RegisteredEntities.Contains(entity)) return;
            m_RegisteredEntities.Add(entity);
            AddModifiers(entity);
        }

        public void Unregister(Entity entity)
        {
            m_RegisteredEntities.Remove(entity);
            RemoveAllModifiers(entity);
        }
        #endregion
    }

}