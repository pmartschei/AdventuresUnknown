using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class InitializeGame : MonoBehaviour
{
    [SerializeField] private InventoryIdentifier m_Equipment = null;
    [SerializeField] private ContextDataIdentifier m_ContextData = null;
    [SerializeField] private ModTypeIdentifier m_LevelMod = null;
    [SerializeField] private string m_StartingScene = "core.scenes.title";

    private StatModifier m_LevelStatModifier = null;
    void Start()
    {
        if (m_Equipment.ConsistencyCheck())
        {
            m_Equipment.Object.Register(PlayerManager.SpaceShip.Entity);
        }
        int level = 0;
        if (m_ContextData.ConsistencyCheck())
        {
            m_ContextData.Object.OnLevelChange.AddListener(OnLevelChange);
            level = m_ContextData.Object.Level;
        }
        m_LevelStatModifier = new StatModifier(level, AdventuresUnknownSDK.Core.Objects.Mods.CalculationType.Flat, this);
        PlayerManager.SpaceShip.Entity.GetStat(m_LevelMod.Identifier).AddStatModifier(m_LevelStatModifier);
        PlayerManager.SpaceShip.Entity.Notify(ActionTypeManager.Spawn);
        PlayerManager.SetWalletDisplay("core.currencies.gold");

        SceneManager.LoadScene(m_StartingScene);

        //Application.logMessageReceived += Application_logMessageReceived;
        //GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        GameConsole.LogError(condition);
        GameConsole.LogError(stackTrace);
    }

    private void OnLevelChange(int level)
    {
        m_LevelStatModifier.Value = level;
    }
}
