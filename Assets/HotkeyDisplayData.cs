using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using System;
using UnityEngine;

[Serializable]
public class HotkeyDisplayData
{
    [SerializeField] private string m_ContainerName = "";
    [SerializeField] private ActiveGem m_ActiveGem = null;
    [SerializeField] private int m_Slot = -1;

    public string ContainerName { get => m_ContainerName; set => m_ContainerName = value; }
    public ActiveGem ActiveGem { get => m_ActiveGem; set => m_ActiveGem = value; }
    public int Slot { get => m_Slot; set => m_Slot = value; }
}