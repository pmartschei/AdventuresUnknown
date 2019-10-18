using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AdventuresUnknown.Scripts.Core.Managers
{
    public class PoolManagerImpl : PoolManager
    {
        private Dictionary<GameObject, PoolContext> m_PoolDictionary = new Dictionary<GameObject, PoolContext>();


        private class PoolContext
        {
            public PoolDescription PoolDescription;
            public Queue<GameObject> Queue;
            public Transform Transform;
        }
        protected override void CreatePoolImpl(PoolDescription poolDescription)
        {
            if (poolDescription == null) return;
            PoolContext poolContext = null;
            if (!m_PoolDictionary.TryGetValue(poolDescription.Prefab, out poolContext))
            {
                poolContext = new PoolContext();
                poolContext.Queue = new Queue<GameObject>();
                poolContext.PoolDescription = poolDescription;
                GameObject child = new GameObject("Pool: "+poolDescription.Prefab.name);
                child.transform.SetParent(this.transform);
                poolContext.Transform = child.transform;
                m_PoolDictionary.Add(poolDescription.Prefab, poolContext);
            }

            for (int i = poolContext.Queue.Count; i < poolDescription.PoolSize; i++)
            {
                GameObject instance = Object.Instantiate(poolDescription.Prefab);
                instance.GetComponent<PoolDescription>().Prefab = poolDescription.Prefab;
                instance.transform.SetParent(poolContext.Transform);
                instance.SetActive(false);
                poolContext.Queue.Enqueue(instance);
            }
        }

        protected override bool EnqueueInstanceImpl(GameObject instance, PoolDescription poolDescription)
        {
            if (instance == null || poolDescription == null) return false;
            PoolContext poolContext = null;
            if (m_PoolDictionary.TryGetValue(poolDescription.Prefab, out poolContext))
            {

                IPooledComponent[] pooledComponents = instance.GetComponentsInChildren<IPooledComponent>();

                foreach (IPooledComponent pooledComponent in pooledComponents)
                {
                    pooledComponent.OnPreDisable();
                }
                instance.SetActive(false);
                foreach (IPooledComponent pooledComponent in pooledComponents)
                {
                    pooledComponent.OnPostDisable();
                }
                instance.transform.SetParent(poolContext.Transform);
                poolContext.Queue.Enqueue(instance);
                return true;
            }
            return false;
        }
        protected override GameObject InstantiateImpl(GameObject gameObject)
        {
            GameObject instance = SpawnGameObject(gameObject);
            return instance;
        }

        private GameObject SpawnGameObject(GameObject gameObject)
        {
            PoolContext poolContext = null;
            if (m_PoolDictionary.TryGetValue(gameObject, out poolContext))
            {
                GameObject instance = null;
                if (poolContext.Queue.Count == 0)
                {
                    PoolDescription poolDescription = poolContext.PoolDescription;
                    instance = Object.Instantiate(poolDescription.Prefab);
                    instance.GetComponent<PoolDescription>().Prefab = poolDescription.Prefab;
                }
                else
                {
                    instance = poolContext.Queue.Dequeue();
                }

                IPooledComponent[] pooledComponents = instance.GetComponentsInChildren<IPooledComponent>();

                foreach (IPooledComponent pooledComponent in pooledComponents)
                {
                    pooledComponent.OnSpawn();
                }
                return instance;
            }
            else
            {
                GameConsole.LogErrorFormat("Could not Instantiate object {0} from Pool", gameObject);
                return Object.Instantiate(gameObject);
            }
            return null;
        }

        protected override GameObject InstantiateImpl(GameObject gameObject, Transform parent)
        {
            GameObject instance = SpawnGameObject(gameObject);
            if (instance)
            {
                instance.transform.SetParent(parent);
                Activate(instance);
            }
            return instance;
        }

        protected override GameObject InstantiateImpl(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            GameObject instance = SpawnGameObject(gameObject);
            if (instance)
            {
                instance.transform.position = position;
                instance.transform.rotation = rotation;
                Activate(instance);
            }
            return instance;
        }

        protected override GameObject InstantiateImpl(GameObject gameObject, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject instance = SpawnGameObject(gameObject);
            if (instance)
            {
                instance.transform.SetParent(parent);
                instance.transform.position = position;
                instance.transform.rotation = rotation;
                Activate(instance);
            }
            return instance;
        }

        private void Activate(GameObject gameObject)
        {
            IPooledComponent[] pooledComponents = gameObject.GetComponentsInChildren<IPooledComponent>();

            foreach (IPooledComponent pooledComponent in pooledComponents)
            {
                pooledComponent.OnPreEnable();
            }
            gameObject.SetActive(true);
            foreach (IPooledComponent pooledComponent in pooledComponents)
            {
                pooledComponent.OnPostEnable();
            }
        }

    }
}
