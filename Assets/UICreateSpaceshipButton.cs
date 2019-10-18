using AdventuresUnknownRuntime.Core.Utils.Identifiers;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Currencies;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreateSpaceshipButton : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_InputField m_NameInput = null;
    [SerializeField] private GameModeIdentifier m_GameMode = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Create()
    {
        if (!m_GameMode.ConsistencyCheck()) return;
        string shipName = "Default";
        if (m_NameInput)
        {
            shipName = m_NameInput.text;
        }
        ContextData cd = ObjectsManager.FindObjectByIdentifier<ContextData>("core.datas.context");
        IPlayerData[] datas = ObjectsManager.GetAllObjects<IPlayerData>();
        foreach (IPlayerData data in datas)
        {
            data.Reset();
        }
        if (cd == null) return;
        cd.GameMode = m_GameMode.Identifier;
        cd.ShipName = shipName;
        //unique file name
        cd.SaveFileName = cd.ShipName + PlayerManager.FileExtension;
        //starting gear
        Inventory gems = ObjectsManager.FindObjectByIdentifier<Inventory>("core.inventories.gems");
        Wallet wallet = ObjectsManager.FindObjectByIdentifier<Wallet>("core.wallets.player");
        wallet.AddValue("core.currencies.gold", 100);
        gems.SetItemStack(ObjectsManager.FindObjectByIdentifier<Item>("core.items.gems.minigun").CreateItem(),0);
        HotkeyData hotkeyData = ObjectsManager.FindObjectByIdentifier<HotkeyData>("core.datas.hotkeys");
        HotkeyDisplayData hotkeyDisplayData = new HotkeyDisplayData();
        hotkeyDisplayData.ActiveGem = gems.Items[0].Item as ActiveGem;
        hotkeyDisplayData.Slot = 0;
        hotkeyDisplayData.ContainerName = gems.Identifier;
        hotkeyDisplayData.Identifier = "slot_0";
        hotkeyData.PutHotkeyDisplayData(hotkeyDisplayData.Identifier, hotkeyDisplayData);
        PlayerManager.Save();
        PlayerManager.Load(m_GameMode.Object.FolderName+"/"+cd.SaveFileName);
        SceneManager.LoadScene("core.scenes.hangar");
    }
}
