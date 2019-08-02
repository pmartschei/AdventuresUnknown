using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using AdventuresUnknown.Utils;
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
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Objects.Currencies;
using AdventuresUnknownSDK.Core.Objects.GameModes;

namespace Assets.AdventuresUnknown.Core.Managers
{
    public class PlayerManagerImpl : PlayerManager
    {
        [SerializeField] private EntityBehaviour m_PlayerSpaceShip = null;
        [SerializeField] private EntityController m_PlayerController = null;
        [SerializeField] private Wallet m_PlayerWallet = null;

        [SerializeField] private string m_Folder = "Saves/";
        [SerializeField] private string m_Extension = ".sav";
        private Hashtable m_PlayerDataHashtable = new Hashtable();
        private UnityEvent m_OnWalletDisplayChangeEvent = new UnityEvent();
        private Currency[] m_WalletDisplay = new Currency[0];

        public string CompleteSavePath { get; private set; }

        #region Properties
        protected override string FileExtensionImpl { get => m_Extension; }
        protected override EntityBehaviour SpaceShipImpl => m_PlayerSpaceShip;
        protected override EntityController PlayerControllerImpl { get => m_PlayerController; set => m_PlayerController = value; }

        protected override Wallet PlayerWalletImpl => m_PlayerWallet;
        protected override UnityEvent OnWalletDisplayChangeImpl => m_OnWalletDisplayChangeEvent;
        protected override Currency[] WalletDisplayImpl => m_WalletDisplay;

        #endregion

        #region Methods
        public void Awake()
        {
            int index = m_Folder.LastIndexOf('/');
            bool addSlash = (index == -1 || index != m_Folder.Length - 1);
            CompleteSavePath = Application.persistentDataPath + "/" + m_Folder + (addSlash ? "/" : "");
        }
        public void Init()
        {
        }

        protected FileObject LoadFileObject(string file)
        {
            if (!File.Exists(CompleteSavePath + file)) return null;
            using (FileStream fileStream = new FileStream(CompleteSavePath + file, FileMode.Open))
            {
                IFormatter formatter = new AdventuresUnknownFormatter();
                FileObject fileObject = formatter.Deserialize(fileStream) as FileObject;
                ContextData cd = fileObject.Get("core.datas.context") as ContextData;
                if (cd)
                {
                    cd.SaveFileName = file.Substring(file.LastIndexOf("/") + 1);
                }
                return fileObject;
            }
        }
        protected override void DeleteImpl(string file)
        {
            string fullPath = CompleteSavePath + file;
            if (!File.Exists(fullPath)) return;
            File.Delete(fullPath);
        }
        protected override void LoadImpl(string file)
        {
            FileObject fileObject = LoadFileObject(file);
            if (fileObject == null) return;
            fileObject.Load();
        }
        protected override void LoadImpl(FileObject fileObject)
        {
            if (fileObject == null) return;
            ContextData cd = fileObject.Get("core.datas.context") as ContextData;
            GameMode gameMode = ObjectsManager.FindObjectByIdentifier<GameMode>(cd.GameMode);
            if (cd == null) return;
            if (gameMode == null) return;
            LoadImpl(gameMode.FolderName + "/" + cd.SaveFileName);
        }

        protected override void SaveImpl()
        {
            ContextData cd = ObjectsManager.FindObjectByIdentifier<ContextData>("core.datas.context");
            GameMode gameMode = ObjectsManager.FindObjectByIdentifier<GameMode>(cd.GameMode);
            if (cd == null) return;
            if (gameMode == null) return;
            DirectoryInfo directoryInfo = Directory.CreateDirectory(CompleteSavePath+gameMode.FolderName+"/");
            if (!directoryInfo.Exists) return;

            string fileName = cd.SaveFileName;
            if (fileName.Equals(""))
            {
                fileName = CurrentSaveFileName;
            }
            if (fileName.Equals(""))
            {
                fileName = "default" + FileExtension;
            }
            using (FileStream fileStream = new FileStream(directoryInfo.FullName + fileName, FileMode.Create))
            {
                //only process datas which are used (for different gamemodes?)
                IPlayerData[] playerDatas = ObjectsManager.GetAllObjects<IPlayerData>();
                if (playerDatas == null) return;
                FileObject fileObject = new FileObject();
                foreach (IPlayerData playerData in playerDatas)
                {
                    fileObject.Put(playerData.Identifier, playerData);
                }
                IFormatter formatter = new AdventuresUnknownFormatter();
                formatter.Serialize(fileStream, fileObject);
            }
        }

        protected override void SetWalletDisplayImpl(params string[] identifiers)
        {
            List<Currency> currencies = new List<Currency>();
            foreach(string identifier in identifiers)
            {
                Currency currency = ObjectsManager.FindObjectByIdentifier<Currency>(identifier);
                if (!currency) continue;
                currencies.Add(currency);
            }

            m_WalletDisplay = currencies.ToArray();

            m_OnWalletDisplayChangeEvent.Invoke();
        }

        protected override FileObject[] ListSavesImpl(GameMode gameMode)
        {
            List<FileObject> fileObjects = new List<FileObject>();
            if (gameMode == null) return fileObjects.ToArray();
            if (!Directory.Exists(CompleteSavePath + gameMode.FolderName + "/")) return fileObjects.ToArray();
            string[] fileNames = Directory.GetFiles(CompleteSavePath + gameMode.FolderName + "/");
            foreach(string fileName in fileNames)
            {
                FileObject fileObject = LoadFileObject(gameMode.FolderName + "/" + fileName.Substring(fileName.LastIndexOf("/")+1));
                ContextData cd = fileObject.Get("core.datas.context") as ContextData;
                if (cd == null) continue;
                if (!cd.GameMode.Equals(gameMode.Identifier)) continue;
                fileObjects.Add(fileObject);
            }
            return fileObjects.ToArray();
        }
        #endregion
    }
}
