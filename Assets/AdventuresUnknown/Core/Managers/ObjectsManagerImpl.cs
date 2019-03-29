using AdventuresUnknownSDK.Core.Datas;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.DropTables;
using AdventuresUnknownSDK.Core.Objects.Events;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.Objects.Localization;
using AdventuresUnknownSDK.Core.Objects.Mods;
using AdventuresUnknownSDK.Core.Objects.Mods.ModBases;
using AdventuresUnknownSDK.Core.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AdventuresUnknown.Core.Managers
{
    public class ObjectsManagerImpl : ObjectsManager
    {

        private Hashtable m_TypeHashtable = new Hashtable();

        #region Properties

        #endregion

        #region Methods
        public void PreInit()
        {
            LoadCoreObjects<LocalizationLanguage>();
            GameSettingsManager.Language = FindObjectByIdentifier<LocalizationLanguage>("core.languages.english");
        }
        public void Init()
        {
            LoadCoreObjects<GameEvent>();
            LoadCoreObjects<ModTypeFormatter>();
            LoadCoreObjects<ModType>();
            LoadCoreObjects<BasicModBase>();
            LoadCoreObjects<Mod>();
            LoadCoreObjects<ItemType>();
            LoadCoreObjects<Item>();
            LoadCoreObjects<DropTable>();
            LoadCoreObjects<Inventory>();
            LoadCoreObjects<IPlayerData>();
        }

        public void Load<T>(T[] objects) where T : CoreObject
        {
            if (objects == null || objects.Length == 0) return;
            Hashtable hashtable = GetHashtableOrAddNew<T>();
            int startingCount = hashtable.Count;
            List<T> objectsAdded = new List<T>();

            foreach (T loadedObject in objects)
            {
                if (loadedObject.Identifier.Equals(""))
                {
                    GameConsole.LogErrorFormat("Empty Identifier {0} : {1}", typeof(T).Name, loadedObject.Identifier);
                    continue;
                }
                if (hashtable.ContainsKey(loadedObject.Identifier) && !loadedObject.Overrides)
                {
                    GameConsole.LogErrorFormat("Duplicate {0} : {1}", typeof(T).Name, loadedObject.Identifier);
                    continue;
                }
                if (!loadedObject.Overrides)
                {
                    hashtable.Add(loadedObject.Identifier, loadedObject);
                }
                objectsAdded.Add(loadedObject);
            }

            foreach (T objectInHashtable in objectsAdded)
            {
                if (!objectInHashtable.ConsistencyCheck())
                {
                    GameConsole.LogWarningFormat("Skipped inconsistent {0} : {1}", typeof(T).Name, objectInHashtable.Identifier);
                    hashtable.Remove(objectInHashtable);
                    continue;
                }
                if (objectInHashtable.Overrides)
                {
                    hashtable[objectInHashtable.Identifier] = objectInHashtable;
                }
            }

            foreach(T loadedObject in objectsAdded)
            {
                loadedObject.Initialize();
            }
            int count = hashtable.Count - startingCount;
            GameConsole.LogFormat("Loaded {0} {1}.", count, typeof(T).Name);
        }

        private void LoadCoreObjects<T>() where T : CoreObject
        {
            T[] loadedObjects = Resources.LoadAll<T>("");
            Load(loadedObjects);
        }

        private Hashtable GetHashtableOrAddNew<T>() where T : CoreObject
        {
            Hashtable found = new Hashtable();

            Type type = GetBaseType<T>();
            if (m_TypeHashtable.ContainsKey(type))
            {
                found = m_TypeHashtable[type] as Hashtable;
            }
            else
            {
                m_TypeHashtable.Add(type, found);
            }

            return found;
        }
        private static Type GetBaseType<T>()
        {
            Type type = typeof(T);
            return GetBaseType(type);
        }
        private static Type GetBaseType(Type type)
        {
            Type lastType = type;
            Type coreObjectType = typeof(CoreObject);
            while (type != coreObjectType)
            {
                lastType = type;
                type = type.BaseType;
            }
            return lastType;
        }
        protected override T FindObjectByIdentifierImpl<T>(string identifier)
        {
            if (identifier.Equals("")) return default(T);
            Type type = GetBaseType<T>();
            if (!m_TypeHashtable.ContainsKey(type)) return default(T);
            Hashtable hashtable = m_TypeHashtable[type] as Hashtable;
            if (!hashtable.ContainsKey(identifier)) return default(T);
            object obj = hashtable[identifier];
            if (obj == null) return default(T);
            if (!(obj is T)) return default(T);
            return (T)obj;
        }
        protected override CoreObject FindObjectByIdentifierImpl(string identifier,Type type)
        {
            if (identifier.Equals("")) return null;
            type = GetBaseType(type);
            if (!m_TypeHashtable.ContainsKey(type)) return null;
            Hashtable hashtable = m_TypeHashtable[type] as Hashtable;
            if (!hashtable.ContainsKey(identifier)) return null;
            object obj = hashtable[identifier];
            if (obj == null) return null;
            if (!(obj is CoreObject)) return null;
            return obj as CoreObject;
        }
        protected override T[] GetAllObjectsImpl<T>()
        {
            Type type = GetBaseType<T>();
            if (!m_TypeHashtable.ContainsKey(type)) return null;
            Hashtable hashtable = m_TypeHashtable[type] as Hashtable;
            T[] objs = new T[hashtable.Values.Count];
            hashtable.Values.CopyTo(objs, 0);
            return objs;
        }

        #endregion
    }
}
