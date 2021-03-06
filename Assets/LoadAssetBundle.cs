﻿using AdventuresUnknownSDK;
using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using AdventuresUnknown.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Localization;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Log;
using System.Text;
using AdventuresUnknown.Core.Utils;
using AdventuresUnknown.Core.Commands;
using AdventuresUnknownSDK.Core.Objects.Datas;

public class LoadAssetBundle : MonoBehaviour {

    public Log m_Log;
    // Use this for initialization
    void Start() {
        AssetBundle ab = AssetBundle.LoadFromFile("Assets/mod");
        if (ab)
        {
            TextAsset tas = ab.LoadAsset("ScriptsLibrary", typeof(TextAsset)) as TextAsset;
            Assembly assembly = Assembly.Load(tas.bytes);

            //IEnumerable<Type> logicHandlerTypes = GetDerivedTypes(assembly, typeof(AbstractLogicHandler));

            //foreach (Type logicHandlerType in logicHandlerTypes)
            //{
            //    AbstractLogicHandler logicHandler = Activator.CreateInstance(logicHandlerType) as AbstractLogicHandler;
            //    if (logicHandler != null)
            //    {
            //        logicHandler.Init();
            //    }
            //}

            //Version[] version = ab.LoadAllAssets<Version>();
            //Debug.LogFormat("sdk version = {0}.{1}.{2}", SDKInfo.MAJOR_VERSION, SDKInfo.MINOR_VERSION, SDKInfo.FIX_VERSION);
            //if (version.Length > 0)
            //{
            //    Debug.Log("version name = " + version[0].VersionName);

            //    Item[] modItems = ab.LoadAllAssets<Item>();

            //    ObjectsManagerImpl omi = ObjectsManagerImpl.Instance as ObjectsManagerImpl;
            //    if (omi)
            //    {
            //        omi.Load(modItems);
            //    }
            //}

            GameObject[] gels = ab.LoadAllAssets<GameObject>();
            foreach(GameObject gel in gels)
            {
                Instantiate(gel);
            }
            //IPlayerData[] playerDatas = ab.LoadAllAssets<IPlayerData>();
            //ObjectsManagerImpl omi = ObjectsManager.Instance as ObjectsManagerImpl;
            //if (omi)
            //{
            //    omi.Load(playerDatas);
            //}
        }
        //Test[] tests = ab.LoadAllAssets<Test>();
        //foreach(Test test in tests)
        //{
        //    Debug.Log(test.name);
        //}
        //LogicHandler[] logicHandlers = ab.LoadAllAssets<LogicHandler>();
        //foreach(LogicHandler logicHandler in logicHandlers)
        //{
        //    logicHandler.Init();
        //}

        Inventory inventory = ObjectsManager.FindObjectByIdentifier<Inventory>("core.inventories.equipment");
        if (inventory)
        {
            inventory.Register(PlayerManager.SpaceShip.Entity);
        }
        CommandManager.AddCommand(new HelpCommand("help"));
        CommandManager.AddCommand(new ClearCommand("clear"));
        CommandManager.AddCommand(new CreateItemCommand("createitem"));
        CommandManager.AddCommand(new HealCommand("heal"));
        CommandManager.AddCommand(new LoadSceneCommand("loadscene"));
        CommandManager.AddCommand(new SpawnEnemyCommand("spawnenemy"));
        CommandManager.AddCommand(new SaveCommand("save"));
        CommandManager.AddCommand(new LoadCommand("load"));
        CommandManager.AddCommand(new GenerateNewLevelsCommand("generatelevels"));
    }

    public IEnumerable<Type> GetDerivedTypes(Assembly assembly,Type baseType)
    {
        return assembly.GetTypes().Where(type => type != baseType && baseType.IsAssignableFrom(type));
    }
	
	// Update is called once per frame
	//void Update () {
	//	if (Input.GetKey(KeyCode.A))
 //       {
 //           GameSettingsManager.Language = ObjectsManager.FindObjectByIdentifier<LocalizationLanguage>("core.languages.german");
 //       }else if (Input.GetKey(KeyCode.S))
 //       {
 //           GameSettingsManager.Language = ObjectsManager.FindObjectByIdentifier<LocalizationLanguage>("core.languages.english");
 //       }
	//}
}
