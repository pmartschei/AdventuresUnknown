using AdventuresUnknownSDK.Core.Datas;
using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using Assets.AdventuresUnknown.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.AdventuresUnknown.Core.Managers
{
    public class PlayerManagerImpl : PlayerManager
    {
        [SerializeField] private SpaceShip m_PlayerSpaceShip = null;

        [SerializeField] private string m_Folder = "Saves/";
        [SerializeField] private string m_Extension = ".sav";
        private Hashtable m_PlayerDataHashtable = new Hashtable();

        public string CompleteSavePath { get; private set; }


        private FileObject m_FileObject = new FileObject();
        #region Properties
        protected override SpaceShip SpaceShipImpl => m_PlayerSpaceShip;
        #endregion

        #region Methods
        public void Start()
        {
            int index = m_Folder.LastIndexOf('/');
            bool addSlash = (index == -1 || index != m_Folder.Length - 1);
            CompleteSavePath = Application.persistentDataPath + "/" + m_Folder + (addSlash ? "/" : "");
        }
        public void Init()
        {
        }

        protected override void LoadImpl(string file)
        {
            IFormatter formatter = new AdventuresUnknownFormatter();
            if (!File.Exists(CompleteSavePath + file)) return;
            using (FileStream fileStream = new FileStream(CompleteSavePath + file, FileMode.Open))
            {
                m_FileObject = formatter.Deserialize(fileStream) as FileObject;
            }
        }

        protected override void SaveImpl()
        {
            IFormatter formatter = new AdventuresUnknownFormatter();
            DirectoryInfo directoryInfo = Directory.CreateDirectory(CompleteSavePath);
            if (!directoryInfo.Exists) return;
            string fileName = "test" + m_Extension;
            using (FileStream fileStream = new FileStream(CompleteSavePath + fileName, FileMode.Truncate))
            {
                IPlayerData[] playerDatas = ObjectsManager.GetAllObjects<IPlayerData>();
                if (playerDatas == null) return;
                m_FileObject.Clear();
                foreach (IPlayerData playerData in playerDatas)
                {
                    m_FileObject.Put(playerData.Identifier, playerData);
                }
                formatter.Serialize(fileStream, m_FileObject);
            }
        }
        public void SaveTest()
        {
            SaveImpl();
        }
        public void LoadTest()
        {
            LoadImpl("test.sav");
        }
        #endregion
    }
}
