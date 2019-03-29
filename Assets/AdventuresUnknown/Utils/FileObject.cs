using AdventuresUnknownSDK.Core.Datas;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Utils.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Utils
{
    [Serializable]
    public class FileObject : IAdventuresUnknownSerializeCallback
    {
        private List<IPlayerData> m_DataList = new List<IPlayerData>();
        private List<IPlayerData> m_CorruptedDataList = new List<IPlayerData>();
        [NonSerialized]
        private Dictionary<string,IPlayerData> m_Data = new Dictionary<string, IPlayerData>();

        #region Properties

        #endregion

        #region Methods

        public void InitializeObject()
        {
            m_DataList = new List<IPlayerData>();
            m_Data = new Dictionary<string, IPlayerData>();
            m_CorruptedDataList = new List<IPlayerData>();
        }
        public void Clear()
        {
            m_Data.Clear();
        }
        public IPlayerData Get(string key)
        {
            if (m_Data.ContainsKey(key)) return null;
            return m_Data[key];
        }

        public bool OnAfterDeserialize()
        {
            //if (m_Data == null)
            //    m_Data = new Dictionary<string, IPlayerData>();
            //m_CorruptedDataList.Clear();
            //m_Data.Clear();
            //foreach(IPlayerData dataItem in m_DataList)
            //{
            //    Type type = Type.GetType(dataItem.Type);
            //    if (type == null)
            //    {
            //        m_CorruptedDataList.Add(dataItem);
            //        continue;
            //    }
            //    IPlayerData playerData = ObjectsManager.FindObjectByIdentifier(dataItem.Identifier, type) as IPlayerData;
            //    if (playerData == null)
            //    {
            //        m_CorruptedDataList.Add(dataItem);
            //        continue;
            //    }
            //    JsonUtility.FromJsonOverwrite(dataItem.PlayerData, playerData);
            //    playerData.OnAfterDeserialization();
            //}
            //m_Data.Clear();
            return true;
        }

        public bool OnBeforeSerialize()
        {
            m_DataList.Clear();
            foreach(string key in m_Data.Keys)
            {
                IPlayerData playerData = m_Data[key];
                m_DataList.Add(playerData);
            }
            foreach(IPlayerData dataItem in m_CorruptedDataList)
            {
                m_DataList.Add(dataItem);
            }
            return true;
        }

        public void Put(string key,IPlayerData playerData)
        {
            if (m_Data.ContainsKey(key))
            {
                m_Data[key] = playerData;
            }
            else
            {
                m_Data.Add(key, playerData);
            }
        }
        
        #endregion
    }
}
