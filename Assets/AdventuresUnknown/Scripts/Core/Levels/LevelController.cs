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
                PlayerManager.SpaceShip.EntityController = PlayerManager.PlayerController;
                PlayerManager.SpaceShip.Entity.Notify(ActionTypeManager.Spawn);
            }
        }


        public void GenerateLevelBorders()
        {
            Level level = LevelManager.CurrentLevel;
            if (level != null)
            {
                level.Build(m_BorderParent);
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