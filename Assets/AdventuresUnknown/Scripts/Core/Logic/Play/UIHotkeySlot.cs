﻿using AdventuresUnknown.Core.Items;
using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Entities.Controllers.Interfaces;
using AdventuresUnknownSDK.Core.Logic.ActiveGemContainers;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace AdventuresUnknown.Scripts.Core.Logic.Play
{
    public class UIHotkeySlot : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private string m_Identifier = "";
        [SerializeField] private HotkeyDataIdentifier m_HotkeyData = null;
        [SerializeField] private KeyCode m_Hotkey = KeyCode.Space;
        [SerializeField] private IGameText m_HotkeyText = null;
        [SerializeField] private AbstractActiveGemDisplay m_ActiveGemDisplay = null;
        [SerializeField] private Image m_CooldownImage = null;
        [SerializeField] private IGameText m_CooldownText = null;

        [SerializeField] private UIHotkeyPopup m_HotKeyPopupPrefab = null;

        private HotkeyDisplayData m_HotKeyDisplayData = null;
        private PlayerActiveGemContainer m_PlayerActiveGemContainer = null;

        private bool m_ValidInputDown = false;
        #region Properties
        public HotkeyDisplayData HotKeyDisplayData
        {
            get => m_HotKeyDisplayData;
            set
            {
                if (m_HotKeyDisplayData != value)
                {
                    m_HotKeyDisplayData = value;
                    if (m_HotKeyDisplayData != null && m_HotKeyDisplayData.Slot >= 0)
                    {
                        PlayerActiveGemContainer[] pagcs = PlayerManager.SpaceShip.GetComponentsInChildren<PlayerActiveGemContainer>(false);
                        foreach (PlayerActiveGemContainer pagc in pagcs)
                        {
                            if (!pagc.enabled) continue;
                            if (!pagc.ContainerName.Equals(m_HotKeyDisplayData.ContainerName)) continue;
                            m_PlayerActiveGemContainer = pagc;
                            break;
                        }
                    }
                    else
                    {
                        m_PlayerActiveGemContainer = null;
                    }
                    if (m_HotkeyData.ConsistencyCheck())
                    {
                        HotkeyData hkd = m_HotkeyData.Object;
                        hkd.PutHotkeyDisplayData(m_Identifier, m_HotKeyDisplayData);
                    }
                    UpdateDisplay();
                }
            }
        }
        #endregion
        #region Methods
        private void OnEnable()
        {

            if (m_HotkeyData.ConsistencyCheck())
            {
                HotkeyData hkd = m_HotkeyData.Object;
                HotkeyDisplayData hkDisplayData = hkd.GetHotkeyDisplayData(m_Identifier);
                if (hkDisplayData != null)
                {
                    PlayerActiveGemContainer[] pagcs = PlayerManager.SpaceShip.GetComponentsInChildren<PlayerActiveGemContainer>(false);
                    PlayerActiveGemContainer foundContainer = null;
                    foreach (PlayerActiveGemContainer pagc in pagcs)
                    {
                        if (pagc.ContainerName.Equals(hkDisplayData.ContainerName))
                        {
                            foundContainer = pagc;
                            break;
                        }
                    }
                    if (foundContainer != null)
                    {
                        ActiveGem activeGem = foundContainer.Inventory.Items[hkDisplayData.Slot].Item as ActiveGem;
                        if (activeGem != null)
                        {
                            m_HotKeyDisplayData = hkDisplayData;
                            m_HotKeyDisplayData.ActiveGem = activeGem;
                            m_PlayerActiveGemContainer = foundContainer;
                        }
                    }
                }
            }
        }

        private void OnDisable()
        {

        }
        private void UpdateDisplay()
        {
            Entity entity = null;
            if (m_ActiveGemDisplay)
            {
                ActiveGem activeGem = null;
                string[] modTypes = null;
                if (m_HotKeyDisplayData != null && m_HotKeyDisplayData.Slot >= 0)
                {
                    activeGem = m_HotKeyDisplayData.ActiveGem;
                    if (m_PlayerActiveGemContainer)
                    {
                        if (m_PlayerActiveGemContainer.Inventory.Items[m_HotKeyDisplayData.Slot].Item == activeGem)
                        {
                            entity = m_PlayerActiveGemContainer.GetEntityWithApply(m_HotKeyDisplayData.Slot);
                            modTypes = m_PlayerActiveGemContainer.CalculateDisplayMods(m_HotKeyDisplayData.Slot);
                        }
                        else
                        {
                            m_HotKeyDisplayData = null;
                            m_PlayerActiveGemContainer = null;
                            return;
                        }
                    }
                }
                m_ActiveGemDisplay.Display(activeGem, entity, modTypes);
            }
            if (m_HotkeyText)
            {
                m_HotkeyText.SetText(m_Hotkey.ToString());
            }

            float cooldown = GetCooldown(entity);

            if (cooldown > 0.0f)
            {
                if (m_CooldownText)
                {
                    m_CooldownText.SetText(cooldown.ToString("0.0"));
                }
                if (m_CooldownImage)
                {
                    m_CooldownImage.fillAmount = cooldown / 1.0f;
                }
            }
            else
            {
                if (m_CooldownText)
                {
                    m_CooldownText.SetText("");
                }
                if (m_CooldownImage)
                {
                    m_CooldownImage.fillAmount = 0.0f;
                }
            }
        }

        private float GetCooldown(Entity entity)
        {
            float cdGlobal = 0.0f;
            float cdGem = 0.0f;
            if (m_PlayerActiveGemContainer != null)
            {
                cdGlobal = entity.GetStat("core.modtypes.skills.casttime").Calculated > 0 ? CooldownManager.GetCooldown(m_PlayerActiveGemContainer.EntityStats.Entity) : 0;
                cdGem = m_PlayerActiveGemContainer.GetCooldown(m_HotKeyDisplayData.Slot);
            }
            return Mathf.Max(cdGem, cdGlobal);
        }

        private void Update()
        {
            UpdateDisplay();
        }
        private void FixedUpdate()
        {
            if (m_HotKeyDisplayData == null) return;
            //bool spawn = false;
            //if (!m_ValidInputDown && Input.GetKeyDown(m_Hotkey))
            //{
            //    m_ValidInputDown = !EventSystem.current.IsPointerOverGameObject();
            //    spawn = m_ValidInputDown;
            //}
            //else if (Input.GetKey(m_Hotkey) && m_ValidInputDown)
            //{
            //    spawn = true;
            //}
            //else
            //{
            //    m_ValidInputDown = false;
            //}
            //if (spawn)
            if (Input.GetKey(m_Hotkey))
            {
                if (m_PlayerActiveGemContainer && PlayerManager.PlayerController)
                {
                    IMuzzleComponentController muzzleComponentController = PlayerManager.PlayerController as IMuzzleComponentController;
                    m_PlayerActiveGemContainer.Spawn(PlayerManager.PlayerController, m_HotKeyDisplayData.Slot, muzzleComponentController != null ? muzzleComponentController.Muzzles : null);
                }
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerActiveGemContainer[] pagcs = PlayerManager.SpaceShip.GetComponentsInChildren<PlayerActiveGemContainer>(false);

            List<HotkeyDisplayData> displayDatas = new List<HotkeyDisplayData>();
            displayDatas.Add(GetClearDisplayData());
            foreach (PlayerActiveGemContainer pagc in pagcs)
            {
                if (!pagc.enabled) continue;
                int i = 0;
                foreach (ItemStack itemStack in pagc.Inventory.Items)
                {
                    if (itemStack.IsEmpty)
                    {
                        i++;
                        continue;
                    }
                    ActiveGem activeGem = itemStack.Item as ActiveGem;
                    if (activeGem == null)
                    {
                        i++;
                        continue;
                    }
                    HotkeyDisplayData hotkeyDisplayData = new HotkeyDisplayData();
                    hotkeyDisplayData.ActiveGem = activeGem;
                    hotkeyDisplayData.ContainerName = pagc.ContainerName;
                    hotkeyDisplayData.Slot = i;
                    hotkeyDisplayData.Container = pagc;
                    hotkeyDisplayData.Identifier = m_Identifier;
                    displayDatas.Add(hotkeyDisplayData);
                    i++;
                }
            }
            UIHotkeyPopup hotkeyPopup = Instantiate(m_HotKeyPopupPrefab, this.transform.position, Quaternion.identity, transform.root);
            hotkeyPopup.Display(this, displayDatas);
            EventSystem.current.SetSelectedGameObject(hotkeyPopup.gameObject);
        }

        private HotkeyDisplayData GetClearDisplayData()
        {
            HotkeyDisplayData hotkeyDisplayData = new HotkeyDisplayData();

            hotkeyDisplayData.ActiveGem = ObjectsManager.FindObjectByIdentifier<ActiveGem>("core.items.gems.clearhotkey");
            hotkeyDisplayData.ContainerName = "";
            hotkeyDisplayData.Slot = -1;

            return hotkeyDisplayData;
        }
        #endregion
    }
}